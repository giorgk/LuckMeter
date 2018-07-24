using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManagerScript : MonoBehaviour {
	/*   STATS to save
	 * Nplayed 			: The number of games has been played
	 * i (1-49) 		: How many times each number has shown up
	 * Nguessed 		: The total number of numbers the player has guessed 
	 * CumAvguess		: Cumulative average guess rate. CumAvguess/Nplayed gives the average guess rate
	 * Times_i			: How many times the player has found i balls
	 * */

	LoadSaveScript loadsave;

	//This is a variable that contains data for the application
	public GameDataScriptableObj gamedata;
	//public float BarSpace = 1f;
	//public Vector3 BarScale = Vector3.one;

	// This is how many times the game has played
	int Nplayed;
	// This lists the total number of appearances for each number
	Dictionary<int, int> CountNumb = new Dictionary<int,int>();
	Dictionary<int,int> TimesCount = new Dictionary<int,int>();
	int Nguessed;
	float CumAvguess;
	// The maximum count a number has so far
	int MaxCount;
	// The minimum count a number has so far
	int MinCount;

	void Awake(){
		loadsave = GetComponent<LoadSaveScript> ();
		//PlayerPrefs.DeleteAll ();
		//fetchCreateStats ();
	}

//	void fetchCreateStats(){
//		MinCount = 1000000;
//		MaxCount = 0;
//		Nplayed = 0;
//		if (PlayerPrefs.HasKey ("Nplayed")) {
//			Debug.Log ("Retrieve Stats");
//			CountNumb.Clear ();
//			TimesCount.Clear ();
//			Nplayed = PlayerPrefs.GetInt ("Nplayed");
//			Nguessed = PlayerPrefs.GetInt ("Nguessed");
//			CumAvguess = PlayerPrefs.GetFloat ("CumAvguess");
//			Debug.Log ("Nplayed: " + Nplayed + ", Nguessed: " + Nguessed + ", CumAvguess");
//			for (int i = 1; i <= gamedata.NumberOfBalls; i++) {
//				int itimes = PlayerPrefs.GetInt (i.ToString ());
//				if (itimes > MaxCount)
//					MaxCount = itimes;
//				if (itimes < MinCount)
//					MinCount = itimes;
//				CountNumb.Add (i, itimes);
//				//CountNumb.Add (itimes);
//			}
//
//			for (int i = 1; i <= 6; i++) {
//				int ii = PlayerPrefs.GetInt ("Times_" + i.ToString ());
//				TimesCount.Add (i, ii);
//			}
//
//		} else {
//			//Debug.Log ("Reseting STATS");
//			ResetStats ();
//		}
//	}

//	void createbars(){
//		for (int i = 0; i < gamedata.NumberOfBalls; i++) {
//			GameObject tmp = Instantiate (barprefab, transform);
//			tmp.GetComponent<histbarScript> ().setbar (i + 1, (float)CountNumb [i] / (float)Ncount);
//			tmp.transform.localEulerAngles = new Vector3 (0f, 0f, 0f);
//			//tmp.transform.position = new Vector3 (tmp.transform.position.x, (float)i * BarSpace, tmp.transform.position.z);
//			tmp.transform.position = new Vector3 ((float)i * BarSpace, tmp.transform.position.y , tmp.transform.position.z);
//			barlist.Add(tmp);
//		}
//	}

//	public void showStats(){
//		if (fetchStats ()) {
//			createbars ();
//		}
//	}



	public void ResetStats(){
		PlayerPrefs.SetInt ("Nplayed", 0);
		PlayerPrefs.SetInt ("Nguessed", 0);
		PlayerPrefs.SetFloat ("CumAvguess", 0f);
		for (int i = 1; i <= gamedata.NumberOfBalls; i++) {
			PlayerPrefs.SetInt (i.ToString (), 0);
		}

		for (int i = 1; i <= 6; i++) {
			PlayerPrefs.SetInt ("Times_" + i.ToString (), 0);
		}
	}


	public void updateStats(List<int> LottoPicks, int Nguess){
		// LottoPicks is an array of the numbers picked by the simulation. Typically the array is 6 however no check is made
		//Nguess is how many the player guessed from the array above
		for (int i = 0; i < LottoPicks.Count; i++) {
			CountNumb[LottoPicks [i]] += 1;
			PlayerPrefs.SetInt (LottoPicks [i].ToString (), CountNumb[LottoPicks [i]]);
			if (CountNumb [LottoPicks [i]] > MaxCount)
				MaxCount = CountNumb [LottoPicks [i]];
		}
		Nplayed += 1;
		Nguessed += Nguess;
		PlayerPrefs.SetInt ("Nplayed", Nplayed);
		PlayerPrefs.SetInt ("Nguessed",Nguessed);
		CumAvguess += (float)LottoPicks.Count / (float)Nplayed;
		PlayerPrefs.SetFloat ("CumAvguess", CumAvguess);
		if (Nguess > 0) {
			TimesCount [Nguess] += 1;
		}
	}

	public List<float> getScaledStats(){
		List<int> lottoPicks = loadsave.getLottoPickedStats ();
		int imin = 1000000000;
		int imax = 0;
		for (int i = 0; i < lottoPicks.Count; i++){
			Debug.Log ("lottoPicks " + lottoPicks [i]);
			if (lottoPicks[i] < imin)
				imin = lottoPicks[i];
			if (lottoPicks [i] > imax)
				imax = lottoPicks [i];
		}
		float fmin = (float)imin;
		float fmax = (float)imax;
		//Debug.Log ("fmin " + fmin);
		//Debug.Log ("fmax" + fmax);


		List<float> linearstat = new List<float> ();
		for (int i = 0; i < lottoPicks.Count; i++) {
			//Debug.Log (i + ": " + CountNumb [i]);
			float v = MyTools.fit ((float)lottoPicks [i], fmin, fmax, 0f, 1f);
			linearstat.Add (v); 
		}
		return linearstat;
	}

	public List<int> getTimesCount(){
		List<int> tmp = new List<int> ();
		for (int i = 1; i <= 6; i++){
			tmp.Add (TimesCount [i]);
		}
		return tmp;
	}

}
