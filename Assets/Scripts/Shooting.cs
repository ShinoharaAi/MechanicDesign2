using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
	public bool canfire;
	public bool isShooting;
	public float timerbetweenfiring;

	private float timer;
	private float rotZ;
	private Vector3 mousePos;

	private Camera mainCam;
	public GameObject Bullet;
	public Transform BulletTransform;
	Coroutine c_RBulletDisappear;

	void Awake()
	{
		mainCam = FindAnyObjectByType<Camera>();
	}

	void Update()
	{
		mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
		Vector3 rotation = mousePos - transform.position;
		rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

		transform.rotation = Quaternion.Euler(0, 0, rotZ);

		if (!canfire)
		{
			timer += Time.unscaledDeltaTime;
			if(timer > timerbetweenfiring)
			{
				canfire = true;
				timer = 0;

			}
		}
	}

	public void Shoot()
	{
		if (canfire)
		{
			GameObject bullet = Instantiate(Bullet, BulletTransform.position, Quaternion.identity);
			bullet.GetComponent<BulletScript>().Init(rotZ);
			canfire = false;
		}
	}
}
