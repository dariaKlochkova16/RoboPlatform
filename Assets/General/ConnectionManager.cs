using Assets.General;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public struct ConnectionOptions
{
    public IPAddress IpAddress;
    public int RemotePort;
    public int LocalPort;
}

public class ConnectionManager
{
    private static ConnectionManager instance;
    public event EventHandler<MessageEventArgs> RecievedMessage;
    private ConnectionOptions connectionOptions;

    private UdpClient sender;
    private UdpClient _reciveUdpClient;

    private Thread _reciveThread;

    public static ConnectionManager Instanse
    {
        get
        {
            if (instance == null)
                instance = new ConnectionManager();

            return instance;
        }
    }

    private ConnectionManager()
    {
        Debug.Log("ConnectionManager");
    }

    public void Receive(object sender, MessageEventArgs e)
    {
        RecievedMessage(sender, e);
    }

    public void Terminate()
    {
        _reciveThread.Abort();
        sender.Close();

        if (_reciveUdpClient != null)
            _reciveUdpClient.Close();

        instance = null;
    }

    public void SetConnectionOptions(int _remotePort, int _localPort, string host = "localhost")
    {
        connectionOptions.RemotePort = _remotePort;
        connectionOptions.LocalPort = _localPort;
        connectionOptions.IpAddress = IPAddress.Parse("127.0.0.1");

        _reciveUdpClient = new UdpClient(connectionOptions.LocalPort);
        sender = new UdpClient();

        _reciveThread = new Thread(Receiver);
        _reciveThread.Start();

    }

    public void Send(byte[] bytes)
    {
        sender.Connect(connectionOptions.IpAddress, connectionOptions.RemotePort);
        sender.Send(bytes, bytes.Length);
    }

    public void Receiver()
    {
        try
        {
            while (true)
            {
                IPEndPoint RemoteIpEndPoint = null;
                var recievData = _reciveUdpClient.Receive(ref RemoteIpEndPoint);

                if (recievData != null)
                {
                    MessageEventArgs eventArgs = new MessageEventArgs();
                    eventArgs.message = recievData;
                    RecievedMessage(this, eventArgs);
                }
            }
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Debug.Log("Некорректный номер порта");
        }
        catch (SocketException ex)
        {
            Debug.Log("Порт уже используется");
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}


