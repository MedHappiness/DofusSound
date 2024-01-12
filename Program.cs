using System;
using System.Diagnostics;

class Program
{
    static string dofusPath;
    static string RegPath;
    static void Main(string[] args)
    {
        string argPath = @"D:\Desktop 2.0\Argus Beta";
        if (args.Length > 0)
        {
            string appPath = args[0];
            Console.WriteLine($"Startup app path received: {appPath}");

            if (!System.IO.File.Exists(appPath + @"\Dofus.exe"))
            {
                Console.WriteLine("Dofus.exe not found in the startup path.");
                return;
            }

            if (!System.IO.File.Exists(appPath + @"\reg\Reg.exe"))
            {
                Console.WriteLine("Reg.exe not found in the startup path.");
                return;
            }
            
            dofusPath = appPath + @"\Dofus.exe";
            RegPath = appPath + @"\reg\Reg.exe";

            SoundMain.Init(); // Start the sound engine

            Process.Start(RegPath, $"--reg-engine-port={SoundMain.regServer.Port}");

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
        Process.Start(dofusPath, $"--lang=fr --update-server-port={SoundMain.upServer.Port} --updater_version=v2 --reg-client-port={SoundMain.gameServer.Port}");
    }
}
