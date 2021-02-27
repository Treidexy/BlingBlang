using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glava : MonoBehaviour
{
	public bool IsGood { get; private set; }

	[SerializeField]
	private GlavaSettings m_Settings;

	[SerializeField]
	private float m_MarinationSpeed = 0.02f;
	private float m_Marination;

	private Color m_GoodColor { get => m_Settings.GoodColor; }
	private Color m_BadColor { get => m_Settings.BadColor; }

    private void FixedUpdate()
	{
		float marSin = Mathf.Sin(m_Marination);
		IsGood = marSin > 0;

		GetComponent<SpriteRenderer>().color = Color.Lerp(m_GoodColor, m_BadColor, Mathf.Abs((marSin - 1) / 2));

		m_Marination += m_MarinationSpeed;
	}
}
