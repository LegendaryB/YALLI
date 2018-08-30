using LoadLibraryDLLInjector.Win32.Flags;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace LoadLibraryDLLInjector.Win32
{
    internal static class Kernel32
    {
        [DllImport("kernel32")]
        private static extern IntPtr OpenProcess(
            ProcessAccess dwDesiredAccess,
            bool bInheritHandle,
            int dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(
            IntPtr hObject);
    }
}
