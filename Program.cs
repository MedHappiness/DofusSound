
using System.Diagnostics;

string exePath = @"D:\Desktop 2.0\Argus Beta\Dofus.exe";
string RegExeFolder = @"D:\Desktop 2.0\Argus Beta\reg\Reg.exe";
try
{
    if (!SoundMain.Initialized)
        SoundMain.Init(); // on démarre le moteur de son

        Process.Start(RegExeFolder, $"--reg-engine-port={SoundMain.regServer.Port}");
}
catch
{
    Console.WriteLine("Erreur lors du lancement de Reg.exe. Les sons ne seront pas activées");
}

while(true)
{
Process.Start(exePath, $"--lang=fr --update-server-port={SoundMain.upServer.Port} --updater_version=v2 --reg-client-port={SoundMain.gameServer.Port}");
//Process.Start(RegExeFolder, $"--reg-engine-port={SoundMain.regServer.Port}");

Console.ReadLine();
} // on attend que le programme se fermes pour fermer le serveur
