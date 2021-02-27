using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mlogie : MonoBehaviour
{
	[SerializeField]
	private Vector2 m_MoveX;
	[SerializeField]
	private Vector2 m_MoveY;
	[SerializeField, Range(0, 1)]
	private float m_MoveSpeed;
	private float m_MovePos;

	private void FixedUpdate()
	{
		float xPos = transform.position.x;
		float yPos = transform.position.y;
		if (m_MoveX.x != m_MoveX.y)
			xPos = Mathf.Lerp(m_MoveX.x, m_MoveX.y, (Mathf.Sin(m_MovePos) + 1) / 2);
		if (m_MoveY.x != m_MoveY.y)
			yPos = Mathf.Lerp(m_MoveY.x, m_MoveY.y, (Mathf.Sin(m_MovePos) + 1) / 2);
		transform.position = new Vector2(xPos, yPos);

		m_MovePos += m_MoveSpeed;
	}
}
