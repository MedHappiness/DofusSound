public class SoundMain
{
    public static MultiServer upServer = new MultiServer("up");
    public static MultiServer gameServer = new MultiServer("game");
    public static MultiServer regServer = new MultiServer("reg");
    public static bool Initialized = false;

    public static void Init()
    {
        upServer.StartAuthentificate();
        gameServer.StartAuthentificate();
        regServer.StartAuthentificate();
        Initialized = true;
    }

    public static void SendServerData(byte[] data, string type)
    {
        switch (type)
        {
            case "up":
                upServer.Client.Send(data);
                break;
            case "game":
                regServer.Client.Send(data);
                break;
            case "reg":
                gameServer.Client.Send(data);
                break;
        }
    }
}
