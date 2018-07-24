using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;
using UnityEngine.UI;

public class LoadSaveScript : MonoBehaviour {

	public PlayerListScipt PlayerNameList;
	public PlayerListScipt HiScoreList;
	public Text CurrentPlayerText;

	SavedData PlayersData = new SavedData();

	string CurrentPlayerName;
	int CurrentPlayerId;


	void Awake(){
		CurrentPlayerName = null;
		CurrentPlayerId = -9;
		//File.Delete (Application.persistentDataPath + "/LuckyMeter.dat");
		if (!loadBinaryData ()) {
			saveBinaryData ();
		}
	}

	public void setCurrentPLayer(string nm){
		for (int i = 0; i < PlayersData.PlayerStats.Count; i++) {
			if (PlayersData.PlayerStats [i].PlayerName.Equals (nm, System.StringComparison.CurrentCultureIgnoreCase)) {
				CurrentPlayerName = nm;
				CurrentPlayerId = i;
				CurrentPlayerText.text = nm;
				return;
			}
		}
	}

	public string getCurrentPlayer(){
		return CurrentPlayerName;
	}

	public List<string> getPlayerNames(){
		List<string> names = new List<string> ();
		for (int i = 0; i < PlayersData.PlayerStats.Count; i++) {
			names.Add (PlayersData.PlayerStats [i].PlayerName);
		}
		return names;
	}

	public int getNplayers(){
		return PlayersData.PlayerStats.Count;
	}

	public void AddNewPlayer(string nm){
		if (!bPlayerExists (nm)) {
			PlayersData.AddNewPlayer (nm);
			saveBinaryData ();
			PlayerNameList.RefreshNameList ();
			HiScoreList.RefreshNameList ();
		}
	}

	public void RemovePlayer(string nm){
		for (int i = 0; i < PlayersData.PlayerStats.Count; i++) {
			if (PlayersData.PlayerStats [i].PlayerName.Equals (nm, System.StringComparison.CurrentCultureIgnoreCase)) {
				PlayersData.PlayerStats.RemoveAt (i);
				saveBinaryData ();
				break;
			}
		}
		PlayerNameList.RefreshNameList ();
		HiScoreList.RefreshNameList ();
	}

	public void UpdateScore(string nm, int Nsuccess, List<int> LottoPicks){
		for (int i = 0; i < PlayersData.PlayerStats.Count; i++) {
			if (PlayersData.PlayerStats [i].PlayerName.Equals (nm, System.StringComparison.CurrentCultureIgnoreCase)) {
				PlayersData.PlayerStats [i].UpdatePlayerStats (Nsuccess);
			}
		}
		PlayersData.updateNumberDraws (LottoPicks);
		saveBinaryData ();
	}

	public void UpdateScore(int Nsuccess, List<int> LottoPicks){
		PlayersData.PlayerStats [CurrentPlayerId].UpdatePlayerStats (Nsuccess);
		PlayersData.updateNumberDraws (LottoPicks);
		saveBinaryData ();
	}

	public bool bPlayerExists(string nm){
		for (int i = 0; i < PlayersData.PlayerStats.Count; i++){
			if (PlayersData.PlayerStats [i].PlayerName.Equals (nm, System.StringComparison.CurrentCultureIgnoreCase))
				return true;
		}
		return false;
	}

	public float getAdProbability(){
		return PlayersData.ADprobability;
	}

	public void setAdProbability(float t){
		Debug.Log ("New Probability: " + t); 
		PlayersData.ADprobability = t;
	}

	public List<int> getLottoPickedStats(){
		return PlayersData.NumberStats;
	}

	public Dictionary<string,int> getAllPlayersScores(){
		Dictionary<string,int> playerscore = new Dictionary<string, int> ();
		foreach (PlayerStatistics ps in PlayersData.PlayerStats){
			playerscore.Add (ps.PlayerName, ps.TotalScore);
		}

		List<KeyValuePair<string,int>> sortedRec = (from kv in playerscore
			orderby kv.Value descending select kv).ToList();

		playerscore.Clear ();

		foreach (KeyValuePair<string, int> kv in sortedRec) {
			playerscore.Add (kv.Key, kv.Value);
		}

		return playerscore;
	}

	public List<int> fetchPlayerStats(string nm){
		List<int> Stats = new List<int> ();
		for (int i = 0; i < PlayersData.PlayerStats.Count; i++){
			if (PlayersData.PlayerStats [i].PlayerName.Equals (nm, System.StringComparison.CurrentCultureIgnoreCase)) {
				Debug.Log ("=======" + PlayersData.PlayerStats [i].PlayerName + "=======");
				Debug.Log ("NtimesPlayed: " + PlayersData.PlayerStats [i].NtimesPlayed);
				Debug.Log ("Score: " + PlayersData.PlayerStats [i].TotalScore);
				Stats.Add (PlayersData.PlayerStats [i].TotalScore);
				for (int j = 0; j < PlayersData.PlayerStats [i].TimesFound.Count; j++) {
					Debug.Log (j + " : " + PlayersData.PlayerStats [i].TimesFound [j]);
					Stats.Add (PlayersData.PlayerStats [i].TimesFound [j]);
				}
			}
		}
		return Stats;
	}


	public void saveBinaryData(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/LuckyMeter.dat");
		SavedData data = new SavedData ();
		data = PlayersData;
		bf.Serialize (file, data);
		file.Close ();
		//Debug.Log ("Data Saved");
	}

	public bool loadBinaryData(){
		if (File.Exists (Application.persistentDataPath + "/LuckyMeter.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/LuckyMeter.dat", FileMode.Open);
			SavedData data = (SavedData)bf.Deserialize (file);
			file.Close ();

			PlayersData = data;
			//Debug.Log ("Current probability " + PlayersData.ADprobability);
			//Debug.Log ("Times played " + PlayersData.NtimesPlayed);
//			for (int i = 0; i < PlayersData.NumberStats.Count; i++) {
//				Debug.Log ((i + 1).ToString () + " : " + PlayersData.NumberStats [i]);
//			}
//			for (int i = 0; i < PlayersData.PlayerStats.Count; i++) {
//				Debug.Log (PlayersData.PlayerStats [i].PlayerName + " : " + PlayersData.PlayerStats [i].TotalScore);
//			}

			return true;
		} else {
			return false;
		}
	}

}

[System.Serializable]
public class SavedData
{
	public SavedData(){
		
		PlayerStats = new List<PlayerStatistics> ();
		NumberStats = new List<int> ();
		ADprobability = 0.7f;
		exiAgorasi = 0;
		NtimesPlayed = 0;
		for (int i = 0; i < 49; i++) {
			NumberStats.Add (0);
		}
	}

	public float ADprobability;
	public int exiAgorasi;
	// this is the number of times that the game has been played
	// by all players
	public int NtimesPlayed;
	// A list that keeps the score of the players
	public List<PlayerStatistics> PlayerStats;
	// A list that keeps the number of times each number has drown.
	// THis is shared among the players of the same mobile
	public List<int> NumberStats;

	public void AddNewPlayer(string nm){
		PlayerStats.Add(new PlayerStatistics(nm));
	}

	public void updateNumberDraws(List<int> drawnNumbers){
		// The drawnNumbers list are the numbers that were found by the simulator
		//Debug.Log ("Number count " + NumberStats.Count.ToString ());
		for (int i = 0; i < drawnNumbers.Count; i++) {
			if (drawnNumbers [i] <= NumberStats.Count) {
				NumberStats [drawnNumbers[i]-1] += 1;
			}
		}
		NtimesPlayed += 1;
	}
}

[System.Serializable]
public class PlayerStatistics{
	public string PlayerName;
	public int TotalScore;
	public int NtimesPlayed;
	// This list saves the number of times the player has found i balls
	// TimesFound[0] -> number of times that found none
	// goes up to 6. i.e. the lenght of TimesFOund is 7;
	public List<int> TimesFound = new List<int>(); 

	public PlayerStatistics(string nm){
		PlayerName = nm;
		TotalScore = 0;
		NtimesPlayed = 0;
		for (int i = 0; i < 7; i++)
			TimesFound.Add (0);
	}

	public void UpdatePlayerStats(int NballsFound){
		if (NballsFound < 7 && NballsFound >=0) {
			TimesFound [NballsFound] = TimesFound [NballsFound] + 1;
			if (NballsFound > 0)
				TotalScore += (int)Mathf.Pow (10f, (float)NballsFound);
			NtimesPlayed++;
		}
	}
}
