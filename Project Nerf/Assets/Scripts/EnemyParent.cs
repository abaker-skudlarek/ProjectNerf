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
     * Start()
     *
     * Built in Unity function. Start is called before the first frame update
     *
     */
    void Start()
    {

    }

    /**
     * Update()
     *
     * Built in Unity function. Update is called every frame
     *
     */
    void Update()
    {

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


}
