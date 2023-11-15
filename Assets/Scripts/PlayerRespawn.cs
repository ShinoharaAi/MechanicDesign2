using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
	[SerializeField] private AudioClip checkpointSounnd;
	private Transform currentCheckpoint;
	private Health Health; 

	private void Awake()
	{
		Health = GetComponent<Health>();
	}

	public void Respawn()
	{
		transform.position = currentCheckpoint.position;
		Health.Respawn();

		//Camera.main.GetComponent<Camera>().MoveToNewRoom(currentCheckpoint.parent);
	}

	private void OnTriggerEnter(Collider2D collision)
	{
		currentCheckpoint = collision.transform;
		collision.GetComponent<Collider>().enabled = false;
	}
}
