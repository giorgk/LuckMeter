using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBottomScript : MonoBehaviour {

	public List<Transform> lids = new List<Transform> ();
	public float openDist = 0.6f;
	sweeperMainScript swp;

	List<Vector3> initPos = new List<Vector3>();

	// Use this for initialization
	void Start () {
		foreach (Transform trnsf in lids) {
			initPos.Add (trnsf.localPosition);
		}
		swp = GameObject.Find ("SweepersMain").GetComponent<sweeperMainScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void openLid(){
		//Debug.Log ("Open Lid");
		foreach (Transform trnsf in lids) {
			trnsf.localPosition = 1.2f*trnsf.localPosition;
		}
		StartCoroutine (changespeedRoutine (90f, 30f, 5f));
	}

	public void closeLid(){
		int i = 0;
		foreach (Transform trnsf in lids) {
			trnsf.localPosition = initPos [i];// 0.8333333333f*trnsf.localPosition;
			i++;
		}
		StartCoroutine (changespeedRoutine (30f, 90f, 5f));
	}


	IEnumerator changespeedRoutine(float sr, float er, float t){
		float tt = 0f;

		while (tt < t) {
			tt += Time.deltaTime;
			swp.SetRotationSpeed (Mathf.Lerp (sr, er, tt / t));
			yield return null;
		}
	}

}
