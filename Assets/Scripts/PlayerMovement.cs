using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] float m_f_MoveSpeed;
	[SerializeField] public float m_f_JumpForce;
	[SerializeField] Transform m_f_CastPosition;
	[SerializeField] float m_f_CircleRadius;
	[SerializeField] LayerMask m_LayerMask;
	[SerializeField] float dashingPower = 24f;
	[SerializeField] float dashingTime = 0.2f;
	[SerializeField] float dashingCooldown = 1f;


	private float FallApex = 2.5f;
	//private float LowApex = 2f;

	float jumpTime;
	float jumpStartTime;

	Grounded GroundedScr;
	InputHandler InputHandlerScr;

	private bool canDash = true;
	private bool isDashing;
	public bool isJumping; 
	bool m_bRecentJump;
	bool m_bJumpBuffering;
	bool m_bCoyoteTime;
	bool m_b_InMoveActive;
	Coroutine c_RMove;
	Coroutine CoyoteTimeCounter;
	Coroutine JumpBufferingCounter;

	public Rigidbody2D m_rb;

	private float m_f_MoveRequest;

	private void Awake()
	{
		m_rb = GetComponent<Rigidbody2D>();
		GroundedScr = GetComponent<Grounded>();
	}

	public void FixedUpdate()
	{
		if (m_rb.velocity.y < 0)
		{
			m_rb.velocity += Vector2.up * Physics2D.gravity.y * (FallApex - 1) * Time.deltaTime;
		}
	}

	private void OnEnable()
	{
		GroundedScr.OnGroundedChanged += Handle_GroundedChanged;
	}

	private void OnDisable()
	{
		GroundedScr.OnGroundedChanged -= Handle_GroundedChanged;
	}

	//void FixedUpdate()
	//{
	//	//isGrounded = Physics2D.CircleCast(m_CastPosition.position, m_f_CircleRadius, Vector2.zero, 0, m_LayerMask);
	//}

	private void Handle_GroundedChanged(bool grounded)
	{
		if (grounded)
		{
			if (m_bJumpBuffering)
			{
				Debug.Log("JumpBuffering");
				StopCoroutine(JumpBufferingCounter);
				m_bJumpBuffering = false;
				m_rb.velocity = Vector2.right * Vector2.Dot(Vector2.right, m_rb.velocity);
				Jump();
			}
			if(m_f_MoveRequest == 0.0f) 
			{
				m_rb.velocity = new Vector2(0, m_rb.velocity.y);
			}
		}
		else if (!m_bRecentJump)
		{
			CoyoteTimeCounter = StartCoroutine(CR_CoyoteTime());
		}
	}

    public void PlayerJump()
    {
        if (GroundedScr.IsGrounded)
        {
            if (!m_bJumpBuffering && !m_bRecentJump)
            {
                Debug.Log("Jump");
                Jump();
            }
        }
        else if (m_bCoyoteTime)
        {
            Debug.Log("JumpCoyote");
            Jump();
            StopCoroutine(CoyoteTimeCounter);
            m_bCoyoteTime = false;
        }
        else
        {
            if (m_bJumpBuffering)
            {
                StopCoroutine(JumpBufferingCounter);
            }

            JumpBufferingCounter = StartCoroutine(CR_JumpBuffering());
        }
    }

    public void Jump()
	{
		m_rb.AddForce(Vector2.up * m_f_JumpForce, ForceMode2D.Impulse);
		StartCoroutine(CR_JumpBlindness());
		Debug.Log($"Launching from : {m_rb.velocity.y}");
	}

	public void Move(float moveRequest)
	{
		m_f_MoveRequest = moveRequest;
		m_rb.velocity = new Vector2(m_f_MoveRequest * m_f_MoveSpeed, m_rb.velocity.y);
		m_rb.AddForce(transform.right * m_f_MoveRequest * m_f_MoveSpeed * 0.1f, ForceMode2D.Impulse); 
	}

    public void Dasher()
    {
        StartCoroutine(C_Dash());
    }


    IEnumerator C_Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = m_rb.gravityScale;
        m_rb.gravityScale = 0f;
        m_rb.velocity = new Vector2(transform.localScale.x * m_f_MoveRequest * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        m_rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    IEnumerator CR_CoyoteTime()
	{
		m_bCoyoteTime = true;
		yield return new WaitForSeconds(0.5f);
		m_bCoyoteTime = false;
	}

	IEnumerator CR_JumpBuffering()
	{
		m_bJumpBuffering = true;
		yield return new WaitForSeconds(1f);
		m_bJumpBuffering = false;
	}

	IEnumerator CR_JumpBlindness()
	{
		m_bRecentJump = true;
		yield return new WaitForSeconds(0.1f);
		m_bRecentJump = false;
	}
}