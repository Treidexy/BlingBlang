using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }
	private const string TIMESTAMP = "timestamp";

	public Camera MainCamera;
	private AudioSource m_Audio;

	private void Awake()
	{
		if (Instance is null)
			Instance = this;
		else
			Debug.LogError("Instance already exists!");

		m_Audio = GetComponent<AudioSource>();
		m_Audio.Play();
		m_Audio.time = PlayerPrefs.GetFloat(TIMESTAMP);
		#if UNITY_EDITOR
			m_Audio.mute = true;
		#endif
	}

	private void OnDestroy()
	{
		Instance = null;
		PlayerPrefs.SetFloat(TIMESTAMP, m_Audio.time);
		PlayerPrefs.Save();
	}
}
