using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] PlayerInput m_PlayerInput;
	[SerializeField] Shooting shooter; 

    public bool m_b_InJumpActive;
    public bool m_b_InMoveActive;
	private float m_f_InMoveRequest;
	public bool m_b_InSlideActive;

	public Animator animator; 


	Rigidbody2D rb;

    public Coroutine c_RMove;
	public Coroutine c_RJump;
    public Coroutine c_RSlomo;

	Ghost ghost; 
    PlayerMovement PlayerMovement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        PlayerMovement = GetComponent<PlayerMovement>();
        m_PlayerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        m_PlayerInput.actions.FindAction("Movement").performed += Handle_MovePerformed;
        m_PlayerInput.actions.FindAction("Movement").canceled += Handle_MoveCancelled;

        m_PlayerInput.actions.FindAction("Jump").performed += Handle_JumpPerformed;
        m_PlayerInput.actions.FindAction("Jump").canceled += Handle_JumpCancelled;

        m_PlayerInput.actions.FindAction("Shoot").performed += Handle_ShootPerformed;
		m_PlayerInput.actions.FindAction("Shoot").canceled += Handle_ShootCancelled;

		m_PlayerInput.actions.FindAction("Dash").performed += Handle_DashPerformed;
		//m_PlayerInput.actions.FindAction("Dash").canceled += Handle_DashCancelled;

		m_PlayerInput.actions.FindAction("Slide").performed += Handle_SlidePerformed;
		m_PlayerInput.actions.FindAction("Slide").canceled += Handle_SlideCancelled;

        m_PlayerInput.actions.FindAction("SlowMotion").performed += Handle_SlowMotionPerformed;
        m_PlayerInput.actions.FindAction("SlowMotion").canceled += Handle_SlowMotionCancelled;

    }

	private void OnDisable()
    {
        m_PlayerInput.actions.FindAction("Movement").performed -= Handle_MovePerformed;
        m_PlayerInput.actions.FindAction("Movement").canceled -= Handle_MoveCancelled;

        m_PlayerInput.actions.FindAction("Jump").performed -= Handle_JumpPerformed;
        m_PlayerInput.actions.FindAction("Jump").canceled -= Handle_JumpCancelled;

        m_PlayerInput.actions.FindAction("Shoot").performed -= Handle_ShootPerformed;
		m_PlayerInput.actions.FindAction("Shoot").canceled -= Handle_ShootCancelled;

		m_PlayerInput.actions.FindAction("Dash").performed -= Handle_DashPerformed;
        //m_PlayerInput.actions.FindAction("Dash").canceled -= Handle_DashCancelled;

        m_PlayerInput.actions.FindAction("Slide").performed -= Handle_SlidePerformed;
		m_PlayerInput.actions.FindAction("Slide").canceled -= Handle_SlideCancelled;


		m_PlayerInput.actions.FindAction("SlowMotion").performed -= Handle_SlowMotionPerformed;
        m_PlayerInput.actions.FindAction("SlowMotion").canceled -= Handle_SlowMotionCancelled;
    }

	private void Handle_MovePerformed(InputAction.CallbackContext context)
    {
        m_b_InMoveActive = true;
		m_f_InMoveRequest = context.ReadValue<float>();

		PlayerMovement.Move(m_f_InMoveRequest);
        if (c_RMove == null)
        {
            c_RMove = StartCoroutine(C_MoveUpdate());
        }

		ghost.makeGhost = true;
		animator.SetBool("Moving", true);
	}

    private void Handle_MoveCancelled(InputAction.CallbackContext context)
    {
        m_b_InMoveActive = false;
		m_f_InMoveRequest = 0f;

		if (c_RMove != null)
        {
            StopCoroutine(c_RMove);
            c_RMove = null;
        }

		PlayerMovement.Move(m_f_InMoveRequest);

		ghost.makeGhost = false;
		animator.SetBool("Moving", false);
	}

	private void Handle_SlidePerformed(InputAction.CallbackContext context)
	{
		m_b_InSlideActive = true; 
	}

	private void Handle_SlideCancelled(InputAction.CallbackContext context)
	{
		m_b_InSlideActive = false;
	}

	private void Handle_ShootPerformed(InputAction.CallbackContext context)
	{
		shooter.isShooting = true;
		shooter.Shoot();
	}

	private void Handle_ShootCancelled(InputAction.CallbackContext context)
	{
		shooter.isShooting = false;
	}
    private void Handle_SlowMotionPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("slowmo STARTED");
        if (c_RSlomo == null)
        {
            c_RSlomo = StartCoroutine(C_SlowMotion());
        }
    }

    private void Handle_SlowMotionCancelled(InputAction.CallbackContext context)
    {
        if (c_RSlomo != null)
        {
            StopCoroutine(C_SlowMotion());
            c_RSlomo = null;
            Debug.Log("Slowo CR stopped");
        }
    }

    private void Handle_JumpPerformed(InputAction.CallbackContext context)
    {
        m_b_InJumpActive = true;
	    rb.gravityScale = 0.75f;
		if (c_RJump == null)
		{
			c_RJump = StartCoroutine(C_JumpUpdate());	
		}
    }

    private void Handle_JumpCancelled(InputAction.CallbackContext context)
    {
        m_b_InJumpActive = false;
		PlayerMovement.isJumping = false;
		rb.gravityScale = 5f;
		if(c_RJump != null)
		{
			StopCoroutine(c_RJump); 
			c_RJump = null;	
		}

	}

	private void Handle_DashPerformed(InputAction.CallbackContext context)
	{
		PlayerMovement.Dasher();
		//PlayerMovement.Flip();
	}

	IEnumerator C_MoveUpdate()
    {
        while (m_b_InMoveActive)
        {
            PlayerMovement.Move(m_f_InMoveRequest);
            yield return null;
        }
    }

	IEnumerator C_JumpUpdate()
	{
		PlayerMovement.PlayerJump();
		yield return null;
	}

    IEnumerator C_SlowMotion()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(2f);
        Time.timeScale = 1.0f;
    }
}
