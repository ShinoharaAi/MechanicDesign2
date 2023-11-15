using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
	[SerializeField] public float maxHealth = 100;
	[SerializeField] public float currentHealth;

	[SerializeField] Image Fill;

    void Start()
    {
		currentHealth = maxHealth;
    }

	private void Update()
	{
		Fill.fillAmount = currentHealth / maxHealth;
	}

	public void TakeDamage(int amount)
	{
		currentHealth -= amount;
		if (currentHealth <= 0)
		{
			Destroy(gameObject);
		}
	}

	public void Heal(int amount)
	{
		currentHealth += amount;
		if (currentHealth >= 0)
		{
			currentHealth = maxHealth; 
		}
	}

}
