using YALLI;

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace InjectDll
{
    class Program
    {
        private const string TargetProcessName = "notepad";

        private static void Main(string[] args)
        {
            var dllPath = Path.GetFullPath(
                Path.Combine(
                    Environment.CurrentDirectory,
                    @"..\..\..\..\x64\Debug\NativeDll.dll"));

            var process = Process
                .GetProcessesByName(TargetProcessName)
                .First();

            IntPtr moduleAddr = Injector.LoadModule(
                process,
                dllPath);

            Console.WriteLine($"Module is loaded at: {moduleAddr}");
            Console.ReadLine();
        }
    }
}
