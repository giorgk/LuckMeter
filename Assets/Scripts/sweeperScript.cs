using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sweeperScript : MonoBehaviour {
	[System.NonSerialized]
	public bool bSweep = false;
	[System.NonSerialized]
	public float rotateSpeed = 90;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (bSweep) {
			transform.RotateAround (Vector3.zero, Vector3.forward, rotateSpeed * Time.fixedDeltaTime);
		}
	}
}
