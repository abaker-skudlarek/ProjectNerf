/**
 *  @file EnemyAI.cs
 *
 *  @brief Parent class for all enemies. Defines the basic characteristics of
 *          what every enemy should have
 *
 *  @author: Alex Baker
 *  @date:   September 29 2019
 */

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* state machine for our player that defines the states the player can be in */
public enum EnemyState
{
  idle,
  walking,
  attacking,
  staggered
}

public class EnemyParent : MonoBehaviour
{
    /***** Variables *****/

    /* -- Private -- */

    /* -- Public -- */
    public double enemyMaxHealth;   /* the max health of the enemy */
    public double enemyCurrHealth;  /* the current health of the enemy */
    public string enemyName;        /* the name of the enemy */
    public double enemyBaseAttack;  /* the base attack value of the enemy */
    public float enemyBaseSpeed;    /* the base move speed of the enemy */
    public EnemyState currentState; /* current state of the enemy */

    /***** Functions *****/

    /**
     * changeState(EnemyState, EnemyState)
     *
     * Checking the current state against the requested state, returns the new state
     *  if they are different. This prevents changing state for no reason if the
     *  current state is the same as the one we want
     *
     * @param currentState: The state the enemy is currently in
     * @param newState:  The state we want the enemy to be in
     *
     * @return newState: Returns the new state if they are different
     * @return currentState: Returns the current state if they are the same
     */
    public EnemyState changeState(EnemyState currentState, EnemyState newState)
    {
        if(currentState != newState)
        {
          return newState;
        }

        /* return current state if they are the same */
        return currentState;
    }

    /**
     * startKnockback(Rigidbody2D, float)
     *
     * Starts the knockback coroutine
     *
     * @param enemyBody: The rigid body of the enemy that is being knocked back
     * @param knockbackTime:  The amount of time that we want the enemy be knocked back for
     *
     */
    public void startKnockback(Rigidbody2D enemyBody, float knockbackTime)
    {
      /* run the coroutine for the knockback */
      StartCoroutine(knockbackCoroutine(enemyBody, knockbackTime));
    }

    /**
     * knockbackCoroutine(Rigidbody2D, float)
     *
     * Coroutine that runs when an enemy is knocked back after being hit by the
     *  player
     *
     * @param enemyBody: The rigid body of the enemy that is being knocked back
     * @param knockbackTime:  The amount of time that we want the enemy be knocked back for
     *
     */
    private IEnumerator knockbackCoroutine(Rigidbody2D enemyBody, float knockbackTime)
    {
      /* as long as there is an enemy and its not already staggered */
      if(enemyBody != null)
      {
        /* wait for the amount of time for the knockback */
        yield return new WaitForSeconds(knockbackTime);

        /* set the enemy's velocity to zero after the knockback is done */
        enemyBody.velocity = Vector2.zero;

        /* after the knockback the enemy should be idle */
        currentState = EnemyState.idle;

        /* this prevents the enemy from sliding back forever */
        enemyBody.velocity = Vector2.zero;
      }
    }

}
