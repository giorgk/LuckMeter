using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManagerScript : MonoBehaviour {

	public int DefaultPanel;
	public List<CanvasGroup> PanelList = new List<CanvasGroup>();
	public Text MessageText;


	public int msgPanel { get; set;}
	public int confirmPanel{ get; set;}

	/* ------------PANEL LIST WITH IDS---------------------
	0 	:	Main Menu
	1	:	PickNumbers
	2	:	Play Panel
	3	:	Stats Panel
	4	:	Block Panel
	5	:	Message Panel
	6	:	Config PLayers
	7	:	Confirmation Panel
	8	:	HiScore Panel
	9	:	Results Panel
	10	:	Info Panel
	*/
	public void  Awake(){
		msgPanel = 5;
		confirmPanel = 7;
	}

	public void SetPanel(int ipanel){
		if (ipanel == msgPanel) {
			StartCoroutine (MSGRoutine ());
			return;
		}
		for (int i = 0; i < PanelList.Count; i++) {
			if (i == ipanel) {
				PanelList [i].alpha = 1f;
				PanelList [i].blocksRaycasts = true;
				PanelList [i].interactable = true;
			} else {
				PanelList [i].alpha = 0f;
				PanelList [i].blocksRaycasts = false;
				PanelList [i].interactable = false;
			}
		}
		if (ipanel == 6) {
			PanelList [6].GetComponent<ConfigPlayerPanelScript> ().setDefaultColor ();
		}
	}

	public void SetMSGtext(string msg){
		MessageText.text = msg;
	}

	IEnumerator MSGRoutine(){
		PanelList [msgPanel].alpha = 1f;
		PanelList [msgPanel].blocksRaycasts = true;
		PanelList [msgPanel].interactable = true;
		yield return new WaitForSecondsRealtime (2f);
		PanelList [msgPanel].alpha = 0f;
		PanelList [msgPanel].blocksRaycasts = false;
		PanelList [msgPanel].interactable = false;
	}

	public void AskConfirmation(string qtxt, int id){
		PanelList [confirmPanel].GetComponent<ConfirmationScript> ().AskConfirmation (qtxt, id);
		SetPanel (confirmPanel);
	}

	public void AddNewPLayer(){
		PanelList [6].GetComponent<ConfigPlayerPanelScript> ().AddPlayer ();
	}


}
