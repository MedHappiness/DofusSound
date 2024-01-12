public class MultiClient
{
    #region Déclaration
    private SimpleClient m_client;
    private string m_servertype;

    public event EventHandler<DisconnectedArgs> Disconnected;
    #endregion

    public MultiClient(SimpleClient client, string type)
    {
        m_client = client;
        m_servertype = type;

        if (client != null)
        {
            m_client.DataReceived += this.ClientDataReceive;
            m_client.Disconnected += this.ClientDisconnected;
        }
    }

    /// <summary>
    /// Permet de déconnecter le client
    /// </summary>
    public void Dipose()
    {
        m_client.DataReceived -= ClientDataReceive;
        m_client.Disconnected -= this.ClientDisconnected;

        m_client.Stop();
    }

    #region Events
    private void ClientDataReceive(object sender, SimpleClient.DataReceivedEventArgs e)
    {
        SoundMain.SendServerData(e.Data, m_servertype);
    }

    private void ClientDisconnected(object sender, SimpleClient.DisconnectedEventArgs e)
    {
        OnDisconnected(new DisconnectedArgs(this));
    }
    private void OnDisconnected(DisconnectedArgs e)
    {
        if (Disconnected != null)
            Disconnected(this, e);
    }
    #endregion
    public class DisconnectedArgs : EventArgs
    {
        public MultiClient Host { get; private set; }

        public DisconnectedArgs(MultiClient host)
        {
            Host = host;
        }
    }

    public void Send(byte[] data)
    {
        if (m_client.Runing)
            m_client.Send(data);
    }
}