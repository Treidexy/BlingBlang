//#define MUTE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }
	private const string TIMESTAMP = "timestamp";
	private const string TUTORIAL = "tutorial";

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

	public void GotoLevel1()
	{
		if (PlayerPrefs.HasKey(TUTORIAL))
			SceneManager.LoadSceneAsync((int)Scenes.Level1);
        else
        {
			PlayerPrefs.SetInt(TUTORIAL, 0);
			PlayerPrefs.Save();

			SceneManager.LoadSceneAsync((int)Scenes.Tutorial);
        }
	}

	public void GotoTutorial() =>
		SceneManager.LoadSceneAsync((int)Scenes.Tutorial);

	public void GotoLevelScreen() =>
		SceneManager.LoadSceneAsync((int)Scenes.Menu);
}
