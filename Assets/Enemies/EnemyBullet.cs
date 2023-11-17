using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
	[SerializeField] float speed;
	[SerializeField] float lineOfSite;
	[SerializeField] float shootingRange;
	[SerializeField] GameObject Bullet;
	[SerializeField] GameObject bulletParent;
	private Transform player;

    // Start is called before the first frame update
    void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distancefromplayer = Vector2.Distance(player.position, transform.position);
		if(distancefromplayer < lineOfSite)
		{
			transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
		}
		else if(distancefromplayer <= shootingRange)
		{
			Instantiate(Bullet, bulletParent.transform.position, Quaternion.identity);
		}
    }

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, lineOfSite);
		Gizmos.DrawWireSphere(transform.position, shootingRange);
	}
}
