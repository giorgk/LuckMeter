using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

	int id;
	Texture2D myTexture;

	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < -10)
			Destroy (this);
	}

	public void SetID(int id_in){
		if (id_in > 49 || id_in < 1) {
			id_in = 1;
		}
		//id_in = 2;
		myTexture = Resources.Load ("ball_mask_" + id_in.ToString (), typeof(Texture2D)) as Texture2D;
		if (myTexture) {
			id = id_in;
			GetComponent<Renderer> ().material.SetTexture("_Mask", myTexture);
		} else {
			Debug.Log ("Didnt find the testure: " + "Resources/ball_mask_" + id_in.ToString () );
		}

	}

	public int getId(){
		return id;
	}


}
