using System;
using System.Diagnostics;
using System.IO;
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
        
        public static IntPtr LoadLibrary(
            this Process process,
            string fileName)
        {
            Argument.IsNotNullOrWhitespace(
                fileName,
                nameof(fileName));

            if (!File.Exists(fileName))
                throw new FileNotFoundException();


            return IntPtr.Zero;
        }

        public static void FreeLibrary(
            this Process process,
            string moduleName)
        {

        }
    }
}
