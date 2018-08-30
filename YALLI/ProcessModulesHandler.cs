using System;
using System.Diagnostics;

namespace YALLI
{
    internal static class ProcessModulesHandler
    {
        internal static IntPtr LoadModule(
            Process process,
            string moduleName)
        {

            return IntPtr.Zero;
        }

        internal static bool UnloadModule(
            Process process,
            string moduleName)
        {

            return true;
        }
    }
}
