using System.CommandLine;
using System.CommandLine.NamingConventionBinder;

namespace YALLI.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PrintBanner();

            var processOption = new Option<string?>(
                new string[] { "--processName", "-pn" },
                "The target process name for the injection.")
            {
                IsRequired = true
            };

            var pathOption = new Option<string?>(
                new string[] { "--dllPath", "-dll" },
                "The path to the *.dll which should be injected.")
            {
                IsRequired = true
            };

            var cmd = new RootCommand
            {
                processOption,
                pathOption
            };

            cmd.Handler = CommandHandler.Create<string, string>(InjectToProcess);

            cmd.Invoke(args);
        }

        private static void InjectToProcess(
            string processName,
            string dllPath)
        {
            var result = Injector.Inject(
                processName,
                dllPath);

            if (result != IntPtr.Zero)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Injection to process '{processName}' succeeded!");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        private static void PrintBanner()
        {
            Console.Write(@"
    __ __ _   _    _    _ 
    \ V // \ | |  | |  | |
     \ /| o || |_ | |_ | |
     |_||_n_||___||___||_|
                      
    Yet another LoadLibrary injector brought to you by LegendaryB
    https://github.com/LegendaryB/YALLI

    =================================================================

");
        }
    }
}