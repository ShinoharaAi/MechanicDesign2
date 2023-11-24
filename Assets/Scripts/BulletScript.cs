using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
	[SerializeField] int damage;
	[SerializeField] float force;
	private Vector3 mousePos;
	private Camera mainCam;
	private Rigidbody2D rb;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	public void Init(float rot)
	{

		transform.rotation = Quaternion.Euler(0,0, rot - 90);

		rb.AddForce(transform.up * force/Time.timeScale, ForceMode2D.Impulse);
	}

    private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Enemy")
		{
			Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            Vector2 enemyPosition = enemy.transform.position;

            if (enemy != null)
            {
				enemy.GetComponent<Enemy>().TakeDamage(damage);
                CameraShake.instance.ShakeCamera();
            }
			enemy.transform.position = enemyPosition;
        }

		StartCoroutine(C_BulletDisappear());
	}


	IEnumerator C_BulletDisappear()
	{
		yield return new WaitForSeconds(0.01f);
		Destroy(gameObject);
	}
}
