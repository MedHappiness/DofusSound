using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

class Program
{
    static string dofusPath;
    static string RegPath;
    static void Main(string[] args)
    {
        string executableType =
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "exe" :
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? "app" :
            "elf";

        if (args.Length > 0)
        {
            string appPath = args[0];
            Console.WriteLine($"Startup app path received: {appPath}");

            if (!System.IO.File.Exists(appPath + @"/Dofus." + executableType))
            {
                Console.WriteLine($"Dofus.{executableType} not found in the startup path.");
                return;
            }

            if (!System.IO.File.Exists(appPath + @"/reg/Reg." + executableType))
            {
                Console.WriteLine($"Reg.{executableType} not found in the startup path.");
                return;
            }

            dofusPath = appPath + @"/Dofus." + executableType;
            RegPath = appPath + @"/reg/Reg." + executableType;

            SoundMain.Init(); // Start the sound engine

            RunProcess(RegPath, $"--reg-engine-port={SoundMain.regServer.Port}");

            while (true)
            {
                Console.WriteLine("Press D to start a new dofus instance");
                Console.WriteLine("Press Q to quit");

                var key = Console.ReadLine();
                if (key == "D")
                {
                    StartNewDofusInstance();
                }
                else if (key == "Q")
                {
                    break;
                }
            }
        }
        else
        {
            Console.WriteLine("Please provide the startup path as a command-line argument.");
        }
    }

    static void StartNewDofusInstance()
    {
        RunProcess(dofusPath, $"--lang=fr --update-server-port={SoundMain.upServer.Port} --updater_version=v2 --reg-client-port={SoundMain.gameServer.Port}");
    }

    public static void RunProcess(string executablePath, string arguments)
    {
        try
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = executablePath,
                Arguments = $"{arguments}",
                UseShellExecute = false
            };

            using (Process process = new Process { StartInfo = startInfo })
            {
                process.Start();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

}
