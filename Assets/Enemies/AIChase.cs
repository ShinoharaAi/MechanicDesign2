using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
	[SerializeField] GameObject player;
	[SerializeField] float speed;
	[SerializeField] float distancebetween;
    [SerializeField] float retreatdistance;
    [SerializeField] float starttimebetweenshots;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject enemyspawn;
    [SerializeField] private AudioSource shootSoundEffect; 

    private float distance;
    private float timebetweenshots;
    private Transform Player;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
		Vector2 direction = player.transform.position - transform.position;
		direction.Normalize();

		if (distance > distancebetween)
		{
			transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
		}
		else if(distance < distancebetween && distance > retreatdistance)
		{
			transform.position = this.transform.position;
		}
		else if (distance < retreatdistance)
		{
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -speed * Time.deltaTime);
        }

        if(timebetweenshots <= 0)
        {
            shootSoundEffect.Play();    
            Instantiate(projectile, transform.position, Quaternion.identity);
            Instantiate(enemyspawn, transform.position, Quaternion.identity);

            timebetweenshots = starttimebetweenshots; 
        }
        else
        {
            timebetweenshots -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, timebetweenshots);
        Gizmos.DrawWireSphere(transform.position, starttimebetweenshots);
    }
}
