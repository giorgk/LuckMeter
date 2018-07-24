using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class ManagerAdScript : MonoBehaviour {

	public float InitialShowAdPropability = 0.5f;
	GameManagerScript gamemanager;
	LoadSaveScript loadsave;

	void Awake(){
		gamemanager = GetComponent<GameManagerScript> ();
		loadsave = GetComponent<LoadSaveScript> ();
	}

	public void ShowRewardedAd(){
		float r = Random.Range (0.1f, 1f);
		//Debug.Log(r + " < " + PlayerPrefs.GetFloat ("ShowAdProp"));
		if (r < loadsave.getAdProbability()) {
			//Debug.Log ("SHOW AD");
			if (Advertisement.IsReady ("video")) {//video ,rewardedVideo
				ShowOptions options = new ShowOptions{ resultCallback = HandleShowResult };
				Advertisement.Show ("video", options);
			}
		} else {
			//Debug.Log ("SKIP AD");
			gamemanager.setNewGameState (GameState.PickNumbers);
		}
	}

	private void HandleShowResult(ShowResult result){
		switch (result) {
		case ShowResult.Finished:
			loadsave.setAdProbability(Mathf.Max(0.1f, loadsave.getAdProbability() - 0.05f));
			break;
		case ShowResult.Skipped:
			loadsave.setAdProbability (Mathf.Min (1f, loadsave.getAdProbability () + 0.1f));
			loadsave.saveBinaryData ();
			break;
		case ShowResult.Failed:
			loadsave.setAdProbability(Mathf.Min(1f, loadsave.getAdProbability() + 0.05f));
			break;
		}
		gamemanager.setNewGameState (GameState.PickNumbers);

	}

	//public bool IsShowingAd(){
	//	return Advertisement.isShowing;
	//}
}
