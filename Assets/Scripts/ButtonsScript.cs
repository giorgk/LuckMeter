using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsScript : MonoBehaviour {

	Texture2D mytexture;
	int Number;

	Vector3 refscale;

	public void setNumber(int numb){
		Number = numb;
		mytexture = Resources.Load ("ball_mask_" + Number.ToString (), typeof(Texture2D)) as Texture2D;
		if (mytexture) {
			GetComponent<Renderer> ().material.mainTexture = mytexture;
		} else {
			Debug.Log ("Didnt find the testure: " + "Resources/ball_mask_" + Number.ToString () );
		}
	}

	public int getNumber(){
		return Number;
	}

	void Awake(){
		refscale = transform.localScale;
	}

	public void setScale(float scl){
		transform.localScale = scl * refscale;
	}
//	TopLidScript Toplid = null;
//	OpenBottomScript BottomLid = null;
//
//	
//
//	public void OpenTopLid(){
//		if (Toplid == null)
//			Toplid = GameObject.Find ("CLDR_0").GetComponent<TopLidScript> ();
//		
//		//Toplid.OpenLid();
//	}
//
//	public void OpenBottomLid(){
//		if (BottomLid == null) {
//			BottomLid = GameObject.Find ("CLDR_6").GetComponent<OpenBottomScript> ();
//		}
//		BottomLid.openLid ();
//
//	}
}
