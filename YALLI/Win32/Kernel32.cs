using System;
using System.Runtime.InteropServices;
using YALLI.Win32.Flags;

namespace YALLI.Win32
{
    internal static class Kernel32
    {
        [DllImport("kernel32.dll")]
        internal static extern IntPtr OpenProcess(
            ProcessAccess dwDesiredAccess,
            bool bInheritHandle,
            int dwProcessId);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        internal static extern IntPtr GetProcAddress(
            IntPtr hModule,
            string lpProcName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr GetModuleHandle(
            string lpModuleName);

        [DllImport("kernel32.dll")]
        internal static extern IntPtr VirtualAllocEx(
            IntPtr hProcess,
            IntPtr lpAddress,
            IntPtr dwSize,
            AllocationType flAllocationType,
            MemoryProtection flProtect);

        [DllImport("kernel32.dll")]
        internal static extern bool WriteProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            byte[] lpBuffer,
            IntPtr nSize,
            out int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        internal static extern IntPtr CreateRemoteThread(
            IntPtr hProcess,
            IntPtr lpThreadAttributes,
            uint dwStackSize,
            IntPtr lpStartAddress,
            IntPtr lpParameter,
            uint dwCreationFlags,
            IntPtr lpThreadId);

        [DllImport("kernel32.dll")]
        internal static extern bool CloseHandle(
            IntPtr hObject);
    }
}
