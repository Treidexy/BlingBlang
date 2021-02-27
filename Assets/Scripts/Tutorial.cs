using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
	[SerializeField]
	private GameObject[] m_Pages, m_PagesUI;
	private uint m_Index;

	private void Awake()
	{
		m_Pages[m_Index].SetActive(true);
		m_PagesUI[m_Index].SetActive(true);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			m_Pages[m_Index].SetActive(false);
			m_PagesUI[m_Index].SetActive(false);

			m_Index++;

			if (m_Index >= m_Pages.Length)
			{
				SceneManager.LoadSceneAsync((int)Scenes.Level1);
				return;
			}

			m_Pages[m_Index].SetActive(true);
			m_PagesUI[m_Index].SetActive(true);
		}
	}
}
