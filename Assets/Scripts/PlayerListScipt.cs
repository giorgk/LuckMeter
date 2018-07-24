using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ListTypeEnum{Names,Hiscores}

public class PlayerListScipt : MonoBehaviour {

	public ListTypeEnum ListType;
	public GameObject NameHolders;
	public Transform ScrollColliderLeft;
	public Transform ScrollColliderRight;
	public GameManagerScript gmanager;

	public ScrollwheelScript WheelL;
	public ScrollwheelScript WheelR;

	LoadSaveScript loadsave;

	List<GameObject> NameHolderList = new List<GameObject>();

	Vector3 initPosition;


	// Use this for initialization
	void Start () {
		loadsave = gmanager.GetComponent<LoadSaveScript> ();
		initPosition = transform.position;

		RefreshNameList ();
	}

	public void resetposition(){
		transform.position = initPosition;
	}

	public void RefreshNameList(){
		foreach (GameObject GO in NameHolderList) {
			Destroy (GO);
		}
		NameHolderList.Clear ();
		resetposition ();

		List<string> pNames = new List<string>();
		Dictionary<string,int> HiscoreNames = new Dictionary<string, int> ();
		float h = 0f;

		if (ListType == ListTypeEnum.Names) {
			pNames = loadsave.getPlayerNames ();
			h = (float)pNames.Count;
		} else if (ListType == ListTypeEnum.Hiscores) {
			HiscoreNames = loadsave.getAllPlayersScores ();
			h = (float)HiscoreNames.Count;
		}

		if (h > 0) {
			ScrollColliderLeft.localScale = new Vector3 (0.35f, h, 1f);
			ScrollColliderRight.localScale = new Vector3 (0.35f, h, 1f);
			ScrollColliderLeft.localPosition = new Vector3 (-1.9f, -(h - 1f) / 2f, 0f);
			ScrollColliderRight.localPosition = new Vector3 (1.9f, -(h - 1f) / 2f, 0f);

			if (ListType == ListTypeEnum.Names) {
				for (int i = 0; i < pNames.Count; i++) {
					GameObject nameholder = Instantiate (NameHolders);
					nameholder.name = "PlayersList_" + pNames [i];
					nameholder.transform.SetParent (transform);
					nameholder.transform.localPosition = new Vector3(0f,-1f*(float)i ,0f);
					nameholder.GetComponent<PlayerNameHolderScript> ().SetText (pNames [i]);
					NameHolderList.Add (nameholder);
				}

			} else if (ListType == ListTypeEnum.Hiscores) {
				int cnt = 0;
				foreach (KeyValuePair<string,int> score in HiscoreNames) {
					GameObject nameholder = Instantiate (NameHolders);
					nameholder.name = "HiScoreLIst_" + score.Key;
					nameholder.transform.SetParent (transform);
					nameholder.transform.localPosition = new Vector3(0f,-1f*(float)cnt ,0f);
					nameholder.GetComponent<PlayerNameHolderScript> ().SetText (score.Key);
					nameholder.GetComponent<HiScoreHolderScript> ().SetText (score.Value.ToString());
					NameHolderList.Add (nameholder);
					cnt++;
				}
			}
		}
	}

	public int getNnames(){
		return loadsave.getNplayers ();
	}

	public void ScrollWheels(float dist){
		WheelL.Scroll (dist);
		WheelR.Scroll (dist);
	}
}
