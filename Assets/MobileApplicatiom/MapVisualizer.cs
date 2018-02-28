using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapVisualizer : MonoBehaviour {

	public int scale = 20;

	public int width = 200;
	public int hight = 200;

	public Texture2D texture;

	public float[] map;

	private int Step;

	public float Y;
	public float X;

	void Start ()
	{
		//texture = new Texture2D(width,hight);

	}

	public void CreateMap(float[] _map){

		map = new float[360];

		for (int i = 0; i < 360; i++) {
			map [i] = 4;
		}
		texture = new Texture2D(width,hight);

		Y = hight / 2;
		X = width / 2;
		
		//map = _map;
	
		float angle = 360 / map.Length;

		texture.SetPixel ((int)X, (int)Y, Color.red);

		for (int i = 0; i < map.Length; i++) {
			float x =  map [i] * scale * Mathf.Cos (i * angle);
			float y = map [i] * scale * Mathf.Sin (i * angle);

			int x1 = (int)(X + x);
			int y1 = (int)(Y + y);
			texture.SetPixel (x1, y1, Color.black);
		}

		texture.Apply ();
	}

}
