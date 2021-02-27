using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glava : MonoBehaviour
{
	public bool IsGood { get; private set; }

	[SerializeField]
	private GlavaSettings m_Settings;

	private float m_MarinationSpeed;
	private float m_Marination;

    private void Start()
    {
		m_MarinationSpeed = m_Settings.MarinationSpeed;
	}

    private void FixedUpdate()
	{
		float marSin = Mathf.Sin(m_Marination);
		IsGood = marSin > 0;

		GetComponent<SpriteRenderer>().color = Color.Lerp(Color.green, Color.white, Mathf.Abs((marSin - 1) / 2));

		m_Marination += m_MarinationSpeed;
	}
}
