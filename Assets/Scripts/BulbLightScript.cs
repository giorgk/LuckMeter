using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulbLightScript : MonoBehaviour {

	public float CycleTime = 2f;
	public float delayTime = 0.2f;

	public int idBulb{ get; set; }

	bool bAnimate = false;
	Material mymaterial;

	// Use this for initialization
	void Start () {
		mymaterial = GetComponent<Renderer> ().material;
		mymaterial.SetColor ("_Emit_color", Color.red);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AnimateLight(){
		bAnimate = true;
		StartCoroutine (AnimateLightRoutine ());

	}
	public void StopAnimatingLIght(){
		bAnimate = false;
	}

	IEnumerator AnimateLightRoutine(){
		float waittime = (float)idBulb * delayTime;
		yield return new WaitForSeconds (waittime);
		float t = 0;
		while (bAnimate == true) {
			t = 0f;
			while (t < CycleTime) {
				float huevalue = Mathf.Lerp (0f, 1f, t / CycleTime);
				Color temp = Color.HSVToRGB (huevalue, 1f, 1f);
				mymaterial.SetColor ("_Emit_color", temp);
				t += Time.fixedDeltaTime*Time.timeScale;
				//Debug.Log (Time.timeScale);
				yield return null;
			}
			yield return null;
		}
	}
}
