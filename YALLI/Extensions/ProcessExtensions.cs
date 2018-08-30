using System;
using System.Diagnostics;

namespace YALLI.Extensions
{
    public static class ProcessExtensions
    {
        public static IntPtr LoadModule(
            this Process process,
            string moduleName)
        {
            return Injector.LoadModule(
                process,
                moduleName);
        }

        public static bool UnloadModule(
            this Process process,
            string moduleName)
        {
            return Injector.UnloadModule(
                process,
                moduleName);
        }
    }
}
