//#define MUTE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }
	public const string TUTORIAL = "tutorial";
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
		#if MUTE
			m_Audio.mute = true;
		#endif
	}

	private void OnDestroy()
	{
		Instance = null;
		PlayerPrefs.SetFloat(TIMESTAMP, m_Audio.time);
		PlayerPrefs.Save();
	}

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
