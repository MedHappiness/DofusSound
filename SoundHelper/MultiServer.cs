using System.Net.Sockets;

public class MultiServer
{
    #region Déclaration
    private SimpleServer m_server;
    private MultiClient m_client;
    private string m_servertype;
    #endregion
    ushort m_port;
    public ushort Port
    {
        get
        {
            return m_port;
        }
        private set
        {
            m_port = value;
        }
    }
    public MultiServer(string type)
    {
        m_server = new SimpleServer();
        m_servertype = type;
    }

    public void StartAuthentificate()
    {
        m_port = (ushort)NetworkHelper.FindFreePort(64000, 65000);
        m_server.Start(m_port);
        m_server.ConnectionAccepted += AccepteClient;
    }

    #region Socket auth
    private void AccepteClient(Socket client)
    {
        var newClient = new SimpleClient(client);
        m_client = new MultiClient(newClient, m_servertype);
    }
    private void ClientDisconnected(object sender, MultiClient.DisconnectedArgs e)
    {
        m_client = null;
    }
    #endregion

    public MultiClient Client
    {
        get { return m_client; }
    }
}
