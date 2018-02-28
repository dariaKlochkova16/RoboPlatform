using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour {

	public float[] map;


	public void CreateMap(LidarController lidarController,ref float angle){

		map = new float[(int)(360 / angle)];

		for(int i = 0; i < map.Length; i++){
			map[i] = lidarController.SendRay ();
			lidarController.Rotate (angle);
		}
	}
}
