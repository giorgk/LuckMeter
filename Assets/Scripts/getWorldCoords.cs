using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getWorldCoords : MonoBehaviour {

	// Use this for initialization
	void Start () {
		MyTools.getBoxColliderCoords (GetComponent<BoxCollider> ());	
	}
}
