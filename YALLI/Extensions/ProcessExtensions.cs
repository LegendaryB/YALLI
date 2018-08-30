using System;
using System.Diagnostics;
using System.IO;
using YALLI.Win32;
using YALLI.Win32.Flags;

namespace YALLI.Extensions
{
    public static class ProcessExtensions
    {
        private static readonly IntPtr LoadLibraryProc;

        static ProcessExtensions()
        {
            var kernel32ModuleHandle = Kernel32.GetModuleHandle(
                "kernel32.dll");

            LoadLibraryProc = Kernel32.GetProcAddress(
                kernel32ModuleHandle,
                "LoadLibraryA");
        }

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

            if (!File.Exists(moduleName))
                throw new FileNotFoundException();

            return ProcessModulesHandler.LoadModule(
                process,
                moduleName);
        }

        public static bool UnloadModule(
            this Process process,
            string moduleName)
        {
            Argument.IsNotNullOrWhitespace(
                moduleName,
                nameof(moduleName));

            return ProcessModulesHandler.UnloadModule(
                process,
                moduleName);
        }
    }
}
