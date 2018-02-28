using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;

	public Image image;

	private GameObject lidar;

	public RenderTexture texture;

	private LidarController lidarController;

	private Queue<byte[]> messages = new Queue<byte[]>();

	void Start ()
	{
		//rb = GetComponent<Rigidbody>();
		//transform = GetComponent<Transform>();

		lidar = transform.Find ("Lidar").gameObject;

		lidarController = lidar.GetComponent<LidarController> ();

		ConnectionMenager.SetConnectionOptions(6006, 6007);

		ConnectionMenager.RecievedMessage += new EventHandler (RecieveMessage);

		CreateMap ();

	}

	void Update(){
	
		ProcessQueue ();

		if(Input.GetKeyUp(KeyCode.Escape)){

			CreateMap ();
		}


//		Texture2D tex = new Texture2D (texture.width, texture.height, TextureFormat.ARGB32, false);
//		Texture2D oldTex = (Texture2D)texture;
//		tex.SetPixels(oldTex.GetPixels());

		Texture2D tex = GetRTPixels (texture);

 		VideoMessage message = new VideoMessage ();
		message.texture = tex.EncodeToPNG();
		var mes = message.Serialize ();

		var mes2 = Message.Deserialize (mes);

		if (message == mes2) {
			Debug.Log ("ok");
		}

		Debug.Log(tex.GetPixel(30,30));

		ConnectionMenager.Instanse.Send(mes);

		//////////////////
//
//		var mm = message.Serialize ();
//
//		var mm2 = Message.Deserialize (mm);
//
//		var mes = (mm2 as VideoMessage);
//		var tx = mes.texture;
//		var tx2 = new Texture2D (80,60);
//	
//		tx2.LoadImage (tx);
//		tx2.Apply ();
//
//		Sprite mySprite = Sprite.Create (tx2, new Rect (0.0f, 0.0f, tx2.width, tx2.height), new Vector2 (0.5f, 0.5f), 100.0f);
//
//		image.sprite = mySprite;

	}

	public Texture2D GetRTPixels(RenderTexture rt)
	{
		RenderTexture currentActiveRT = RenderTexture.active;

		RenderTexture.active = rt;

		Texture2D tex = new Texture2D(rt.width, rt.height);
		tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);

		RenderTexture.active = currentActiveRT;
		return tex;
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		transform.position += movement * speed;
	}

	void RecieveMessage(object sender, EventArgs e)
	{
		MessageEventArgs eventArgs = e as MessageEventArgs;

		messages.Enqueue(eventArgs.message);
	}

	void CreateMap()
	{
		MapCreator mapCreator = GetComponent<MapCreator> ();

		float angle = 1.0f;
		mapCreator.CreateMap (lidarController,ref angle);

		float[] map = mapCreator.map;

		byte[] mess = BitConverter.GetBytes(map[0]);

		MapMessage mapMessage = new MapMessage ();
		mapMessage.map = mess;

		ConnectionMenager.Instanse.Send (mapMessage.Serialize());
	}

	void ProcessQueue(){
		if (messages.Count > 0) {
			byte[] ms = messages.Peek ();
			Message message = Message.Deserialize(ms);

			if (message is MotionMessage) {
				MotionMessage motionMessage = message as MotionMessage;
				Vector3 movement = new Vector3 (0.0f, 0.0f, 0.0f);

				switch (motionMessage.motionType) {
				case MotionType.Right:
					movement = new Vector3 (3.0f, 0.0f, 0.0f);
					break;
				case MotionType.Left:
					movement = new Vector3 (-3.0f, 0.0f, 0.0f);
					break;
				case MotionType.Forward:
					movement = new Vector3 (0.0f, 0.0f, 3.0f);
					break;
				case MotionType.Backward:
					movement = new Vector3 (0.0f, 0.0f, -3.0f);
					break;
				}
				transform.position += movement;
			}

			messages.Dequeue ();
		}
	}
}
