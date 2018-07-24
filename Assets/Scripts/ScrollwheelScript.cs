using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollwheelScript : MonoBehaviour {
	public float RotationSpeed = 1f;

	// Use this for initialization
	void Start () {
		
	}

	public void Scroll(float Dist){
		transform.Rotate (0f,RotationSpeed*Dist, 0f, Space.Self);
	}
}
