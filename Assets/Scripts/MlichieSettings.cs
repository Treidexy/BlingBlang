using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MlichieSettings : ScriptableObject
{
	/// <summary>
	/// X = Start y position
	/// Y = End y position
	/// </summary>
	public Vector2 ThrompY = new Vector2(0.5f, -2);
	[Range(0, 1)]
	public float ThrompSpeed = 0.03f;
}
