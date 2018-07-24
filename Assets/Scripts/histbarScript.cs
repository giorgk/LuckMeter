using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class histbarScript : MonoBehaviour {

	public float RadScale = 1f;
	public float BaseHeightScale = 1f;
	public float MaxHeight = 1f;

	public Transform baseMesh = null;
	public Transform movableMesh = null;

	int Number;
	Texture2D mytexture;

	void Awake(){

	}




//	public void setbar(int number, float barheight){
//
//		bar.localScale = new Vector3 (2f, barheight, 2f);
//		bar.localPosition = new Vector3 (0f, barheight / 2f, 0f);
//	}

	public void initializeBar(int numb){
		Number = numb;
		mytexture = Resources.Load ("ball_mask_" + Number.ToString (), typeof(Texture2D)) as Texture2D;
		if (mytexture) {
			baseMesh.GetComponent<Renderer> ().material.mainTexture = mytexture;
			movableMesh.GetComponent<Renderer> ().material.mainTexture = mytexture;
		} else {
			Debug.Log ("Didnt find the testure: " + "Resources/ball_mask_" + Number.ToString () );
		}
	}

	public void setHeight (float h){
		Vector3 tmp = Vector3.one;
		tmp.y = h;
		movableMesh.transform.localScale = tmp;
	}

}
