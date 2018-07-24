using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlaceBulbsScript : MonoBehaviour {
	public bool CreateBulbs;
	public bool DeleteBulbs;
	public int BulbNumber = 10;
	public GameObject BulbPrefab;

	List<GameObject> bulbs = new List<GameObject>();
	float delta_angle;

	// Use this for initialization
	void Start () {
		Debug.Log ("Start Placing");
		delta_angle = 360f / (float)BulbNumber;
		Debug.Log (delta_angle.ToString ());
	}
	
	// Update is called once per frame
	void Update () {
		if (CreateBulbs) {
			Debug.Log ("Create stuff");
			for (int i = 0; i < BulbNumber; i++) {
				GameObject tempbulb = Instantiate (BulbPrefab, transform);
				tempbulb.transform.RotateAround (transform.position, Vector3.forward, delta_angle*(float)i);
				tempbulb.transform.GetChild (0).GetComponent<BulbLightScript> ().idBulb = i;
				bulbs.Add (tempbulb);
			}
			CreateBulbs = false;
		}

		if (DeleteBulbs) {
			foreach (GameObject go in bulbs) {
				DestroyImmediate (go);
			}
			bulbs.Clear ();
			Debug.Log ("Delete stuff");
			DeleteBulbs = false;
		}
	}
}
