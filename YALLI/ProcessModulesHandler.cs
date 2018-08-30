using System;
using System.Runtime.InteropServices;
using System.Text;
using YALLI.Win32;
using YALLI.Win32.Flags;
using YALLI.Extensions;

namespace YALLI
{
    internal static class ProcessModulesHandler
    {
        private static readonly IntPtr _loadLibraryProc;
        private static readonly int _charMarshalSize;

        static ProcessModulesHandler()
        {
            var kernel32ModuleHandle = Kernel32.GetModuleHandle(
                "kernel32.dll");

            _loadLibraryProc = Kernel32.GetProcAddress(
                kernel32ModuleHandle,
                "LoadLibraryA");

            _charMarshalSize = Marshal.SizeOf<char>();
        }

        internal static IntPtr LoadModule(
            IntPtr hProcess,
            string lpModuleName)
        {
            var pArg = LoadArgument(
                hProcess, 
                lpModuleName);

            if (pArg.IsNULL())
                return IntPtr.Zero;

            return Kernel32
                .CreateRemoteThread(hProcess, IntPtr.Zero, 0, _loadLibraryProc, pArg, 0, IntPtr.Zero);
        }

        internal static bool UnloadModule(
            IntPtr hProcess,
            string lpModuleName)
        {

            return true;
        }

        private static IntPtr LoadArgument(
            IntPtr hProcess,
            string argument)
        {
            try
            {
                var allocationSize = new IntPtr(((argument.Length + 1) * _charMarshalSize));
                var allocationType = AllocationType.Commit | AllocationType.Reserve;

                var pArg = Kernel32.VirtualAllocEx(
                    hProcess,
                    IntPtr.Zero,
                    allocationSize,
                    allocationType,
                    MemoryProtection.ReadWrite);

                if (pArg.IsNULL())
                    return IntPtr.Zero;

                byte[] buffer = Encoding.UTF8.GetBytes(argument);

                Kernel32.WriteProcessMemory(
                    hProcess,
                    pArg,
                    buffer,
                    allocationSize,
                    out int bytesWritten);

                return pArg;
            }
            catch { }

            return IntPtr.Zero;
        }
    }
}
