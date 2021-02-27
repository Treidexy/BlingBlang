using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mlichie : MonoBehaviour
{
	[SerializeField]
	private MlichieSettings m_Settings;

	private Vector2 m_ThrompY { get => m_Settings.ThrompY; }
	private float m_ThrompSpeed { get => m_Settings.ThrompSpeed; }
	private float m_ThrompPos;

	private void FixedUpdate()
	{
		float yPos = Mathf.Lerp(m_ThrompY.x, m_ThrompY.y, (Mathf.Sin(m_ThrompPos) + 1) / 2);
		transform.position = new Vector2(transform.position.x, yPos);

		m_ThrompPos += m_ThrompSpeed;
	}
}
