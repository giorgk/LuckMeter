using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ConfirmationScript : MonoBehaviour {
	public Text QuestionText;
	public GameManagerScript gmanager;

	int questionID;
	/* List of IDs and questions
	1	Confirm new player
	*/
	public void AskConfirmation(string qtxt, int id){
		QuestionText.text = qtxt;
		questionID = id;

	}

	public void ButtonYES(){
		switch (questionID) {
		case 1://Add new player and go back to configuration menu
			gmanager.AddnewPLayer ();
			break;
		case 2:
			gmanager.DeletePlayer ();
			break;
		case 3:
			
			break;
		default:
			break;
			
			
		}


	}

	public void ButtonNO(){
		switch (questionID) {
		case 1: // Just go back to configuration menu
			//gmanager.AddnewPLayer();
			break;
		case 2:

			break;
		case 3:

			break;
		default:
			break;


		}
	}
}
