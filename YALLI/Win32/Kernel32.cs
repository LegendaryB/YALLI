using System;
using System.Runtime.InteropServices;
using YALLI.Win32.Flags;

namespace YALLI.Win32
{
    internal static class Kernel32
    {
        [DllImport("kernel32")]
        internal static extern IntPtr OpenProcess(
            ProcessAccess dwDesiredAccess,
            bool bInheritHandle,
            int dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(
            IntPtr hObject);
    }
}
