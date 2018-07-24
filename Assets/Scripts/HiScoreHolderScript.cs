using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiScoreHolderScript : MonoBehaviour {

	public TextMesh HiScoreText;

	public void SetText(string nm){
		HiScoreText.text = nm;
	}
}
