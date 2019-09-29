using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSlime : EnemyParent
{
    /***** Variables *****/

    /* -- Private -- */

    /* -- Public -- */
    public Transform target;       /* what the enemy is set to chase */
    public float chaseRadius;      /* the radius that the enemy will chase at */
    public float attackRadius;     /* the radius that the enemy will attack at */
    public Transform homePosition; /* where to move back to when the player
                                       moves out of radius from the enemy */

    /***** Functions *****/

    /**
     * Start()
     *
     * Built in Unity function. Start is called before the first frame update
     *
     */
    void Start()
    {
      /* get the location of the player (.transform just gets the location) */
      target = GameObject.FindWithTag("Player").transform;
    }

    /**
     * Update()
     *
     * Built in Unity function. Update is called every frame
     *
     */
    void Update()
    {
      checkDistance();
    }

    /**
     * checkDistance()
     *
     * This funtion checks the enemy distance with the player's (AKA target) and
     *  moves towards the player if they are within range
     *
     */
    void checkDistance()
    {
      /* if the distance is less than the chase radius AND greater than the attack radius */
      if(Vector3.Distance(target.position, transform.position) <= chaseRadius
          && Vector3.Distance(target.position, transform.position) > attackRadius)
      {
        /* move towards the player at a rate of the enemy's speed */
        transform.position = Vector3.MoveTowards(transform.position,
                                                 target.position,
                                                 enemyBaseSpeed * Time.deltaTime);
      }

    }


}
