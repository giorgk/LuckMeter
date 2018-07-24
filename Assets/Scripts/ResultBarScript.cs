using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultBarScript : MonoBehaviour {

	Image ResultBar;

	public List<Sprite> BarImages = new List<Sprite>();

	void Awake(){
		ResultBar = GetComponent<Image> ();
	}

	public void setBarImage(int ii){
		StartCoroutine (setImagesRoutine (ii));
		//ResultBar.sprite = BarImages [ii];
	}

	IEnumerator setImagesRoutine(int ii){

		for (int i = 0; i <= ii; i++) {
			ResultBar.sprite = BarImages [i];
			yield return new WaitForSecondsRealtime (0.5f);
		}
	}

}
