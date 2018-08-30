using System;
using System.Diagnostics;
using YALLI.Win32;
using YALLI.Win32.Flags;

namespace YALLI.Extensions
{
    public static class ProcessExtensions
    {
        internal static IntPtr OpenProcess(
            this Process process,
            ProcessAccess processAccess)
        {
            return Kernel32.OpenProcess(
                processAccess,
                false,
                process.Id);
        }

        public static IntPtr LoadModule(
            this Process process,
            string moduleName)
        {
            Argument.IsNotNullOrWhitespace(
                moduleName,
                nameof(moduleName));

            var hProcess = OpenProcess(
                process,
                ProcessAccess.PROCESS_CREATE_THREAD |
                ProcessAccess.PROCESS_QUERY_INFORMATION |
                ProcessAccess.PROCESS_VM_OPERATION |
                ProcessAccess.PROCESS_VM_READ |
                ProcessAccess.PROCESS_VM_WRITE);

            return ProcessModulesHandler.LoadModule(
                hProcess,
                moduleName);
        }

        public static bool UnloadModule(
            this Process process,
            string moduleName)
        {
            Argument.IsNotNullOrWhitespace(
                moduleName,
                nameof(moduleName));

            var hProcess = OpenProcess(
                process,
                ProcessAccess.PROCESS_ALL_ACCESS);

            return ProcessModulesHandler.UnloadModule(
                hProcess,
                moduleName);
        }
    }
}
