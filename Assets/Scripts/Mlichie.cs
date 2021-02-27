using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mlichie : MonoBehaviour
{
	[SerializeField]
	private Vector2 m_ThrompX;
	[SerializeField]
	private Vector2 m_ThrompY = new Vector2(-1, -4);
	[SerializeField, Range(0, 1)]
	private float m_ThrompSpeed = 0.03f;
	private float m_ThrompPos;

	private void FixedUpdate()
	{
		float xPos = transform.position.x;
		float yPos = transform.position.y;
		if (m_ThrompX.x != m_ThrompX.y)
			xPos = Mathf.Lerp(m_ThrompX.x, m_ThrompX.y, (Mathf.Sin(m_ThrompPos) + 1) / 2);
		if (m_ThrompY.x != m_ThrompY.y)
			yPos = Mathf.Lerp(m_ThrompY.x, m_ThrompY.y, (Mathf.Sin(m_ThrompPos) + 1) / 2);
		transform.position = new Vector2(xPos, yPos);

		m_ThrompPos += m_ThrompSpeed;
	}
}
