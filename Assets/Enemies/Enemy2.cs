using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float speed;
    [SerializeField] float distancebetween;
    [SerializeField] float closerange;
    [SerializeField] float starttimebetweenshots;
    [SerializeField] GameObject projectile;

    private float distance;
    private float timebetweenshots;
    private Transform Player;

    Coroutine c_REnemyFire;

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

        if (distance < distancebetween)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            timebetweenshots -= Time.deltaTime;
        }

        if (starttimebetweenshots >= distance)
        {
            //Instantiate(projectile, transform.position, Quaternion.identity);
            if(c_REnemyFire == null)
            {
                c_REnemyFire = StartCoroutine(C_EnemyFire());
                timebetweenshots = starttimebetweenshots;

            }
        }

    }

    IEnumerator C_EnemyFire ()
    {
        Instantiate(projectile, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        c_REnemyFire = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, closerange);
        Gizmos.DrawWireSphere(transform.position, distancebetween);
    }
}
