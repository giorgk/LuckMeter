//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createBarrelColliderScript : MonoBehaviour {

	public GameObject primCollider;
	public GameObject BottomCollider;
	public GameDataScriptableObj gamedata;

	TopLidScript TopLid;

	// Use this for initialization
	void Awake () {
		Debug.Log (System.DateTime.Now);
		// create cyllinder collider
		if (primCollider != null && BottomCollider != null) {
			float dAngle = 360f / gamedata.ColliderSides;
			float angle = 0f;
			for (int i = 0; i < gamedata.ColliderSides; i++) {
				if (i == gamedata.ColliderSides / 2) {
					GameObject test = (GameObject)Instantiate (BottomCollider, transform);
					test.name = "BottomLid";
					test.transform.position = new Vector3 (0f, - gamedata.radius, 0f);
				} else {

					GameObject test = (GameObject)Instantiate (primCollider, transform); // the second argument make the object child
					test.name = "CLDR_" + i.ToString ();
					test.transform.localScale = gamedata.PrimitiveColliderScale;
					test.transform.position = new Vector3 (0f, gamedata.radius, 0f);
					test.transform.RotateAround (Vector3.zero, Vector3.forward, angle);
					if (i == 0) {
						test.AddComponent<TopLidScript> ();
						TopLid = test.GetComponent<TopLidScript> ();
						test.name = "TopLid";
					}
				}
				angle += dAngle;
			}
		}

		// create walls
		createChildWall(new Vector3 (0f, 0f, gamedata.PrimitiveColliderScale.z/2f), new Vector3 (10f, 10f, 0.5f), "WALL01");
		createChildWall (new Vector3 (0f, 0f, -gamedata.PrimitiveColliderScale.z / 2f), new Vector3 (10f, 10f, 0.5f), "WALL02");

		// create the column on the top of the rotator
		createChildWall (new Vector3 (0f, gamedata.radius + gamedata.TopColumnHeight/2, -gamedata.PrimitiveColliderScale.z / 2f), new Vector3 (gamedata.PrimitiveColliderScale.x, gamedata.TopColumnHeight, 0.5f), "TopWall01");
		createChildWall (new Vector3 (0f, gamedata.radius + gamedata.TopColumnHeight/2, gamedata.PrimitiveColliderScale.z / 2f), new Vector3 (gamedata.PrimitiveColliderScale.x, gamedata.TopColumnHeight, 0.5f), "TopWall02");
		createChildWall (new Vector3 (gamedata.PrimitiveColliderScale.x / 2f, gamedata.radius + gamedata.TopColumnHeight/2, 0f), new Vector3 (0.5f, gamedata.TopColumnHeight, gamedata.PrimitiveColliderScale.z), "TopWall03");
		createChildWall (new Vector3 (-gamedata.PrimitiveColliderScale.x / 2f, gamedata.radius + gamedata.TopColumnHeight/2, 0f), new Vector3 (0.5f, gamedata.TopColumnHeight, gamedata.PrimitiveColliderScale.z), "TopWall04");

		// create the pickholder underneath
		createDetailsBellow();


	}


	void createChildWall(Vector3 pos, Vector3 Scl, string name){
		GameObject tmp = (GameObject)Instantiate (primCollider, transform);
		tmp.name = name;
		tmp.transform.position = pos;
		tmp.transform.localScale = Scl;
	}
		

	void createDetailsBellow(){
		float offset = 0.5f;
		// This is the Bottom cap
		GameObject tmp_cap = Instantiate (primCollider, transform);
		tmp_cap.transform.position = new Vector3 (0f, -gamedata.radius - offset, 0f);
		tmp_cap.transform.localScale = new Vector3 (2f, 0.5f, 2f);
		tmp_cap.name = "BottomCap";

		// This is the trigger zone
		GameObject tmp_trigger = Instantiate (primCollider, transform);
		tmp_trigger.transform.position = new Vector3 (0f, -gamedata.radius - offset, 0f);
		tmp_trigger.transform.localScale = new Vector3 (2f, 0.5f, 2f);
		tmp_trigger.GetComponent<Collider> ().isTrigger = true;
		tmp_trigger.name = "trigger_zone";
		tmp_trigger.AddComponent<TriggerZoneScript> ();
	}
}
