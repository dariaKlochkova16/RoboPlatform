using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class InputImageController : MonoBehaviour {

	private Queue<byte[]> messages = new Queue<byte[]>();

	public Image image;
		
	void Update () {
		if (messages.Count > 0) {
			var ms = messages.Dequeue();

			var message = Message.Deserialize (ms);

			if (message is VideoMessage) {
				
				var videoMessage = message as VideoMessage;

				var tex2 = videoMessage.texture;

				var tex = new Texture2D (80,60);
					
				tex.LoadImage (tex2);
				tex.Apply ();

				Debug.Log(tex.GetPixel(30,30));

				Sprite mySprite = Sprite.Create (tex, new Rect (0.0f, 0.0f, tex.width, tex.height), new Vector2 (0.5f, 0.5f), 100.0f);

				image.sprite = mySprite;
			}
		}
	}
}
