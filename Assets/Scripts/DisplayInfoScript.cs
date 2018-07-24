using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInfoScript : MonoBehaviour {
	public Text TipText;

	List<string> Msg;
	int id_msg;

	// Use this for initialization
	void Awake () {
		Msg = CreateInfoMsgs ();
		id_msg = 0;
		TipText.text = Msg [id_msg];
	}

	public void ShowNextMSG(){
		id_msg = id_msg + 1;
		if (id_msg >= Msg.Count) {
			id_msg = 0;
		}
		Debug.Log (Msg.Count);
		TipText.text = Msg [id_msg];
	}

	public void SetInitMSG(){
		TipText.text = Msg [0];
	}



	List<string> CreateInfoMsgs(){
		List<string> msgs = new List<string> ();

		msgs.Add (" Luck-O-Meter" + System.Environment.NewLine +
			"is a gravity pick lotto machine simulator." + System.Environment.NewLine +
			"There are no money transactions involved whatsoever." + System.Environment.NewLine +
			"As the name implies it simply tests your luck." + System.Environment.NewLine +
			"Click Next for some tips");
		msgs.Add ("When you pick the numbers you will notice that each button has a different color." + System.Environment.NewLine +
			"These colors correspond to the frequency each number has drawn since the initial installment of the game." + System.Environment.NewLine +
			"The Red coloured numbers appeared less frequently. The more yellow they get the more often they show up." + System.Environment.NewLine +
				  "Although in every simulation each number has the same probability to by picked," + 
			" the probability of a number to be picked more frequently is way less." + System.Environment.NewLine +
			"After an unlimitted number of simulations all numbers should have roughly the same color. " +
			"Therefore, theoretacally speaking, the redish buttons have higher chances to appear" + System.Environment.NewLine +
				   "Nevertheless randomness has each own rules" );
		msgs.Add("Advertisments are annoying!!!." + System.Environment.NewLine +
			"We all know that." + System.Environment.NewLine +
			"This game offers a possibility to reduce the appearance of advertisments" + System.Environment.NewLine +
			"Every time you fully watch an add the probability for an AD to show is reduced." + System.Environment.NewLine +
			"The minimum probablility is 10%. That means if you always watch the ADs you will end up with a 10% chance to see ADs every time you click play." + System.Environment.NewLine +
			"On the other hand if you skip the ADs the chances to see the AD is increased. If you always skip them, then soon an AD will pop up every time to click play." + System.Environment.NewLine +
			"Bottom line: 'watching ADs is win-win!!!'");
		

		

		return msgs;
	}

}
