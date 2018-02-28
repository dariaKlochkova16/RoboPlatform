using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class ObserverServerEvents
{
	public event EventHandler RecievedMessage;

	public IPEndPoint ipEndPoint;

	private ConnectionOptions connectionOptions;

	public ObserverServerEvents(ConnectionOptions _connectionOptions)
	{
		connectionOptions = _connectionOptions;

		ipEndPoint = new IPEndPoint (connectionOptions.IpAddress, connectionOptions.RemotePort);
	}

	public void Send (byte[] bytes)
	{
		UdpClient sender = new UdpClient ();
		sender.Connect (connectionOptions.IpAddress, connectionOptions.RemotePort);
		Debug.Log (bytes.Length);
		sender.Send (bytes, bytes.Length);
		sender.Close ();
	}

	public void Receiver ()
	{
		try {
			UdpClient receivingUdpClient = new UdpClient (connectionOptions.LocalPort);

			while (true) {
				IPEndPoint RemoteIpEndPoint = null;
				var recievData = receivingUdpClient.Receive (ref RemoteIpEndPoint);

				if (recievData != null) {
					MessageEventArgs eventArgs = new MessageEventArgs();
					eventArgs.message = recievData;
					RecievedMessage (this, eventArgs);
				}
			}
		} catch (ArgumentOutOfRangeException ex) {
			Debug.Log ("Некорректный номер порта");
		} catch (SocketException ex) {
			Debug.Log ("Порт уже используется");
		}

	}
}

	

