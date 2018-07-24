using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageBulbsScript : MonoBehaviour {

	List<BulbLightScript> bulbs = new List<BulbLightScript>();
	// Use this for initialization
	void Awake () {
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild (i).transform.GetChild(0).GetComponent<BulbLightScript> ().idBulb = i;
			bulbs.Add(transform.GetChild (i).transform.GetChild(0).GetComponent<BulbLightScript> ());
		}
		//Debug.Log ("N CHilds: " + bulbs.Count.ToString ());
	}

	public void AnimateLights(){
		foreach (BulbLightScript b in bulbs) {
			b.AnimateLight ();
		}
	}

	public void StopLights(){
		foreach (BulbLightScript b in bulbs) {
			b.StopAnimatingLIght ();
		}
	}


}
