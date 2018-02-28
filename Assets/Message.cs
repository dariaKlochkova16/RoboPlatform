using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;

public enum MotionType{
	Forward,
	Backward,
	Left,
	Right
}

[Serializable]
public abstract class Message  {

	public byte[] Serialize()
	{
		MemoryStream memoryStream = new MemoryStream ();
		BinaryFormatter formatter = new BinaryFormatter();

		formatter.Serialize (memoryStream, this);

		return memoryStream.ToArray ();
	}

	public static Message Deserialize(byte[] binaryMessage)
	{
		MemoryStream memoryStream = new MemoryStream (binaryMessage);

		BinaryFormatter formatter = new BinaryFormatter();

		return (Message)formatter.Deserialize(memoryStream);
	}
}

[Serializable]
public class MotionMessage : Message
{
	public MotionType motionType;
}

[Serializable]
public class VideoMessage: Message
{
	public byte[] texture;
}

[Serializable]
public class MapMessage: Message
{
	public byte[] map;
}

public class MessageEventArgs :EventArgs
{
	public byte[] message;
}