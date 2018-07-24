using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneScript : MonoBehaviour {

	GameManagerScript MyGameManager;

	Collider PickedBall;
	bool bStartCoRoutine = false;
	Vector3 PickedBallPos = new Vector3(0f, -5.81f, -2.25f);

	void Awake(){
		MyGameManager = GameObject.Find ("GameManager").GetComponent<GameManagerScript> ();
	}

	void OnTriggerEnter(Collider other){
		if (!bStartCoRoutine) {
			bStartCoRoutine = true;
			StartCoroutine (TriggeredEvents ());
		}
		PickedBall = other;
	}

	//void OnTriggerStay(Collider other){
		
	//}


	IEnumerator TriggeredEvents(){
		// first wait few seconds
		yield return new WaitForSeconds(2f);

		// then make the collider kinematic
		//PickedBall.GetComponent<Rigidbody>().isKinematic = true;
		//close the lid and move the object


		//PickedBall.GetComponent<Rigidbody>().MovePosition(PickedBall.transform.position - Vector3.up);
		PickedBall.GetComponent<Rigidbody>().MovePosition(PickedBallPos);
		yield return null;
		OpenBottomScript BottomLid = GameObject.Find ("BottomLid").GetComponent<OpenBottomScript> ();
		BottomLid.closeLid ();
		MyGameManager.PickedBallID = PickedBall.GetComponent<BallScript> ().getId ();
		//PickedBall.GetComponent<Rigidbody>().isKinematic = false;
		//PickedBall.GetComponent<Rigidbody> ().interpolation = RigidbodyInterpolation.Extrapolate;
		//PickedBall.GetComponent<Rigidbody> ().collisionDetectionMode = CollisionDetectionMode.Continuous;
		//PickedBall.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
		//PickedBall.GetComponent<Rigidbody> ().ResetInertiaTensor ();
		//PickedBall.GetComponent<Rigidbody> ().ResetCenterOfMass ();
		bStartCoRoutine = false;

	}

}
