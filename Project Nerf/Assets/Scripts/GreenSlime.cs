/**
 *  @file SlimeEnemy.cs
 *
 *  @brief Defines the way the green slime will behave and interact with the
 *          world and the player.
 *
 *  @author: Alex Baker
 *  @date:   September 26 2019
 */

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSlime : EnemyParent
{
    /***** Variables *****/

    /* -- Private -- */
    private Animator slimeAnimator;

    /* -- Public -- */
    public float chaseRadius;      /* the radius that the enemy will chase at */
    public float attackRadius;     /* the radius that the enemy will attack at */
    public Transform homePosition; /* where to move back to when the player
                                       moves out of radius from the enemy */
    public Transform target;       /* what the enemy is set to chase */

    /***** Functions *****/

    /**
     * Start()
     *
     * Built in Unity function. Start is called before the first frame update
     *
     */
    void Start()
    {
      /* get the animator attached to the slime */
      slimeAnimator = GetComponent<Animator>();

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
      /* check the distance between the player and the slime, move towards player */
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

    /**
     * slimeDeath()
     *
     * Defines what happens when the slime dies. Death animation should play, it
     *  should stop moving, and be removed from the scene
     *
     */
    public void slimeDeath()
    {
      /* set the boolean for the death animation to be true */
      slimeAnimator.SetBool("isDead", true);

      /* if the slime is dead we want his model to stop moving so he doesn't
          continue to follow the player even when it's dead */
      enemyBaseSpeed = 0;

      /* start coroutine to remove the slime */
      StartCoroutine(removeSlime());

    }

    /**
     * removeSlime()
     *
     * This will be used to set the enemy as inactive when it dies, which will make
     *  it disappear
     *
     */
    private IEnumerator removeSlime()
    {
      /* wait for roughly the time of the animation */
      yield return new WaitForSeconds(1.3f);

      /* set the game object to inactive, making it invisible */
      this.gameObject.SetActive(false);

    }

}
