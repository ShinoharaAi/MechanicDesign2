using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
	public event Action<bool> OnGroundedChanged;

	private bool m_bGrounded;
	public bool IsGrounded { get { return m_bGrounded; } }
	Rigidbody2D m_rb;

	InputHandler m_InputHandler;

	[SerializeField] private Collider2D m_GroundCol;
	[SerializeField] private LayerMask m_GroundLayer;
	[SerializeField] private LayerMask m_wallLayer;

	private BoxCollider2D m_BoxCollider;

	private void Awake()
	{
		if (!m_GroundCol) m_GroundCol = GetComponent<Collider2D>();
		m_rb = GetComponent<Rigidbody2D>();
	}

	public void Update()
	{
		ContactFilter2D filter = new ContactFilter2D();
		filter.layerMask = m_GroundLayer;
		List<RaycastHit2D> results = new List<RaycastHit2D>();
		bool newGrounded = (m_GroundCol.Cast(Vector2.down, filter, results, 0.01f, true) > 0);

		if (m_bGrounded != newGrounded)
		{
			m_bGrounded = newGrounded;
			OnGroundedChanged?.Invoke(m_bGrounded);
		}

	}

	private bool isGrounded()
	{
		RaycastHit2D raycastHit = Physics2D.BoxCast(m_BoxCollider.bounds.center, m_BoxCollider.bounds.size, 0, Vector2.down, 0.1f, m_GroundLayer);
		return raycastHit.collider != null;
	}

	private bool onWall()
	{
		RaycastHit2D raycastHit = Physics2D.BoxCast(m_BoxCollider.bounds.center, m_BoxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, m_GroundLayer);
		return raycastHit.collider != null;
	}
} 
