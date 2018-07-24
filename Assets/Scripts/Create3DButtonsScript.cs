using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create3DButtonsScript : MonoBehaviour {

	public GameObject ButtonPrefab;
	public GameObject HistPrefab;
	public float radius = 1.45f;
	public float topbotdist = 0.17f;
	public float btnscale = 1f;

	public float histy = -1.34f;
	public float histscale = 1f;

	public StatsManagerScript Stats = null;

	List<GameObject> ButtonList = new List<GameObject>();
	List<GameObject> Histogram = new List<GameObject> ();
	List<float> ypos = new List<float>();
	int nRows = 17;



	void Awake(){
		ypos.Add (topbotdist);
		ypos.Add (0f);
		ypos.Add (-topbotdist);

		// create buttons
		int count = 1;
		for (int i = 0; i < 17; i++) {
			for (int j = 0; j < ypos.Count; j++){
				GameObject tmp = Instantiate (ButtonPrefab, transform);
				tmp.name = "Button_" + count.ToString ();
				Vector3 temppos = ParametricCircle ((2 * Mathf.PI / (float)nRows) * (float)i);
				temppos.y = ypos[j];
				tmp.transform.localPosition = temppos;

				tmp.transform.LookAt (transform.position);
				tmp.transform.localEulerAngles = new Vector3 (90f, tmp.transform.localEulerAngles.y, 180f);

				tmp.AddComponent<buttonNumberScript> ();

				if (count > 49) {
					tmp.GetComponent<buttonNumberScript> ().setValid (false);
					Destroy (tmp.GetComponent<Renderer> ());
				} else {
					tmp.GetComponent<buttonNumberScript> ().SetNumber (count);
					tmp.GetComponent<buttonNumberScript> ().setValid (true);
				}
				ButtonList.Add (tmp);

				count++;
			}	
		}

		// Create histogram bar
//		for (int i = 0; i < 49; i++) {
//			GameObject tmp = Instantiate (HistPrefab, transform);
//			tmp.name = "hist_" + (i+1).ToString ();
//			Vector3 temppos = ParametricCircle ((2 * Mathf.PI / 49f) * (float)i);
//			temppos.y = histy;
//			tmp.transform.localPosition = temppos;
//			tmp.transform.LookAt (transform.position);
//			tmp.transform.localEulerAngles = new Vector3 (0f, tmp.transform.localEulerAngles.y-90f, 0f);
//			tmp.transform.localScale = histscale * Vector3.one;
//			tmp.GetComponent<histbarScript> ().initializeBar (i + 1);
//			Histogram.Add (tmp);
//		}
		setPickNumbersDisplay ();
	}

//	void Update(){
//		updateBTN_TRNSF ();
////
////		for (int i = 0; i < 49; i++) {
////			Histogram [i].transform.localScale = histscale * Vector3.one;
////			Vector3 temp = Histogram [i].transform.localPosition;
////			temp.y = histy;
////			Histogram [i].transform.localPosition = temp;
////		}
////
////
//	}

	Vector3 ParametricCircle(float t){
		return new Vector3 (radius * Mathf.Cos (t), 0f, radius * Mathf.Sin (t));
	}

	// This method assigns proper color and histogram height according to ball draw proability
	public void setPickNumbersDisplay(){
		List<float> h = Stats.getScaledStats ();
		List<Vector3> autumn = MyTools.Colorgrade ();
		for (int i = 0; i < h.Count; i++) {
			//Debug.Log (i.ToString () + " :: " + h [i]);
			Vector3 tmpv = MyTools.getColor (100f*h[i], autumn);
			Color tmp = new Color(tmpv.x, tmpv.y,tmpv.z);
			Color bckgr = new Color (0.956f, 0.874f, 0.294f);//new Color (0.03f, 0.01f, 0.06f);
			ButtonList[i].GetComponent<Renderer> ().material.SetColor("_BckGrndColor", bckgr);
			ButtonList[i].GetComponent<Renderer> ().material.SetColor ("_CircleColor", tmp);
			ButtonList[i].GetComponent<Renderer> ().material.SetColor ("_TextColor", Color.black);
			//ButtonList [i].GetComponent<Renderer> ().material.SetFloat ("_EmmitValue", 0f);
			if (ButtonList [i].GetComponent<buttonNumberScript> ().IsSelected ()) {
				ButtonList [i].GetComponent<buttonNumberScript> ().toggleSelection ();
			}
			//Histogram [i].GetComponent<histbarScript> ().setHeight (h [i]);
		}
	}


	void updateBTN_TRNSF(){
		ypos[0] = topbotdist;
		ypos[1] = 0f;
		ypos[2] = -topbotdist;
		int count = 1;
		for (int i = 0; i < nRows; i++) {
			for (int j = 0; j < ypos.Count; j++) {
				if (count > 49)
					continue;

				Vector3 temppos = ParametricCircle ((2 * Mathf.PI / (float)nRows) * (float)i);
				temppos.y = ypos[j];
				ButtonList [count - 1].transform.localPosition = temppos; 
				ButtonList [count - 1].GetComponent<ButtonsScript> ().setScale (btnscale);
				count++;
			}
		}

	}
}
