using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderCustomScript : MonoBehaviour {

	public float velocityMagnitude = 2f;

	Vector3 temp;

	void Awake(){
		temp = transform.forward;
		//Debug.Log (temp.ToString ());
	}

	void OnCollisionEnter(Collision collision){
		collision.rigidbody.velocity = -velocityMagnitude * temp;
	}
}
