using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class buttonNumberScript : MonoBehaviour {

	//public Text buttonText;
	//public PickedNumbersScript PickedNumbersPanel;

	int Number;
	//float cl_mult = 1f;
	bool bselected = false;
	bool bIsValid = false;

	Texture2D mytexture;
	Material myMaterial;

	void Awake(){
		myMaterial = GetComponent<Renderer> ().material;
//		if (buttonText) {
//			string temp = name;
//			temp = temp.Substring (temp.IndexOf ("(") + 1);
//			//Debug.Log (temp);
//			buttonText.text = temp.Remove(temp.Length-1);
//			Number = int.Parse(buttonText.text);
//			buttonText.color = Color.gray;
//			GetComponent<Image> ().color = Color.Lerp (Color.yellow, Color.red, (float)Number / 49f);
//			//GetComponent<Image> ().color = new Color(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f));
//		}
	}

//	public void OnButtonClick(){
//		PickedNumbersPanel.SetPickedNumber (Number);
//	}

//	public void setColor(Color cl){
//		
//		cl.r = cl_mult * cl.r;
//		cl.g = cl_mult * cl.g;
//		cl.b = cl_mult * cl.b;
//		float h, s, v;
//		Color.RGBToHSV (cl, out h, out s, out v);
//		//Debug.Log (cl.ToString () + " " + h + "," + s + "," + v);
//		if (h > 0.5f) {
//			buttonText.color = Color.white;
//		} else {
//			buttonText.color = Color.black;
//		}
//		GetComponent<Image> ().color = cl;
//
//	}

	public void SetNumber(int numb){
		Number = numb;
		mytexture = Resources.Load ("ball_mask_" + Number.ToString (), typeof(Texture2D)) as Texture2D;
		if (mytexture) {
			myMaterial.SetTexture("_Mask", mytexture);
		} else {
			Debug.Log ("Didnt find the texture: " + "Resources/ball_mask_" + Number.ToString () );
		}
	}

	public int getNumber(){
		return Number;
	}

	// This method shoud toggle on and off the selection visual
	public void toggleSelection(){
		if (bselected) {
			bselected = false;
			myMaterial.SetFloat ("_EmmitValue", 0f);
		} else {
			bselected = true;
			myMaterial.SetFloat ("_EmmitValue", 0.6f);
		}
	}

	public bool IsSelected(){
		return bselected;
	}

	public bool IsValid(){
		return bIsValid;
	}

	public void setValid(bool tf){
		bIsValid = tf;
	}
}
