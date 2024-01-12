using System.Net.NetworkInformation;

public class NetworkHelper
{
    public static int FindFreePort(int min, int max)
    {
        int num = new Random().Next(min, max);
        TcpConnectionInformation[] activeTcpConnections = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpConnections();
        Func<TcpConnectionInformation, bool> predicate = null;
        for (int i = num; i < max; i++)
        {
            if (predicate == null)
            {
                predicate = x => x.LocalEndPoint.Port != i;
            }
            if (activeTcpConnections.All<TcpConnectionInformation>(predicate))
            {
                return i;
            }
        }
        Func<TcpConnectionInformation, bool> func2 = null;
        for (int i = min; i < num; i++)
        {
            if (func2 == null)
            {
                func2 = x => x.LocalEndPoint.Port != i;
            }
            if (activeTcpConnections.All<TcpConnectionInformation>(func2))
            {
                return i;
            }
        }
        throw new Exception(string.Format("No free port available in range {0}-{1}", min, max));
    }
}
