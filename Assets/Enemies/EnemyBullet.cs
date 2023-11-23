using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
	[SerializeField] float speed;
    [SerializeField] int damage;
    private Transform player;
	private Vector2 target;

    private void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		target = new Vector2(player.position.x, player.position.y);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
		if(transform.position.x == target.x && transform.position.y == target.y)
		{
			DestroyProjectile(); 
		}
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
		{
			DestroyProjectile();
		}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Health health = collision.transform.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
                DestroyProjectile();
            }
        }
    }

    private void DestroyProjectile()
    {
		Destroy(gameObject); 
    }

}
