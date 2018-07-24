using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoLights : MonoBehaviour {

	public List<Light> PlaySceneLights = new List<Light> ();

	float blinkspeed = 1f;

	bool bstoplights = false;

	List<Color> startCL = new List<Color> ();
	List<Color> endCL = new List<Color> ();

	void Start(){
		for (int i = 0; i < PlaySceneLights.Count; i++) {
			endCL.Add (PlaySceneLights [i].color);
			startCL.Add (PlaySceneLights [i].color);
		}
	}

	public void setBlinkSpeed(float spd){
		blinkspeed = spd;
	}


	IEnumerator playwithlights(){
		while (true) {
			if (bstoplights) {
				break;
			}
			float t = 0f;

			for (int i = 0; i < PlaySceneLights.Count; i++) {
				startCL [i] = endCL [i];
				endCL [i] = Color.HSVToRGB (Random.Range (0f, 1f), 1f, 1f);
			}

			while (t < blinkspeed) {
				for (int i = 0; i < PlaySceneLights.Count; i++) {
					PlaySceneLights [i].color = Color.Lerp (startCL [i], endCL [i], t / blinkspeed);
				}
				t += Time.unscaledDeltaTime;
				yield return null;
			}

			yield return null;
		}

		for (int i = 0; i < PlaySceneLights.Count; i++) {
			PlaySceneLights [i].color = Color.white;
			startCL [i] = Color.white;
		}
		yield return null;
	}

	public void startLights(){
		bstoplights = false;
		StartCoroutine (playwithlights ());
	}

	public void stopLights(){
		bstoplights = true;
	}

}
