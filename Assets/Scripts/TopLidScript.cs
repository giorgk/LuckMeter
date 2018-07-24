using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopLidScript : MonoBehaviour {

	GameManagerScript gmanager;

	void Start(){
		gmanager = (GameManagerScript)FindObjectOfType (typeof(GameManagerScript));
	}

	public void OpenLid(float initdelay){
		StartCoroutine (OpenLidRoutine(initdelay));
	}

	IEnumerator OpenLidRoutine(float initdelay){
		yield return new WaitForSeconds(initdelay);
		Vector3 closePos = transform.position;
		Vector3 openPos = closePos + 7 * Vector3.forward;
		float t = 0;
		float tot_time = 0.5f;
		while (t < tot_time) {
			transform.position = Vector3.Lerp (closePos, openPos, t / tot_time);
			t += Time.deltaTime;
			yield return null;
		}

		//yield return new WaitForSeconds(3f);

		while (true) {
			yield return new WaitForSeconds (1f);
			if (gmanager.AllBallsIN())
				break;
		}
		// stay open for 1 sec


		// close lif
		t = 0f;
		while (t < tot_time) {
			transform.position = Vector3.Lerp (openPos, closePos, t / tot_time);
			t += Time.deltaTime;
			yield return null;
		}
	}
}
