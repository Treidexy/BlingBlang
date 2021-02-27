using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mlogie : MonoBehaviour
{
	[SerializeField]
	private Vector2 m_MoveX;
	[SerializeField, Range(0, 1)]
	private float m_MoveSpeed;
	private float m_MovePos;

	private void FixedUpdate()
	{
		float xPos = Mathf.Lerp(m_MoveX.x, m_MoveX.y, (Mathf.Sin(m_MovePos) + 1) / 2);
		transform.position = new Vector2(xPos, transform.position.y);

		m_MovePos += m_MoveSpeed;
	}
}
