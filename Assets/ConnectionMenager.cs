using System;
using System.Net;
using System.Threading;


public struct ConnectionOptions{

	public  IPAddress IpAddress;

	public int RemotePort;

	public int LocalPort;
}

public class ConnectionMenager
{
	private static ConnectionMenager instance;

	private ObserverServerEvents observerServerEvents;

	public static event EventHandler RecievedMessage;

	private static ConnectionOptions connectionOptions;

	private static System.Object lockThis = new System.Object();

	public static ConnectionMenager Instanse {
		get {
			lock (lockThis) {
				
				if (instance == null ) {
					instance = new ConnectionMenager ();
				}
				return instance;
			}
		}
	}
		 

	private ConnectionMenager ()
	{
		observerServerEvents = new ObserverServerEvents (connectionOptions);
		observerServerEvents.RecievedMessage += new EventHandler (Receive);

		Thread thread = new Thread (observerServerEvents.Receiver);
		thread.Start();
	}

	public void Send (byte[] bytes)
	{
		observerServerEvents.Send (bytes);
	}

	public void Receive(object sender, EventArgs e)
	{
		RecievedMessage (sender, e);
	}

	public static void SetConnectionOptions(int _remotePort, int _localPort, string host = "localhost")
	{
		connectionOptions.RemotePort = _remotePort;
		connectionOptions.LocalPort = _localPort;
		connectionOptions.IpAddress = IPAddress.Parse("127.0.0.1");//TODO
	}


}


