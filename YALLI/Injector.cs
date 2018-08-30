using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using YALLI.Extensions;
using YALLI.Win32;
using YALLI.Win32.Flags;

namespace YALLI
{
    public static class Injector
    {
        private const ProcessAccess DESIRED_PROCESS_ACCESS =
            ProcessAccess.PROCESS_CREATE_THREAD |
            ProcessAccess.PROCESS_QUERY_INFORMATION |
            ProcessAccess.PROCESS_VM_OPERATION |
            ProcessAccess.PROCESS_VM_READ |
            ProcessAccess.PROCESS_VM_WRITE;

        private static readonly IntPtr _loadLibraryProc;
        private static readonly IntPtr _freeLibraryProc;
        private static readonly int _charMarshalSize;

        static Injector()
        {
            var kernel32ModuleHandle = Kernel32.GetModuleHandle(
                "kernel32.dll");

            _loadLibraryProc = Kernel32.GetProcAddress(
                kernel32ModuleHandle,
                "LoadLibraryA");

            _freeLibraryProc = Kernel32.GetProcAddress(
                kernel32ModuleHandle,
                "FreeLibrary");

            _charMarshalSize = Marshal.SizeOf<char>();
        }

        public static IntPtr LoadModule(
            Process targetProcess,
            string moduleName)
        {
            Argument.IsNotNull(
                targetProcess,
                nameof(targetProcess));

            moduleName = NormalizeModuleName(
                moduleName);

            Argument.IsNotNullOrWhitespace(
                moduleName,
                nameof(moduleName));

            var processHandle = GetProcessHandle(targetProcess);

            if (processHandle.IsNULL())
                return IntPtr.Zero;

            var pArg = LoadArgument(
                processHandle,
                moduleName);

            if (pArg.IsNULL())
                return IntPtr.Zero;

            return Kernel32
                .CreateRemoteThreadWrapped(processHandle, _loadLibraryProc, pArg);
        }

        public static bool UnloadModule(
            Process targetProcess,
            string moduleName)
        {
            throw new NotImplementedException();

            Argument.IsNotNull(
                targetProcess,
                nameof(targetProcess));

            moduleName = NormalizeModuleName(
                moduleName);

            Argument.IsNotNullOrWhitespace(
                moduleName,
                nameof(moduleName));

            return true;
        }

        private static IntPtr LoadArgument(
            IntPtr processHandle,
            string argument)
        {
            try
            {
                var dwSize = new IntPtr(((argument.Length + 1) * _charMarshalSize));
                var flAllocationType = AllocationType.Commit | AllocationType.Reserve;

                var pAlloc = Kernel32.VirtualAllocEx(
                    processHandle,
                    IntPtr.Zero,
                    dwSize,
                    flAllocationType,
                    MemoryProtection.ReadWrite);

                if (pAlloc.IsNULL())
                    return IntPtr.Zero;

                byte[] buffer = Encoding.UTF8.GetBytes(argument);

                Kernel32.WriteProcessMemory(
                    processHandle,
                    pAlloc,
                    buffer,
                    dwSize,
                    out int bytesWritten);

                return pAlloc;
            }
            catch { }

            return IntPtr.Zero;
        }

        private static string NormalizeModuleName(
            string moduleName)
        {
            if (string.IsNullOrWhiteSpace(moduleName))
                return moduleName;

            if (!moduleName.EndsWith(".dll"))
                moduleName = $"{moduleName}.dll";

            return moduleName;
        }

        private static IntPtr GetProcessHandle(
            Process process)
        {
            return Kernel32.OpenProcess(
                DESIRED_PROCESS_ACCESS,
                false,
                process.Id);
        }
    }
}
