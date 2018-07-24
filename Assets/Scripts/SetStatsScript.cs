using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetStatsScript : MonoBehaviour {

	public List<Text> DisplayItems = new List<Text> ();

	public void SetStats(List<int> timesFound){
		
		for (int i = 0; i < timesFound.Count; i++) {
			DisplayItems [i].text = timesFound [i].ToString ();
		}
	}



}
