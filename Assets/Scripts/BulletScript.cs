using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
	[SerializeField] int damage;
	[SerializeField] float force;
	[SerializeField] GameObject EnemyObj; 
	private Vector3 mousePos;
	private Camera mainCam;
	private Rigidbody2D rb;

	void Awake()
	{
		EnemyObj = GameObject.FindGameObjectWithTag("Enemy");
		mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		rb = GetComponent<Rigidbody2D>();
		mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
		Vector3 direction = mousePos - transform.position;
		Vector3 rotation = transform.position - mousePos;
		rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
		float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0, 0, rot + 90); 
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Enemy")
		{
			EnemyObj.GetComponent<Enemy>().TakeDamage(damage);
		}
	}
}
