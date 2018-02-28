using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;

public class MobileApplicationController : MonoBehaviour {

	private Queue<byte[]> messages = new Queue<byte[]>();

	void Start () {
		ConnectionMenager.RecievedMessage += new EventHandler (RecieveMessage);
	}

	void Update () {
		
	}

	void RecieveMessage(object sender, EventArgs e)
	{
		MessageEventArgs eventArgs = e as MessageEventArgs;

		messages.Enqueue(eventArgs.message);
	}
}
