using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public static class MyTools{

	// returns an integer to be used as random seed
	public static int getRandomSeed(){
		// Initialize random number generator
		int yr = System.DateTime.Now.Year;
		int m = System.DateTime.Now.Month;
		int d = System.DateTime.Now.Day;
		int hr = System.DateTime.Now.Hour;
		int mn = System.DateTime.Now.Minute;
		int sc = System.DateTime.Now.Second;
		int ml = System.DateTime.Now.Millisecond;
		//Debug.Log(yr.ToString() + "/" + m.ToString() + "/" + d.ToString() + " " + hr.ToString() + ":" + 
		//	mn.ToString() + ":" + sc.ToString() + ":" + ml.ToString());
		//Debug.Log (ml + sc * 1000 + mn * 100000 + hr * 10000000);
		return ml + sc * 1000 + mn * 100000 + hr * 10000000;
	}

	//Returns a list of colors that correspond to rainbow
	public static List<Vector3> RainbowColors(){
		List<Vector3> rainbow = new List<Vector3> ();
		rainbow.Add(new Vector3(148f/255f, 0f, 211f/255f));
		rainbow.Add(new Vector3(75f/255f, 0f, 130f/255f));
		rainbow.Add(new Vector3(0f, 0f, 255f/255f));
		rainbow.Add(new Vector3(0f, 255f/255f, 0f));
		rainbow.Add(new Vector3(255f/255f, 255f/255f, 0f));
		rainbow.Add(new Vector3(255f/255f, 127f/255f, 0f));
		rainbow.Add(new Vector3(255f/255f, 0f, 0f));
		return rainbow;
	}

	// Permutes randomly  numbers between 0 and N-1
	public static List<int> randomPermute(int N){
		Random.InitState (getRandomSeed ());
		List<int> permutedNumbers = new List<int> ();
		List<int> sortedNumbers = new List<int> ();

		for (int i = 0; i < N; i++) {
			sortedNumbers.Add (i);
		}

		while (sortedNumbers.Count > 0) {
			int j = Random.Range (0, sortedNumbers.Count);
			permutedNumbers.Add (sortedNumbers [j]);
			sortedNumbers.RemoveAt (j);
		}

		//For debug only
		//string tmp = "";
		//for (int i = 0; i < N; i++) {
		//	tmp += permutedNumbers [i].ToString () + ",";
		//}
		//Debug.Log (tmp);
		return permutedNumbers;
	}

	//returns a number between min max that follows a given probability function
	// given as animation curve. The limits of the animation curve have to be 0-1
	// for both axis
	public static float getRandomNumber(AnimationCurve CV, float min, float max){
		float y = Random.Range (0f, 1f);
		float x = CV.Evaluate (y);
		return fit (x, 0f, 1f, min, max);

	}

	public static float fit(float value, float oldMin, float oldMax, float newMin, float newMax){
		return (value - oldMin) * (newMax - newMin)/(oldMax - oldMin) + newMin;
	}

	public static List<Vector3> GetRainbowColor(){
		int Ngroups = 7;
		int Nper_group = Mathf.FloorToInt (49f / (float)Ngroups);
		//Debug.Log (Nper_group);
		int Nfinal_group = 49 - (Ngroups - 1) * Nper_group;

		List<Vector3> ColorGrade = new List<Vector3> ();
		List<Vector3> Rainclr = RainbowColors ();
		int igrp = 0; int grpcnt = 1;
		for (int ii = 1; ii < 50; ii++){
			float t = 0;
			if (grpcnt == Ngroups)
				t = (float)igrp / (float)Nfinal_group;
			else {
				t = (float)igrp / (float)Nper_group;
			}
			
			//ColorGrade.Add(Vector3.Lerp (Rainclr [grpcnt - 1], Rainclr [grpcnt], t));
			ColorGrade.Add(Rainclr [grpcnt - 1]);

			igrp++;
			if (igrp >= Nper_group && grpcnt != Ngroups) {
				igrp = 0;
				grpcnt++;
			}
		}
			
		//float x = (ind - 1) / 8.2f;
		//int ii = Mathf.FloorToInt(x);
		//float jj = ind - xx*8.2f;

		//Vector3.Lerp (Rainclr [ii], Rainclr [ii + 1], jj);

		return ColorGrade;
	}




	public static void getBoxColliderCoords(BoxCollider box){
		Vector3 pos = box.transform.localPosition;
		Vector3 rot = box.transform.localRotation.eulerAngles;
		Vector3 scl = box.transform.localScale;
		//Debug.Log (pos.ToString ());
		//Debug.Log (rot.ToString ());
		//Debug.Log (scl.ToString ());
		// this is unit box collider
		List<Vector3> rect = new List<Vector3> ();
		rect.Add (new Vector3 (-0.5f,-0.5f,-0.5f));
		rect.Add (new Vector3 ( 0.5f,-0.5f,-0.5f));
		rect.Add (new Vector3 ( 0.5f,-0.5f, 0.5f));
		rect.Add (new Vector3 (-0.5f,-0.5f, 0.5f));
		rect.Add (new Vector3 (-0.5f, 0.5f,-0.5f));
		rect.Add (new Vector3 ( 0.5f, 0.5f,-0.5f));
		rect.Add (new Vector3 ( 0.5f, 0.5f, 0.5f));
		rect.Add (new Vector3 (-0.5f, 0.5f, 0.5f));

		Matrix4x4 m = Matrix4x4.identity;
		Quaternion Q = Quaternion.Euler (rot);
		m.SetTRS (pos, Q, scl);
		for (int i = 0; i < rect.Count; i++) {
			rect [i] = m.MultiplyPoint3x4 (rect [i]);
			//Debug.Log (rect [i].ToString ());
		}
			
		Transform T = box.transform.parent;

		//Debug.Log (T.localPosition.ToString());
		//Debug.Log (T.localRotation.eulerAngles.ToString ());
		//Debug.Log (T.localScale.ToString ());

		Q = Quaternion.Euler (T.localRotation.eulerAngles);
		m.SetTRS (T.localPosition, Q, T.localScale);
		for (int i = 0; i < rect.Count; i++) {
			rect [i] = m.MultiplyPoint3x4 (rect [i]);
			//Debug.Log (rect [i].ToString ());
		}

		string meshName = box.name;
		string fileName =  "F:\\Unity3D\\LuckMeter\\Houdfiles\\lottoPart\\geo\\" + meshName + ".obj";

		StringBuilder sb = new StringBuilder ();

		for (int i = 0; i < rect.Count; i++) {
			//Debug.Log (sb.Length);
			//Debug.Log (sb);
			sb.Append(string.Format("v {0} {1} {2}\n", rect[i].x,rect[i].y,-rect[i].z));
		}

		sb.Append(string.Format("f {0} {1} {2} {3}\n", 1, 2, 3, 4));
		sb.Append(string.Format("f {0} {1} {2} {3}\n", 5, 6, 7, 8));
		sb.Append(string.Format("f {0} {1} {2} {3}\n", 1, 2, 6, 5));
		sb.Append(string.Format("f {0} {1} {2} {3}\n", 4, 3, 7, 8));
		sb.Append(string.Format("f {0} {1} {2} {3}\n", 1, 4, 8, 5));
		sb.Append(string.Format("f {0} {1} {2} {3}\n", 2, 3, 7, 6));

		StreamWriter sw = new StreamWriter (fileName);
		//Debug.Log (sb);
		sw.Write (sb);
		sw.Close ();
	}

	public static Vector3 getColor(float perc, List<Vector3> cl_schem){
		int ind = Mathf.FloorToInt (perc);
		if (ind < 0)
			ind = 0;
		if (ind > 99)
			ind = 99;
		//Debug.Log ("ind:" + ind);
		return  cl_schem [ind];
	}

	public static List<Vector3> Colorgrade(){
		List<Vector3> Autumn = new List<Vector3> ();
		Autumn.Add (new Vector3 (1.000f, 0.000f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.010f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.020f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.030f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.040f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.051f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.061f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.071f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.081f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.091f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.101f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.111f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.121f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.131f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.141f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.152f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.162f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.172f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.182f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.192f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.202f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.212f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.222f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.232f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.242f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.253f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.263f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.273f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.283f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.293f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.303f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.313f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.323f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.333f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.343f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.354f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.364f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.374f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.384f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.394f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.404f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.414f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.424f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.434f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.444f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.455f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.465f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.475f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.485f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.495f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.505f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.515f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.525f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.535f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.545f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.556f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.566f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.576f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.586f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.596f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.606f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.616f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.626f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.636f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.646f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.657f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.667f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.677f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.687f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.697f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.707f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.717f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.727f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.737f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.747f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.758f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.768f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.778f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.788f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.798f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.808f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.818f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.828f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.838f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.848f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.859f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.869f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.879f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.889f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.899f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.909f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.919f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.929f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.939f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.949f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.960f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.970f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.980f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 0.990f, 0.000f));
		Autumn.Add (new Vector3 (1.000f, 1.000f, 0.000f));

		return Autumn;
	}
}
