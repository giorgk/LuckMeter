using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class sweeperMainScript : MonoBehaviour {

	public sweeperScript swL;
	public sweeperScript swR;

	float maxTimeScale = 5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartSweeping(float delay){
		StartCoroutine(startSweepRoutine(delay));
	}

	IEnumerator startSweepRoutine(float delay){
		yield return new WaitForSeconds(delay);
		swL.rotateSpeed = 0f;
		swR.rotateSpeed = 0f;
		swL.bSweep = true;
		swR.bSweep = true;
		float t = 0;
		float tot_time = 1f;
		while (t < tot_time) {
			swL.rotateSpeed = Mathf.Lerp (0f, 90f, t / tot_time);
			swR.rotateSpeed = -Mathf.Lerp (0f, 90f, t / tot_time);
			t += Time.deltaTime;
			yield return null;
		}
		t = 0f;
		while (t < tot_time) {
			Time.timeScale = Mathf.Lerp (1f, maxTimeScale, t / tot_time);
			t += Time.deltaTime;
			yield return null;
		}
	}

	public void StopSweeping(float delay){
		StartCoroutine(stopSweeRoutine(delay));
	}

	IEnumerator stopSweeRoutine(float delay){
		delay = 2f * delay;
		float t = 0;
		float tot_time = 1f;

		// bring the physics update down to 1
		while (t < tot_time) {
			Time.timeScale = Mathf.Lerp (maxTimeScale, 1f, t / tot_time);
			t += Time.deltaTime;
			yield return null;
		}
		Time.timeScale = 1f;
		// bring the sweepers to full stop
		t = 0f;
		while (t < tot_time){
			swL.rotateSpeed = Mathf.Lerp (90f, 0f, t / tot_time);
			swR.rotateSpeed = -Mathf.Lerp (90f, 0f, t / tot_time);
			t += Time.deltaTime;
			yield return null;
		}
		swL.rotateSpeed = 0f;
		swR.rotateSpeed = 0f;
		swL.bSweep = false;
		swR.bSweep = false;
	}

	public void SetRotationSpeed(float rot){
		swL.rotateSpeed = rot;
		swR.rotateSpeed = -rot;
	}
}
