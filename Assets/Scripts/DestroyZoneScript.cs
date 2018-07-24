using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZoneScript : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		Debug.Log ("The " + other.name + " is lost in the oblivion");
		Destroy (other.gameObject);
	}
}
