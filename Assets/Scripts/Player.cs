using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	[SerializeField]
	private PlayerSettings m_Settings;

	private Vector3 m_StartPosition;

	private float m_Speed { get => m_Settings.Speed; }
	private float m_AirBonus { get => m_Settings.AirBonus; }
	private float m_GlavaBounce { get => m_Settings.GlavaBounce; }
	private float m_MlogieBounce { get => m_Settings.MlogieBounce; }
	private float m_MlichieBounce { get => m_Settings.MlichieBounce; }

	private bool m_MouseOver;
	private bool m_Frozen;

	private Rigidbody2D m_Rigidbody;
	private Vector2 m_Velocity;

	private uint m_SafeCollisions;
	private bool m_CollidingSafe { get => m_SafeCollisions > 0; }

	private uint m_Collisions;
	private bool m_Colliding { get => m_Collisions > 0; }

	[SerializeField]
	private Text m_TimeText;
	private float m_Time;

	internal static int s_Level;

	private void Start() =>
		m_Rigidbody = GetComponent<Rigidbody2D>();

	private void Awake() =>
		m_StartPosition = transform.position;

    private void OnDestroy()
    {
		float oldTime = PlayerPrefs.GetFloat($"Level{s_Level}.Time", float.PositiveInfinity);
        PlayerPrefs.SetFloat($"Level{s_Level}.Time", Mathf.Min(m_Time, oldTime));
		PlayerPrefs.Save();
    }

    private void FixedUpdate()
	{
		if (Input.GetKey(KeyCode.R))
			Restart();

		if (m_MouseOver && Input.GetMouseButton(0) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
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

		if (!(m_TimeText is null))
			m_TimeText.text = m_Time.ToString("n2");
		m_Time += Time.deltaTime;
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
			AddForce(new Vector2 { y = m_MlichieBounce });
			m_SafeCollisions++;
		}
		else if (collision.gameObject.layer == LayerMask.NameToLayer("MlogieTop"))
		{
			AddForce(new Vector2 { y = m_MlogieBounce });
			m_SafeCollisions++;
		}
		else if (collision.gameObject.layer == LayerMask.NameToLayer("Mlichie"))
		{
			if (m_Rigidbody.velocity.y <= 0.01f && !m_Frozen && !m_CollidingSafe)
				Restart();
		}
		else if (collision.gameObject.layer == LayerMask.NameToLayer("Mlogie"))
		{
			if (m_Rigidbody.velocity.y <= 0.01f && !m_Frozen && !m_CollidingSafe)
				Restart();
		}
		else if (collision.gameObject.layer == LayerMask.NameToLayer("Glava"))
		{
			if (collision.GetComponent<Glava>().IsGood)
				AddForce(new Vector2 { y = m_GlavaBounce });
			else
				Restart();
		}
		else if (collision.gameObject.layer == LayerMask.NameToLayer("Goal") || collision.gameObject.layer == LayerMask.NameToLayer("AutoWin"))
			NextLevel();

		m_Collisions++;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("MlichieBottom") || collision.gameObject.layer == LayerMask.NameToLayer("MlogieTop"))
			m_SafeCollisions--;
		m_Collisions--;
	}

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

	private void NextLevel()
	{
		int lvl = ++s_Level + (int)Scenes.Level1;
		if (lvl > (int)Scenes.LevelLast)
			lvl = (int)Scenes.Playground;
		SceneManager.LoadSceneAsync(lvl);
	}

	private void Restart()
	{
		transform.position = m_StartPosition;
		m_Time = 0;
	}

	private void Lose() =>
		SceneManager.LoadSceneAsync((int)Scenes.Lose);
}
