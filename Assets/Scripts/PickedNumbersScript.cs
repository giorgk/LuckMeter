using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickedNumbersScript : MonoBehaviour {

	public GameManagerScript MyGameManager;

	public Text[] PickedNumberText;
	int Number2Set = 0; 
	public Text LogText;


	// Use this for initialization
	void Awake () {
		for (int i = 0; i < PickedNumberText.Length; i++) {
			PickedNumberText [i].text = "";
		}
	}

	public void SetPickedNumber (int picked) {
		if (MyGameManager.IsComplete()) {
			StartCoroutine (showLogMsgRoutine ("You have already choosen six numbers"));
			//Debug.Log ("You have already choosen six numbers");
			return;
		}
		if (MyGameManager.NumberExists (picked)) {
			StartCoroutine (showLogMsgRoutine ("You have already picked this number"));
			//Debug.Log ("You have already picked this number");
			return;
		} else {
			PickedNumberText [Number2Set].text = picked.ToString ();
			MyGameManager.AddNumber (picked);
			Number2Set++;
		}
	}

	public void ClearPrevious(){
		if (Number2Set > 0) {
			Number2Set--;
			PickedNumberText [Number2Set].text = "";
			MyGameManager.RemoveNumber(Number2Set);
		}
	}

	public void clearAll(){
		for (int i = 0; i < PickedNumberText.Length; i++) {
			PickedNumberText [i].text = "";
		}
		MyGameManager.ClearPickedNumbers ();
		Number2Set = 0;
	}

	IEnumerator showLogMsgRoutine(string s){
		LogText.text = s;
		yield return new WaitForSeconds(2f);
		LogText.text = "";
	}
}
