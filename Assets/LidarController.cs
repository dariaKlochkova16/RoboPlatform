using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LidarController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public float SendRay()
	{
		Transform transform = GetComponent<Transform>();
		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit hit;
		Physics.SphereCast (ray, 0.75f, out hit);

		return hit.distance;
 	}

	public void Rotate(float angle){
		
		Transform transform = GetComponent<Transform>();
		transform.Rotate (0,angle, 0);
	}

}
