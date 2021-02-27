using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField]
	private float m_Speed;
	private Rigidbody2D m_Rigidbody;

	private bool m_MouseOver;
	private bool m_Frozen;

	private Vector2 m_Velocity;

	private void Start()
	{
		m_Rigidbody = GetComponent<Rigidbody2D>();
	}

    private void FixedUpdate()
	{
		if (m_MouseOver && Input.GetMouseButton(0))
		{
			if (!m_Frozen)
				OnMouseDown();
		}
		else if (m_Frozen)
			OnMouseUp();

		if (!m_Frozen)
		{
			float velHor = Input.GetAxis("Horizontal");
			float velVer = Input.GetAxis("Vertical");

			m_Rigidbody.AddForce(new Vector2(velHor, velVer) * m_Speed);
		}
		else
		{
		}

		m_MouseOver = false;
	}

    private void OnMouseOver() =>
		m_MouseOver = true;

    private void OnMouseDown()
	{
		m_Frozen = true;

		// Save velocity
		m_Rigidbody.velocity = Vector2.zero;
		m_Velocity = m_Rigidbody.velocity;
	}

	private void OnMouseUp()
	{
		m_Frozen = false;

		// Restore velocity
		m_Rigidbody.velocity = m_Velocity;
	}
}
