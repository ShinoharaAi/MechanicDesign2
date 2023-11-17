using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
	private Camera mainCam;
	private Vector3 mousePos;
	public GameObject Bullet;
	public Transform BulletTransform;
	public bool canfire;
	private float timer;
	public float timerbetweenfiring;

	void Start()
	{
		mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}

	void Update()
	{
		mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
		Vector3 rotation = mousePos - transform.position;
		float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0, 0, rotZ);

		if(!canfire)
		{
			timer += Time.deltaTime;
			if(timer > timerbetweenfiring)
			{
				canfire = true;
				timer = 0;

			}
		}

		if(Input.GetMouseButton(0) && canfire)
		{
			canfire = false;
			Instantiate(Bullet, BulletTransform.position, Quaternion.identity);
		}

	}
}
