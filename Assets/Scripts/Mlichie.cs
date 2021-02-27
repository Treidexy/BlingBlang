using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mlichie : MonoBehaviour
{
	[SerializeField]
	private Vector2 m_ThrompY = new Vector2(-1, -4);
	[SerializeField, Range(0, 1)]
	private float m_ThrompSpeed = 0.03f;
	private float m_ThrompPos;

	private void FixedUpdate()
	{
		float yPos = Mathf.Lerp(m_ThrompY.x, m_ThrompY.y, (Mathf.Sin(m_ThrompPos) + 1) / 2);
		transform.position = new Vector2(transform.position.x, yPos);

		m_ThrompPos += m_ThrompSpeed;
	}
}
