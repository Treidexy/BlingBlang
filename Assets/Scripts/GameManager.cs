using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }
	public const string TUTORIAL = "tutorial";
	private const string MUTED = "muted";
	private const string TIMESTAMP = "timestamp";

	public Camera MainCamera;
	private AudioSource m_Audio;

	private void Awake()
	{
		if (Instance is null)
			Instance = this;
		else
			Debug.LogError("Instance already exists!");
	}

	private void Start()
	{
		m_Audio = GetComponent<AudioSource>();
		m_Audio.Play();
		m_Audio.time = PlayerPrefs.GetFloat(TIMESTAMP);
		m_Audio.mute = PlayerPrefs.GetInt(MUTED) == 1;
	}

	private void OnDestroy()
	{
		Instance = null;
		PlayerPrefs.SetFloat(TIMESTAMP, m_Audio.time);
		PlayerPrefs.SetInt(MUTED, m_Audio.mute ? 1 : 0);
		PlayerPrefs.Save();
	}

	private void FixedUpdate()
	{
		if (Input.GetKeyDown(KeyCode.M))
			ToggleMute();
	}

	public void Exit() =>
		Application.Quit();

	public void ToggleMute() =>
		m_Audio.mute = !m_Audio.mute;

	public void GotoLevel(int lvl = 0)
	{
		if (PlayerPrefs.HasKey(TUTORIAL) || lvl > 0)
		{
			Player.s_Level = lvl;
			SceneManager.LoadSceneAsync((int)Scenes.Level1 + lvl);
		}
		else
			GotoTutorial();
	}

	public void GotoMenu() =>
		SceneManager.LoadSceneAsync((int)Scenes.Menu);

	public void GotoTutorial() =>
		SceneManager.LoadSceneAsync((int)Scenes.Tutorial);

	public void GotoLevelScreen() =>
		SceneManager.LoadSceneAsync((int)Scenes.Level);
}
