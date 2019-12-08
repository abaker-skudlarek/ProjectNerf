/**
 *  @file EnemyParent.cs
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
    public FloatValue enemyMaxHealth;   /* the max health of the enemy */
    public float enemyCurrHealth;       /* the current health of the enemy */
    public string enemyName;            /* the name of the enemy */
    public double enemyBaseAttack;      /* the base attack value of the enemy */
    public float enemyBaseSpeed;        /* the base move speed of the enemy */
    public EnemyState currentState;     /* current state of the enemy */
    public Vector2 homePosition;        /* position the enemy should go back to when active again */
    public ScriptableSignal roomSignal; /* signal to send on death */

    /***** Functions *****/


    /**
     * Awake()
     *
     * Built in Unity function. Called when the script instance is being loaded.
     *  Used to initialize any variables before the game starts.
     */
    private void Awake()
    {
      /* start the HP at the max HP the enemy can have */
      enemyCurrHealth = enemyMaxHealth.initialValue;
    }

    private void OnEnable()
    {
      /* set home position */
      transform.position = homePosition;
    }

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
     */
    public void startKnockback(Rigidbody2D enemyBody, float knockbackTime, float damage)
    {
      /* run the coroutine for the knockback */
      StartCoroutine(knockbackCoroutine(enemyBody, knockbackTime));

      /* take damage after the hit */
      takeDamage(damage);
    }

    /**
     * knockbackCoroutine(Rigidbody2D, float)
     *
     * Coroutine that runs when an enemy is knocked back after being hit by the
     *  player
     *
     * @param enemyBody: The rigid body of the enemy that is being knocked back
     * @param knockbackTime:  The amount of time that we want the enemy be knocked back for
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

    /**
     * takeDamage(float)
     *
     * This applies the damage to the enemy that is taken when they are hit by
     *  the player. If the damage is enough to bring them to or below 0, they
     *  are dead and need to call their death function
     *
     * @param damage: The amount of damage that the hit does, will be subtracted
     *                  from the HP of the enemy
     */
    public virtual void takeDamage(float damage)
    {
      /* this function is overridden by the child classes so they can call their
          own death handlers */
    }


}
