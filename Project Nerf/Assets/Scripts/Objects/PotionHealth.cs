/**
 *  @file PotionHealth.cs
 *
 *  @brief Defines a health potion and the way that it interacts with the player
 *          and the world. Inherits from ConsumableParent
 *
 *  @author: Alex Baker
 *  @date:   Novermber 13 2019
 */

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHealth : ConsumableParent
{
  /***** Variables *****/

  /* -- Private -- */

  /* -- Public -- */
  public FloatValue heartContainers; /* the amount of heart containers the player has */
  public FloatValue playerHealth;    /* float value reference to the player's health */
  public float healthIncrease;       /* amount that we are going to increase the health with the potion */

  /***** Functions *****/


  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  /**
   * OnTriggerEnter2D(Collider2d)
   *
   * Executes when this object is triggered. This function will check to see if the
   *  player is in the trigger zone for the potion and if so, increase the player's
   *  health by one full heart and raise the signal for the consumable
   *
   * @param otherCollider: The thing that is colliding into the object this is
   *                        attached to. The code should only work if it's the player
   */
  public void OnTriggerEnter2D(Collider2D otherCollider)
  {

    /* we only want the player to be able to trigger this code */
    if(otherCollider.CompareTag("Player") && !otherCollider.isTrigger)
    {
      /* increase the runtime value of the player's health by the amount to increase */
      playerHealth.runtimeValue += healthIncrease;

      /* if the increase would increase the amount of health past the maximum the
          player is allowed, just set it equal to the maximum. This is just a
          health potion, not a new health container */
      if(playerHealth.initialValue > heartContainers.runtimeValue * 2f)
      {
        playerHealth.initialValue = heartContainers.runtimeValue * 2f;
      }

      /* raise the signal for the consumable */
      consumableSignal.raise();

      /* destroy the potion after it has been used so it is removed from the game */
      Destroy(this.gameObject);
    }

  }


}
