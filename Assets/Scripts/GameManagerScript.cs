using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState{MainMenu, Play, PickNumbers, HiScores, Stats, info, Results, AD, ConfigPlayer, ConfirmQuestion, DeletePlayer}

public class GameManagerScript : MonoBehaviour {

	//This is a variable that contains data for the application
	public GameDataScriptableObj gamedata;

	// This is the prefab for the ball
	public GameObject ballPrefab;

	// how fast to spawn the balls
	public float BallSpawnDelayTime = 0.1f;

	// THis is a reference to the object that controls the sweepers
	public sweeperMainScript sweeperManager; 

	public CanvasManagerScript canvasManager;
	public MyCameraScript cameraManager;

	public Create3DButtonsScript createButtonBarrel;

	// This variables defines a cumulative probability density function
	// which is used to dictate when the bottom lid opens
	public AnimationCurve CumProDensFunct = new AnimationCurve();
	public float minOpenTime = 5f;
	public float maxOpenTime = 10f;

	public Text PickedNumbersHUD;
	public Text PickPanelText;


	public GameState gamestate { get; set;}

	public ResultBarScript ResultBar;

	public PlayerListScipt PlayerList;
	public PlayerListScipt HiscoreList;

	public DiscoLights lightsmanager;

	public SetStatsScript IndieStats;

	public flapTopScript Flaps;

	public ManageBulbsScript buldsOnOff;

	List<int> PlayerPickedNumbers = new List<int>();
	List<int> LottoPicks = new List<int> ();

	List<GameObject> ballList = new List<GameObject> ();
	bool bIscreatingballs = false;

	TopLidScript Toplid = null;
	OpenBottomScript BottomLid = null;

	[System.NonSerialized]
	public int PickedBallID = 0;

	List<int> ballInitNumbers = new List<int> ();
	List<Vector3> ballColors = new List<Vector3>();



	StatsManagerScript statManager = null;
	ManagerAdScript Admanager = null;
	InputManagerScript inputmanager = null;
	LoadSaveScript loadsave = null;


	void Awake(){
		
		ballColors = MyTools.GetRainbowColor ();
		Toplid = GameObject.Find ("TopLid").GetComponent<TopLidScript> ();
		BottomLid = GameObject.Find ("BottomLid").GetComponent<OpenBottomScript> ();

		statManager = GetComponent<StatsManagerScript> ();
		Admanager = GetComponent<ManagerAdScript> ();
		inputmanager = GetComponent<InputManagerScript> ();
		loadsave = GetComponent<LoadSaveScript> ();

		setNewGameState (GameState.MainMenu);

		// for Debug only
		//setNewGameState (GameState.ConfigPlayer);
	}


	public void StartNewGame(){
		if (IsComplete ()) {
			setNewGameState (GameState.Play);
			SetDisplayPickedNumbers ();// sets the text on the top part of the screen to the player picked numbers

			StartCoroutine (createBallsRoutine ());
			Flaps.OpenClose (2.9f, 3.5f);
			Toplid.OpenLid (3f);
			sweeperManager.StartSweeping (6f);
			StartCoroutine (SelectBallsRoutine ());
		}

	}

	// THe following methods control the canvas Picked Number Panel
	// returns true if the selected number has already been choosen
	public bool NumberExists(int numb){
		return PlayerPickedNumbers.Contains (numb);
	}

	// assings the picked number
	public bool AddNumber(int pickedNumber){
		if (PlayerPickedNumbers.Count < 6) {
			PlayerPickedNumbers.Add (pickedNumber);
			updatePickNumbersText ();
			return true;
		} else {
			return false;
		}
	}

	// remove the number from the list
	public void RemoveNumber(int N){
		PlayerPickedNumbers.Remove(N);
		updatePickNumbersText ();
	}

	// removes all numbers
	public void ClearPickedNumbers(){
		PlayerPickedNumbers.Clear ();
		updatePickNumbersText ();
	}

	//returns true in the player has picked 6 numbers
	public bool IsComplete(){
		if (PlayerPickedNumbers.Count == 6)
			return true;
		else
			return false;
	}

	// destroys all the balls in the game
	void killBalls(){
		foreach (GameObject ball in ballList) {
			Destroy (ball);
		}
		ballList.Clear ();
	}

	//This is a coroutine that creates the balls in the scene with delay
	IEnumerator createBallsRoutine(){
		killBalls ();
		if (ballPrefab != null) {
			bIscreatingballs = true;
			for (int i = 0; i < gamedata.NumberOfBalls; ++i) {
				int indexball = ballInitNumbers [i]+1;
				GameObject tmp = Instantiate (ballPrefab, new Vector3 (Random.Range (0f, 1f), gamedata.radius + gamedata.TopColumnHeight - 1f, Random.Range (0f, 1f)), Quaternion.identity);
				tmp.name = "ball_" + indexball.ToString ();
				tmp.GetComponent<Renderer> ().material.SetColor ("_TextColor", Color.white);
				tmp.GetComponent<Renderer> ().material.SetColor ("_BckGrndColor", new Color(Random.Range(0.1f,0.9f), Random.Range(0.1f,0.9f), Random.Range(0.1f,0.9f)));
				tmp.GetComponent<BallScript> ().SetID (indexball);
				ballList.Add (tmp);
				yield return new WaitForSeconds (BallSpawnDelayTime);
			}
			bIscreatingballs = true;
		}
	}

	IEnumerator SelectBallsRoutine(){
		LottoPicks.Clear ();
		lightsmanager.startLights ();
		lightsmanager.setBlinkSpeed (0.075f);
		yield return new WaitForSecondsRealtime (10f);
		for (int cnt = 0; cnt < 6; cnt++) {
			lightsmanager.setBlinkSpeed (0.075f);
			//Debug.Log ("Number of Picks: " + cnt);
			float waitTime = MyTools.getRandomNumber (CumProDensFunct, minOpenTime, maxOpenTime);
			//Debug.Log ("wait " + waitTime); 
			//System.DateTime start = System.DateTime.Now;
			yield return new WaitForSecondsRealtime (waitTime);
			//System.DateTime end = System.DateTime.Now;
			//Debug.Log (end.Subtract (start).TotalSeconds);
			if (BottomLid) {
				lightsmanager.setBlinkSpeed (0.75f);
				BottomLid.openLid ();
				while (PickedBallID == 0) {
					yield return null;
				}
				if (PickedBallID != 0) {
					//Debug.Log ("Loto picked " + PickedBallID);
					LottoPicks.Add (PickedBallID);
					PickedBallID = 0;
				}
			}
			else {
				//Debug.Log ("Bottom Lid is empty!");
			}

		}

		// stop rotators
		sweeperManager.StopSweeping(4f);
		lightsmanager.stopLights ();
		//Debug.Log ("Guess Success: " + calculateSuccess ());
		int nSucc = calcNguess();
		loadsave.UpdateScore (nSucc,LottoPicks);
		HiscoreList.RefreshNameList ();


		// save stats
		//statManager.updateStats(LottoPicks, nSucc);
		yield return new WaitForSecondsRealtime (14f);
		ResultBar.setBarImage (nSucc);//  .fillAmount = (float)nSucc / (float)LottoPicks.Count;
		setNewGameState(GameState.Results);
	}

	public void Pick2MainMenu(){
		gamestate = GameState.MainMenu;

	}

	// Transition from Results to main menu
	public void Results2MainMenu(){

		gamestate = GameState.MainMenu;
	}
		

	void SetDisplayPickedNumbers(){
		string displayText = "";
		for (int i = 0; i < PlayerPickedNumbers.Count; i++) {
			displayText += PlayerPickedNumbers [i].ToString ();
			if (i !=PlayerPickedNumbers.Count-1)
				displayText += "  ";

		}
		PickedNumbersHUD.text = displayText;
	}

	public void exitplay(){
		StopCoroutine (SelectBallsRoutine ());
		sweeperManager.StopSweeping (0.5f);
		gamestate = GameState.MainMenu;

	}
		

	// transition from Main menu to stats
	public void ShowHiScore(){
		setNewGameState (GameState.HiScores);
	}

	//transition from stats to main menu
	public void Stats2MainMenu(){
		gamestate = GameState.MainMenu;
	}


	public void ShowIndieStats(List<int> Stats){
		setNewGameState (GameState.Stats);
		IndieStats.SetStats (Stats);

	}


	void updatePickNumbersText(){
		string displ = "  ";
		foreach (int i in PlayerPickedNumbers) {
			displ += i.ToString () + "  ";
		}
		PickPanelText.text = displ;
	}


	int calcNguess(){
		int Nsucc = 0;
		foreach (int ii in LottoPicks){
			if (PlayerPickedNumbers.Contains (ii)) {
				Nsucc++;
			}
		}
		return Nsucc;
	}

	public void setNewGameState(GameState newstate){
		gamestate = newstate;
		if (newstate == GameState.MainMenu) {//-------------------------MAIN MENU
			canvasManager.SetPanel (0);
			cameraManager.setCameraState (CamState.Play);

		} else if (newstate == GameState.PickNumbers) {//--------------PICK NUMBERS
			canvasManager.SetPanel (1);
			cameraManager.setCameraState (CamState.Pick);
			createButtonBarrel.setPickNumbersDisplay ();
			ClearPickedNumbers ();
			updatePickNumbersText ();
			buldsOnOff.AnimateLights ();
		} else if (newstate == GameState.Play) {//---------------------PLAY 
			canvasManager.SetPanel (2);
			cameraManager.setCameraState (CamState.Play);
			ballInitNumbers = MyTools.randomPermute (gamedata.NumberOfBalls);
		} else if (newstate == GameState.AD) {//-------------------------------SHOW ADs
			canvasManager.SetPanel (4);// This will block the UI

		} else if (newstate == GameState.Results) {//---------------RESULTS
			buldsOnOff.StopLights();
			killBalls ();
			canvasManager.SetPanel (9);
			cameraManager.setCameraState (CamState.Results);

		} else if (newstate == GameState.ConfigPlayer) {//----------CONFIG PLAYERS
			PlayerList.resetposition();
			canvasManager.SetPanel (6);
			cameraManager.setCameraState (CamState.ConfigPlayers);
		} else if (newstate == GameState.ConfirmQuestion) {//---------CONFIRM QUESTION
			canvasManager.SetPanel (canvasManager.confirmPanel);
		} else if (newstate == GameState.HiScores) {// ---------------HiScores
			HiscoreList.resetposition();
			cameraManager.setCameraState (CamState.HiScores);
			canvasManager.SetPanel (8);
		} else if (newstate == GameState.Stats) {
			canvasManager.SetPanel (3);
		}else if (newstate == GameState.info){
			canvasManager.SetPanel (10);
		}
		
	}

	public void PlayButton(){
		if (loadsave.getCurrentPlayer() == null) {
			setNewGameState (GameState.ConfigPlayer);
		} else {
			setNewGameState (GameState.AD);
			Admanager.ShowRewardedAd ();
		}
	}

	public void ShowWarningMSG(string msg){
		canvasManager.SetMSGtext (msg);
		canvasManager.SetPanel (canvasManager.msgPanel);
	}

	public void ConfirmQuestion(string txt, int id){
		setNewGameState (GameState.ConfirmQuestion);
		canvasManager.AskConfirmation (txt, id);
	}

	public void AddnewPLayer(){
		canvasManager.AddNewPLayer ();
		setNewGameState (GameState.ConfigPlayer);
	}

	public void DeletePlayer(){
		loadsave.RemovePlayer (inputmanager.DeletedPlayerName);
		setNewGameState (GameState.ConfigPlayer);
	}

	public void EnterNamesMenu(){
		setNewGameState (GameState.ConfigPlayer);
	}

	public void Back2MainMenu(){
		setNewGameState (GameState.MainMenu);
	}

	public void ExitGame(){
		Application.Quit ();
	}

	public void InfoPanel(){
		setNewGameState (GameState.info);
	}

	public bool AllBallsIN(){
		bool allin = true;
		foreach (GameObject ball in ballList) {
			if (ball.transform.position.y > 4.5f) {
				allin = false;
				break;
			}
		}
		return allin;
	}
}
