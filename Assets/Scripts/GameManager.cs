using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	public Camera MainCamera;

	private void Awake()
	{
		if (Instance is null)
			Instance = this;
		else
			Debug.LogError("Instance already exists!");
	}

    private void OnDestroy() =>
		Instance = null;
}
