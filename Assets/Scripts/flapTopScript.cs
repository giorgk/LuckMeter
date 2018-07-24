using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flapTopScript : MonoBehaviour {

	public Transform LeftFlap;
	public Transform RightFlap;
	public float openTime = 0.2f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OpenClose(float initdelay, float tt){
		StartCoroutine (AnimateFlaps (initdelay, tt));

	}

	IEnumerator AnimateFlaps(float initialdelay, float tt){
		yield return new WaitForSeconds(initialdelay);
		float sR = 0f;
		float eR = 0f;

		//Open 
		sR = 0f;
		eR = 90f;
		float t = 0f;
		float u;
		while (t < openTime) {
			u = Mathf.Lerp (sR, eR, t / openTime);
			LeftFlap.rotation = Quaternion.Euler(0f, 0f, -u);
			RightFlap.rotation = Quaternion.Euler(0f, 0f, u);
			t += Time.unscaledDeltaTime;
			yield return null;
		}

		yield return new WaitForSecondsRealtime (tt);

		sR = 90f;
		eR = 0f;
		t = 0f;
		while (t < openTime) {
			u = Mathf.Lerp (sR, eR, t / openTime);
			LeftFlap.transform.rotation = Quaternion.Euler(0f, 0f, -u);
			RightFlap.transform.rotation = Quaternion.Euler(0f, 0f, u);
			t += Time.unscaledDeltaTime;
			yield return null;
		}
	}
}
