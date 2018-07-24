using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum ObjetType{Button, Pick, Stats, none, PlayerName}

enum inputStates{PlayerList, DrumRoll, HiScoreList};

public class InputManagerScript : MonoBehaviour {


	public RotateObjScript PickNumbersDrum = null;
	public float SlowDownTime = 2f;
	public AnimationCurve SlowDownProfile = new AnimationCurve ();

	// This stores the screen position everytime the player holds the mouse or the finger down
	Vector3 TouchPos;
	bool bmouseTouchdown = false;

	bool bobjTouched = false;
	string objType;

	bool bfirstTouchset = false;
	Vector3 FirstScreenPos;
	Vector3 SecondScreenPos;
	Vector3 DeltaVec;

	bool bhasclickedButton = false;

	ObjetType cldr_type;
	float deltaX = 0f;

	GameManagerScript gamemanager = null;
	LoadSaveScript loadsave = null;

	bool doUpdate = false;

	public string DeletedPlayerName{ get; set; }

	inputStates inputstate;

	Transform playerlisttransform;
	PlayerListScipt PLscript;
	Transform HiScoreListTransform;
	PlayerListScipt HLscript;

	void Awake(){
		gamemanager = GetComponent<GameManagerScript> ();
		loadsave = GetComponent<LoadSaveScript> ();
	}



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			bool skipRest = false;
			Collider col = RayCastfromPoint (Input.mousePosition);

			if (col) {
				if (gamemanager.gamestate == GameState.DeletePlayer || gamemanager.gamestate == GameState.ConfigPlayer) {
					if (col.name.Length > 9) {
						if (col.name.Substring (0, 9).CompareTo ("ConfigCol") == 0) {
							inputstate = inputStates.PlayerList;
							bmouseTouchdown = true;
							playerlisttransform = col.transform.parent;
							PLscript = playerlisttransform.GetComponent<PlayerListScipt> ();
							Debug.Log (playerlisttransform.position.ToString ());
							skipRest = true;
						}	
					}
				} else if (gamemanager.gamestate == GameState.PickNumbers) {
					if (col.name.Length > 4) {
						if (col.name.Substring (0, 4).CompareTo ("CLDR") == 0) {
							inputstate = inputStates.DrumRoll;
							bmouseTouchdown = true;
							skipRest = true;
						}
					}
				} else if (gamemanager.gamestate == GameState.HiScores) {
					Debug.Log (col.name);
					if (col.name.Length > 10) {
						if (col.name.Substring (0, 10).CompareTo ("HiScoreCol") == 0) {
							inputstate = inputStates.HiScoreList;
							bmouseTouchdown = true;
							HiScoreListTransform = col.transform.parent;
							HLscript = HiScoreListTransform.GetComponent<PlayerListScipt> ();
							skipRest = true;
						}
					}
				}

				if (!skipRest) {
					if (gamemanager.gamestate == GameState.DeletePlayer) { //Identify buttons during delete players mode
						Debug.Log (col.name);
						PlayerNameHolderScript pickedPlayer = col.GetComponent<PlayerNameHolderScript> ();
						if (pickedPlayer) {
							DeletedPlayerName = pickedPlayer.PlayerNameText.text;
							gamemanager.ConfirmQuestion ("Are you sure you want" +
							System.Environment.NewLine + "to delete player:" +
							System.Environment.NewLine + pickedPlayer.PlayerNameText.text, 2);
							bmouseTouchdown = false;
							//loadsave.RemovePlayer (pickedPlayer.PlayerNameText.text);

						} else {
							Debug.Log ("is null");
						}

					} else if (gamemanager.gamestate == GameState.ConfigPlayer) { // Identify buttons during select player mode
						PlayerNameHolderScript pickedPlayer = col.GetComponent<PlayerNameHolderScript> ();
						if (pickedPlayer) {
							loadsave.setCurrentPLayer (pickedPlayer.PlayerNameText.text);
							gamemanager.setNewGameState (GameState.MainMenu);
							gamemanager.ShowWarningMSG ("Selected player:" +
							System.Environment.NewLine + pickedPlayer.PlayerNameText.text);
							bmouseTouchdown = false;
						}
					} else if (gamemanager.gamestate == GameState.PickNumbers) {// Identify the picked numbers
						buttonNumberScript pressedBTN = col.GetComponent<buttonNumberScript> ();
						if (pressedBTN) {
							if (pressedBTN.IsValid ()) {
								if (pressedBTN.IsSelected ()) {
									gamemanager.RemoveNumber (pressedBTN.getNumber ());
									pressedBTN.toggleSelection ();
								} else {
									bool tf = gamemanager.AddNumber (pressedBTN.getNumber ());
									if (tf) {
										pressedBTN.toggleSelection ();
									}
								}
							}
							bmouseTouchdown = false;
						}
					} else if (gamemanager.gamestate == GameState.HiScores) {// Identify buttons during hiscore menu
						PlayerNameHolderScript pickedPlayer = col.GetComponent<PlayerNameHolderScript> ();
						if (pickedPlayer) {
							List<int> Stats = loadsave.fetchPlayerStats (pickedPlayer.PlayerNameText.text);
							gamemanager.ShowIndieStats (Stats);
						}

					}
				}
			}
		}




		if (bmouseTouchdown) {
			TouchPos = Input.mousePosition;

			if (bfirstTouchset == false) {
				FirstScreenPos = TouchPos;
				bfirstTouchset = true;
			} else {
				SecondScreenPos = TouchPos;
				DeltaVec = SecondScreenPos - FirstScreenPos;

				switch (inputstate) {
				case inputStates.PlayerList:
					float yy = playerlisttransform.position.y + 0.01f * DeltaVec.y;
					Debug.Log ("yy: " + yy);
					if (yy < 1.9f) {
						yy = 1.9f;
						DeltaVec.y = 0f;
					}
					if (loadsave.getNplayers () > 4) {
						float ylim = 1.9f + (float)(loadsave.getNplayers () - 4);
						if (yy > ylim) {
							yy = ylim;
							DeltaVec.y = 0f;
						}
					} else {
						DeltaVec.y = 0f;
						yy = 1.9f;
					}
					PLscript.ScrollWheels (-DeltaVec.y);
					playerlisttransform.position = new Vector3 (playerlisttransform.position.x, yy, playerlisttransform.position.z);
					//Debug.Log ("SecondScreenPos: " + SecondScreenPos.ToString());
					FirstScreenPos = SecondScreenPos;
					break;
				case inputStates.DrumRoll:
					PickNumbersDrum.RotateAroundY (PickNumbersDrum.transform.position, FirstScreenPos, SecondScreenPos);
					FirstScreenPos = SecondScreenPos;
					break;
				case inputStates.HiScoreList:
					float yHi = HiScoreListTransform.position.y + 0.01f * DeltaVec.y;
					if (yHi < 1.5f) {
						yHi = 1.5f;
						DeltaVec.y = 0f;
					}
					if (loadsave.getNplayers () > 4) {
						float ylim = 1.9f + (float)(loadsave.getNplayers () - 4);
						if (yHi > ylim) {
							yHi = ylim;
							DeltaVec.y = 0f;
						}
					} else {
						DeltaVec.y = 0f;
						yHi = 1.5f;
					}
					HLscript.ScrollWheels (-DeltaVec.y);
					HiScoreListTransform.position = new Vector3 (HiScoreListTransform.position.x, yHi, HiScoreListTransform.position.z);
					FirstScreenPos = SecondScreenPos;
					break;
				}
			}
		}

		if (Input.GetMouseButtonUp (0)) {
			bmouseTouchdown = false;
			bfirstTouchset = false;
			//Debug.Log("Mouse Up");
		}



			

//		if (Input.GetMouseButtonUp(0)){
//			bmouseTouchdown = false;
//			bfirstTouchset = false;
//			bhasclickedButton = false;
//			//Debug.Log("Mouse Up");
//			if (cldr_type == ObjetType.Pick || cldr_type == ObjetType.Stats){
//				StartCoroutine(SlowDownRoutine());
//			}
//		}




//		if (bmouseTouchdown && !bhasclickedButton) {
//			//Debug.Log (bfirstTouchset);
//			if (bfirstTouchset == false) {
//				Ray ray = Camera.main.ScreenPointToRay(TouchPos);
//				RaycastHit hit;
//				if (Physics.Raycast (ray, out hit, 10f)) {
//					string nm_cldr = hit.collider.name;
//					string prefix = nm_cldr.Substring(0,7);
//					if (prefix.CompareTo ("Button_") == 0) {
//						cldr_type = ObjetType.Button;
//						Debug.Log ("WHY?");
//						buttonNumberScript pressedBTN = hit.collider.gameObject.GetComponent<buttonNumberScript> ();
//
//						if (pressedBTN.IsSelected ()) {
//							gamemanager.RemoveNumber (pressedBTN.getNumber ());
//							pressedBTN.toggleSelection ();
//						} else {
//							bool tf = gamemanager.AddNumber (pressedBTN.getNumber ());
//							if (tf) {
//								pressedBTN.toggleSelection ();
//							}
//						}
//						bhasclickedButton = true;
//					} else {
//						bfirstTouchset = true;
//						oldScreenPos = TouchPos;
//					}
//				}
//
//				//Debug.Log ("Is here at all?");
//			} else {
//				//Debug.Log ("IS HERE?");
//				Ray ray = Camera.main.ScreenPointToRay(TouchPos);
//				RaycastHit hit;
//
//				if (Physics.Raycast (ray, out hit, 10f)) {
//					string nm_cldr = hit.collider.name;
//					if (nm_cldr.CompareTo ("CLDR_UP") == 0 ||
//						nm_cldr.CompareTo ("CLDR_Down") == 0) {
//						cldr_type = ObjetType.Pick;
//						newScreenPos = TouchPos;
//						PickNumbers.RotateAroundY (PickNumbers.transform.position, oldScreenPos, newScreenPos);
//						deltaX = oldScreenPos.x - newScreenPos.x;
//						oldScreenPos = newScreenPos;
//					} else if (nm_cldr.CompareTo ("Stats_CLDR") == 0) {
//						cldr_type = ObjetType.Stats;
//						newScreenPos = TouchPos;
//						Stats.RotateAroundY (Stats.transform.position, oldScreenPos, newScreenPos);
//						deltaX = oldScreenPos.x - newScreenPos.x;
//						oldScreenPos = newScreenPos;
//					}
//				}
//			}
//		}
	}

	IEnumerator SlowDownRoutine(){
		float t = 0;
		// clamp deltaX
		float s = Mathf.Sign(deltaX);
		deltaX = Mathf.Abs (deltaX);
		if ( deltaX > 15f)
			deltaX =  Mathf.Lerp(15f,25f, Mathf.Abs(deltaX)/150f);
		deltaX = Mathf.Clamp (deltaX, 0f, 25f);
		Debug.Log (deltaX);
		while (t < SlowDownTime) {
			t += Time.deltaTime;
			float dx = Mathf.Lerp (deltaX, 0f, SlowDownProfile.Evaluate( t / SlowDownTime));
			if (cldr_type == ObjetType.Pick) {
				PickNumbersDrum.RotateAroundY (PickNumbersDrum.transform.position, s* dx);
			} 
			yield return null;
		}
		cldr_type = ObjetType.none;
		deltaX = 0f;
	}

	Collider RayCastfromPoint(Vector3 p){
		//p.z = -10f;
		//Vector3 worldTouchPos2D = Camera.main.ScreenToWorldPoint (p);
		//Debug.Log (worldTouchPos2D.ToString ());
		//Ray r = new Ray (new Vector3 (worldTouchPos2D.x, worldTouchPos2D.y, -17.5f), 12 * Vector3.forward);
		//Debug.DrawRay (new Vector3 (worldTouchPos2D.x, worldTouchPos2D.y, -17.5f), 12 * Vector3.forward, Color.red, 2f);
		Ray r = Camera.main.ScreenPointToRay(p);
		Debug.DrawRay (r.origin, r.direction,Color.red,2f);
		RaycastHit hit;
		if (Physics.Raycast (r, out hit)) {
			return hit.collider;
		} else
			return null;
	}
}
