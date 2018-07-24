using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigPlayerPanelScript : MonoBehaviour {

	public Text ChoosenName;
	public GameManagerScript gmanager;
	LoadSaveScript loadsave;
	public Color DefaultColor; 
	public Color warningColor;

	Image ImagePanel;
	void Awake(){
		loadsave = gmanager.GetComponent<LoadSaveScript> ();
		ImagePanel = GetComponent<Image> ();
	}

	public void EnterNameAction(){
		string nm = ChoosenName.text;
		if (nm.Length < 3) {
			gmanager.ShowWarningMSG ("The name has to be at least 3 characters long");
			return;
		}

		if (loadsave.bPlayerExists (nm)) {
			gmanager.ShowWarningMSG ("The player " + nm + System.Environment.NewLine +
			"already exists");
			return;
		} else {
			// Confirm that the player wants to add this name
			gmanager.ConfirmQuestion ("Do you want to add" + System.Environment.NewLine +
			"a player with name:" + System.Environment.NewLine +
			nm, 1);// message, id

		}
	}

	public void AddPlayer(){
		loadsave.AddNewPlayer (ChoosenName.text);
	}

	public void EnterDeleteMode(){
		gmanager.setNewGameState (GameState.DeletePlayer);
		ImagePanel.color = warningColor;
	}

	public void setDefaultColor(){
		ImagePanel.color = DefaultColor;
	}

}
