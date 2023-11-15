using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelaxedSemiSolid : MonoBehaviour
{
	private Collider2D p_collider;
	private bool p_platform;

    void Start()
    {
        p_collider = GetComponent<Collider2D>();
    }

	private void Update()
	{
		if (p_platform && Input.GetAxisRaw("Vertical") < 0);
		{
			p_collider.enabled = false;
			StartCoroutine(EnableCollider()); 
		}
	}

	private IEnumerator EnableCollider()
	{
		yield return new WaitForSeconds(0.5f);
		p_collider.enabled = true;
	}

	private void SetPlayerOnPlatform(Collision2D other, bool value)
	{
		var player = other.gameObject.GetComponent<PlayerMovement>();
		if (player != null)
		{
			p_platform = value;
		}
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		SetPlayerOnPlatform(other, true);
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		SetPlayerOnPlatform(other, true);
	}
}
