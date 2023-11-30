using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] int damage;
	[SerializeField] float maxHealth = 100;
	[SerializeField] float currentHealth;
	[SerializeField] private AudioSource deathSoundEffect;

	void Start()
	{
		currentHealth = maxHealth;
	}

	public void TakeDamage(int amount)
	{
		currentHealth -= amount;
		if (currentHealth <= 0)
		{
			Destroy(this.gameObject);
		}
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			Health health = collision.transform.GetComponent<Health>();
            if (health != null)
            {
				health.TakeDamage(damage);
            }
		}

        deathSoundEffect.Play();
    }

}
