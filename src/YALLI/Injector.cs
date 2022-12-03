using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

using YALLI.Extensions;
using YALLI.Native;
using YALLI.Native.Enumerations;
using YALLI.Native.PInvoke;

namespace YALLI
{
    public static class Injector
    {
        private const ProcessAccessFlags DESIRED_PROCESS_ACCESS =
            ProcessAccessFlags.PROCESS_CREATE_THREAD |
            ProcessAccessFlags.PROCESS_QUERY_INFORMATION |
            ProcessAccessFlags.PROCESS_VM_OPERATION |
            ProcessAccessFlags.PROCESS_VM_READ |
            ProcessAccessFlags.PROCESS_VM_WRITE;

        public static IntPtr Inject(
            string processName,
            string dllPath)
        {
            return Inject(
                processName,
                (processes) => processes.FirstOrDefault(),
                dllPath);
        }

        public static IntPtr Inject(
            string processName,
            Func<Process[], Process> processResolver,
            string dllPath)
        {
            if (string.IsNullOrWhiteSpace(processName))
                throw new ArgumentException($"'{nameof(processName)}' cannot be null or whitespace.", nameof(processName));

            if (processResolver is null)
                throw new ArgumentNullException(nameof(processResolver));

            if (string.IsNullOrWhiteSpace(dllPath))
                throw new ArgumentException($"'{nameof(dllPath)}' cannot be null or whitespace.", nameof(dllPath));

            var processes = Process.GetProcessesByName(processName);
            var process = processResolver(processes);

            if (process == null)
                throw new InvalidOperationException($"Could not retrieve process by the given process resolver function.");

            return Inject(
                process,
                dllPath);
        }

        public static IntPtr Inject(
            int processId,
            string dllPath)
        {
            if (processId < 0)
                throw new ArgumentException($"'{nameof(processId)}' cannot be negative.", nameof(processId));

            if (string.IsNullOrWhiteSpace(dllPath))
                throw new ArgumentException($"'{nameof(dllPath)}' cannot be null or whitespace.", nameof(dllPath));

            var process = Process.GetProcessById(processId);

            if (process == null)
                throw new InvalidOperationException($"Could not retrieve process by the given id '{processId}'.");

            return Inject(
                process,
                dllPath);
        }

        public static IntPtr Inject(
            Process process,
            string dllPath)
        {
            if (process is null)
                throw new ArgumentNullException(nameof(process));

            if (string.IsNullOrWhiteSpace(dllPath))
                throw new ArgumentException($"'{nameof(dllPath)}' cannot be null or whitespace.", nameof(dllPath));

            dllPath = NormalizeDllPath(dllPath);

            var hProcess = Kernel32.OpenProcess(
                DESIRED_PROCESS_ACCESS,
                false,
                (uint)process.Id);

            if (hProcess.IsDefault())
                throw new Win32Exception($"Failed to retrieve handle for process '{process.ProcessName}'!");

            var dwSize = (uint)(dllPath.Length + 1);

            var pAllocatedRegion = Kernel32.VirtualAllocEx(
                hProcess,
                IntPtr.Zero,
                dwSize,
                MemoryAllocationType.MEM_COMMIT | MemoryAllocationType.MEM_RESERVE,
                MemoryProtection.PAGE_READWRITE);

            if (pAllocatedRegion.IsDefault())
                throw new Win32Exception($"Failed to allocate memory region in the target process!");

            var lpBuffer = Encoding.Default.GetBytes(dllPath);

            if (!Kernel32.WriteProcessMemory(hProcess, pAllocatedRegion, lpBuffer, dwSize, out int bytesWritten))
                throw new Win32Exception($"Failed to write process memory in the target process!");

            var Kernel32Handle = Kernel32.GetModuleHandle(Lib.Kernel32);
            var LoadLibraryProcedure = Kernel32.GetProcAddress(
                Kernel32Handle,
                "LoadLibraryA");

            var hThread = Kernel32.CreateRemoteThread(
                hProcess,
                LoadLibraryProcedure,
                pAllocatedRegion);

            Kernel32.VirtualFreeEx(
                hProcess,
                IntPtr.Zero,
                dllPath.Length + 1,
                MemoryAllocationType.MEM_RESET);

            Kernel32.CloseHandle(hProcess);

            return hThread;
        }

        private static string NormalizeDllPath(
            string dllPath)
        {
            if (!dllPath.EndsWith(".dll"))
                dllPath = $"{dllPath}.dll";

            return dllPath.ToUpper();
        }
    }
}
