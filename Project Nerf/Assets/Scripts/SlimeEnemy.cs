/**
 *  @file SlimeEnemy.cs
 *
 *  @brief Defines the way the enemey slime will behave and interact with the
 *          world and the player.
 *
 *  @author: Alex Baker
 *  @date:   September 26 2019
 */

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{

    /***** Variables *****/

    /* -- Private -- */
    private Animator slimeAnimator;

    /* -- Public -- */


    /***** Functions *****/

    /**
     * Start()
     *
     * Built in Unity function. Start is called before the first frame update
     *
     */
    void Start()
    {
      slimeAnimator = GetComponent<Animator>();
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
     * SlimeDeath()
     *
     * Defines what happens when the slime dies. We will want the death animation
     *  to play
     *
     */
    public void slimeDeath()
    {
      /* set the boolean for the death animation to be true */
      slimeAnimator.SetBool("isDead", true);

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
