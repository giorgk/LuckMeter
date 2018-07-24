using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CamState{Play, HiScores, Stats, Pick, Results, ConfigPlayers}

public class MyCameraScript : MonoBehaviour {
	CamState camerastate;
	public Camera SecondaryCam;

	void Awake(){
		//setCameraState (CamState.ConfigPlayers); //TEST
		//setCameraState (CamState.Play);
		SecondaryCam.gameObject.SetActive(false);
	}
		
	Vector3 prevClick;
	bool bmousedown = false;


	public void setCameraState(CamState camstate){
		float FOVcoeff = ((float)Screen.height / (float)Screen.width) / 1.6f;
		//Debug.Log ("FOVcoaff: " + FOVcoeff);
		camerastate = camstate;
		switch (camstate) {
		case CamState.Play:
			SecondaryCam.gameObject.SetActive(true);
			transform.position = new Vector3 (0f, -1.95f, -22.05f);
			transform.eulerAngles = new Vector3 (-1.736f, 0f, 0f);
			GetComponent<Camera> ().fieldOfView = 49f*FOVcoeff;
			break;
		case CamState.Stats:
			// Horizontal seeting
			SecondaryCam.gameObject.SetActive(false);
			transform.position = new Vector3 (2.4f, 0.4f, 146f);
			transform.eulerAngles = new Vector3 (0f, 0f, 0f);
			GetComponent<Camera> ().fieldOfView = 35f*FOVcoeff;

			// vertical setting
			//transform.position = new Vector3 (0.441f, 1.076f, 69.89f);
			//transform.eulerAngles = new Vector3 (0f, 0f, 0f);
			//GetComponent<Camera> ().fieldOfView = 35f;
			break;
		case CamState.Pick:
			SecondaryCam.gameObject.SetActive(false);
			transform.position = new Vector3 (74f, 1.765f, -21.33f);
			transform.eulerAngles = new Vector3 (0f, 0f, 0f);
			GetComponent<Camera> ().fieldOfView = 49f*FOVcoeff;
			break;
		case CamState.Results:
			SecondaryCam.gameObject.SetActive(false);
			transform.position = new Vector3 (80f, 1.765f, -21.33f);
			transform.eulerAngles = new Vector3 (0f, 0f, 0f);
			GetComponent<Camera> ().fieldOfView = 49f*FOVcoeff;
			break;
		case CamState.ConfigPlayers:
			SecondaryCam.gameObject.SetActive(false);
			transform.position = new Vector3 (-120f, 0f, -17.5f);
			transform.eulerAngles = new Vector3 (0f, 0f, 0f);
			GetComponent<Camera> ().fieldOfView = 20f*FOVcoeff;
			break;
		case CamState.HiScores:
			SecondaryCam.gameObject.SetActive(false);
			transform.position = new Vector3 (-100f, 0f, -17.5f);
			transform.eulerAngles = new Vector3 (0f, 0f, 0f);
			GetComponent<Camera> ().fieldOfView = 20f*FOVcoeff;
			break;
		}
	}

	public void setCameraStats(){
		setCameraState (CamState.Stats);
	}

	public void setCameraPlay(){
		setCameraState (CamState.Play);
	}

	public void setCameraPick(){
		setCameraState (CamState.Pick);
	}

	public void setCameraResult(){
		setCameraState (CamState.Results);
	}
}
