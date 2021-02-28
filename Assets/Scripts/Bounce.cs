using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
	[SerializeField]
	private Vector2 m_Scales;
	[SerializeField]
	private float m_Speed;
	private float m_Pos;

	private void FixedUpdate()
	{
		transform.localScale = Vector2.one * (Mathf.Lerp(m_Scales.x, m_Scales.y, (Mathf.Sin(m_Pos) + 1) / 2));

		m_Pos += m_Speed;
	}
}
