
using System.Diagnostics;

string exePath = @"D:\Desktop 2.0\Argus Beta\Dofus.exe";
string RegExeFolder = @"D:\Desktop 2.0\Argus Beta\reg\Reg.exe";

SoundMain.Init(); // on démarre le moteur de son

Process.Start(RegExeFolder, $"--reg-engine-port={SoundMain.regServer.Port}");

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
} // on attend que le programme se fermes pour fermer le serveur

void StartNewDofusInstance()
{
    Process.Start(exePath, $"--lang=fr --update-server-port={SoundMain.upServer.Port} --updater_version=v2 --reg-client-port={SoundMain.gameServer.Port}");
}
