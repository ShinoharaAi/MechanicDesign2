using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] PlayerInput m_PlayerInput;

    public bool m_b_InJumpActive;
    public bool m_b_InMoveActive;
	private float m_f_InMoveRequest;

    Rigidbody2D rb;

    public Coroutine c_RMove;

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

    }

    private void OnDisable()
    {
        m_PlayerInput.actions.FindAction("Movement").performed -= Handle_MovePerformed;
        m_PlayerInput.actions.FindAction("Movement").canceled -= Handle_MoveCancelled;

        m_PlayerInput.actions.FindAction("Jump").performed -= Handle_JumpPerformed;
        m_PlayerInput.actions.FindAction("Jump").canceled -= Handle_JumpCancelled;

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
    }

    private void Handle_MoveCancelled(InputAction.CallbackContext context)
    {
        m_b_InMoveActive = false;
		m_f_InMoveRequest = 0f;

		PlayerMovement.Move(m_f_InMoveRequest);
		if (c_RMove != null)
        {
            StopCoroutine(c_RMove);
            c_RMove = null;
        }
    }

    private void Handle_JumpPerformed(InputAction.CallbackContext context)
    {
        m_b_InJumpActive = true;
        PlayerMovement.PlayerJump();

		//Hold down jump button = full height
		PlayerMovement.m_rb.velocity = new Vector2(PlayerMovement.m_rb.velocity.x, PlayerMovement.m_f_JumpForce);
    }

    private void Handle_JumpCancelled(InputAction.CallbackContext context)
    {
        m_b_InJumpActive = false;
		PlayerMovement.isJumping = false;

		//Light tap of Jump button = half of jump
		PlayerMovement.m_rb.velocity = new Vector2(PlayerMovement.m_rb.velocity.x, PlayerMovement.m_rb.velocity.y * 0.5f);
	}

	IEnumerator C_MoveUpdate()
    {
        while (m_b_InMoveActive)
        {
            PlayerMovement.Move(m_f_InMoveRequest);
            yield return null;
        }
    }
}
