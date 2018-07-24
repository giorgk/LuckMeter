using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNameHolderScript : MonoBehaviour {

	public TextMesh PlayerNameText;

	public void SetText(string nm){
		PlayerNameText.text = nm;
	}
}
