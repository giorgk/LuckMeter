using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjScript : MonoBehaviour {

	public float sensitivity = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RotateAroundY(Vector3 center, Vector3 oldpos, Vector3 newpos){
		float dx = oldpos.x - newpos.x;
		transform.RotateAround (center, Vector3.up, sensitivity * dx);
	}

	public void RotateAroundY(Vector3 center, float dx){
		transform.RotateAround (center, Vector3.up, sensitivity * dx);
	}
}
