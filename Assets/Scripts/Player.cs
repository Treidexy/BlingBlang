using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField]
	private float m_Speed;
	[SerializeField]
	private float m_AirBonus;
	[SerializeField]
	private float m_MlichieBounce;
	[SerializeField]
	private float m_GlavaBounce;

	private bool m_MouseOver;
	private bool m_Frozen;

	private Rigidbody2D m_Rigidbody;
	private Vector2 m_Velocity;

	private uint m_Collisions;
	private bool m_Colliding { get => m_Collisions > 0; }

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

		if (m_Frozen)
			m_Rigidbody.velocity = Vector2.zero;
		else
		{
			float velHor = Input.GetAxis("Horizontal");
			float velVer = Input.GetAxis("Vertical");
			if (velVer > 0)
				velVer = 0;

			float speedMul = m_Speed;
			if (!m_Colliding)
				speedMul += m_AirBonus;

			AddForce(new Vector2(velHor, velVer) * speedMul);
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

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("MlichieBottom"))
		{
			//transform.position += new Vector3 { y = GetComponent<Collider2D>().bounds.size.y };
			AddForce(new Vector2 { y = m_MlichieBounce });
		}
		else if (collision.gameObject.layer == LayerMask.NameToLayer("Mlichie"))
		{
			if (m_Rigidbody.velocity.y <= 0.01f && !m_Frozen)
				// TODO: Lose
				Debug.Log("Player died. :(");
		}
		else if (collision.gameObject.layer == LayerMask.NameToLayer("Glava"))
		{
			AddForce(new Vector2 { y = m_GlavaBounce });
		}

		m_Collisions++;
	}

	private void OnTriggerExit2D(Collider2D collision) =>
		m_Collisions--;

	private void AddForce(Vector2 force)
	{
		if (m_Frozen)
		{
			// Checky, but gets the job done
			m_Rigidbody.velocity = m_Velocity;
			m_Rigidbody.AddForce(force);
			m_Velocity = m_Rigidbody.velocity;
			m_Rigidbody.velocity = Vector2.zero;
		}
		else
			m_Rigidbody.AddForce(force);
	}
}
