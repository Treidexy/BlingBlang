using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mlichie : MonoBehaviour
{
	/// <summary>
	/// X = Start y position
	/// Y = End y position
	/// </summary>
	[SerializeField]
	private Vector2 m_ThrompY;
	[SerializeField, Range(0, 1)]
	private float m_ThrompSpeed;
	private float m_ThrompPos;
	private bool m_Reverse;

	private void FixedUpdate()
	{
		float yPos = Mathf.Lerp(m_ThrompY.x, m_ThrompY.y, Mathf.Abs(m_ThrompPos + (m_Reverse ? -1 : 0)));
		transform.position = new Vector3(transform.position.x, yPos);

		m_ThrompPos += m_ThrompSpeed * (m_Reverse ? -1 : 1);
		if (m_ThrompPos >= 1 || m_ThrompPos <= 0)
			m_Reverse = !m_Reverse;
	}
}
