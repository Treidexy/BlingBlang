using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
	[SerializeField]
	private GameObject m_Canvas;
	[SerializeField]
	private GameObject m_Prefab;
	[SerializeField]
	private Vector2 m_Start;
	[SerializeField]
	private Vector2 m_End;
	[SerializeField]
	private Vector2Int m_Grid;
	[SerializeField]
	private uint m_Levels;

    private void Start()
    {
		uint lvl = 1;
		for (uint y = 0; y < m_Grid.y; y++)
			for (uint x = 0; x < m_Grid.x; x++, lvl++)
            {
				if (lvl > m_Levels)
					return;
				GameObject obj = Instantiate(m_Prefab, m_Canvas.transform);
				obj.GetComponent<RectTransform>().position = new Vector2(Mathf.Lerp(m_Start.x, m_End.x, (float)m_Grid.x / x), Mathf.Lerp(m_Start.y, m_End.y, (float)m_Grid.y / y));
				obj.GetComponentInChildren<Text>().text = lvl.ToString();
				Debug.Log($"Level: {lvl}, Text: {lvl}");
			}
	}
}
