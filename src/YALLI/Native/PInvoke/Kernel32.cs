using System;
using System.Runtime.InteropServices;

using YALLI.Native.Enumerations;

namespace YALLI.Native.PInvoke
{
    internal static class Kernel32
    {
        [DllImport(Lib.Kernel32)]
        internal static extern IntPtr OpenProcess(
            ProcessAccessFlags dwDesiredAccess,
            bool bInheritHandle,
            uint dwProcessId);

        [DllImport(Lib.Kernel32, CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        internal static extern IntPtr GetProcAddress(
            IntPtr hModule,
            string lpProcName);

        [DllImport(Lib.Kernel32, CharSet = CharSet.Unicode)]
        internal static extern IntPtr GetModuleHandle(
            string lpModuleName);

        [DllImport(Lib.Kernel32)]
        internal static extern IntPtr VirtualAllocEx(
            IntPtr hProcess,
            IntPtr lpAddress,
            uint dwSize,
            MemoryAllocationType flAllocationType,
            MemoryProtection flProtect);

        [DllImport(Lib.Kernel32)]
        internal static extern bool WriteProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            byte[] lpBuffer,
            uint nSize,
            out int lpNumberOfBytesWritten);
        
        internal static IntPtr CreateRemoteThread(
            IntPtr hProcess,
            IntPtr lpStartAddress,
            IntPtr lpParameter)
        {
            return CreateRemoteThread(
                hProcess,
                IntPtr.Zero,
                0,
                lpStartAddress,
                lpParameter,
                0,
                out _);
        }

        [DllImport(Lib.Kernel32, SetLastError = true)]
        internal static extern uint WaitForSingleObject(
            IntPtr hHandle, 
            uint dwMilliseconds);

        [DllImport(Lib.Kernel32)]
        internal static extern bool GetExitCodeThread(
            IntPtr hThread, 
            out uint lpExitCode);

        [DllImport(Lib.Kernel32)]
        internal static extern void VirtualFreeEx(
            IntPtr hProcess, 
            IntPtr lpAddress, 
            int dwSize, 
            MemoryAllocationType dwFreeType);

        [DllImport(Lib.Kernel32)]
        internal static extern bool CloseHandle(
            IntPtr hObject);

        [DllImport(Lib.Kernel32)]
        private static extern IntPtr CreateRemoteThread(
            IntPtr hProcess,
            IntPtr lpThreadAttributes,
            uint dwStackSize,
            IntPtr lpStartAddress,
            IntPtr lpParameter,
            uint dwCreationFlags,
            out IntPtr lpThreadId);
    }
}
