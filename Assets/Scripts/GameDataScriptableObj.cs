using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class GameDataScriptableObj : ScriptableObject {
	public float radius = 5f;
	public float TopColumnHeight = 3f;
	public int ColliderSides = 12;
	public int NumberOfBalls = 49;
	public Vector3 PrimitiveColliderScale;// = new Vector3 (3f, 0.5f, 6.19);
}
