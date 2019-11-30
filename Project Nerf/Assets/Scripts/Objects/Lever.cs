/**
 *  @file Lever.cs
 *
 *  @brief Defines the lever that will be associated with a door in the world.
 *          When the player walks over the lever it opens the door.
 *
 *  @author: Alex Baker
 *  @date:   Novermber 15 2019
 */

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
  /***** Variables *****/

  /* -- Private -- */
  private SpriteRenderer sprite;  /* reference to the sprite renderer */

  /* -- Public -- */
  public bool isActive;           /* whether the switch is active or not */
  public BoolValue storedValue;   /* "running" value */
  public Sprite activeSprite;     /* sprite that is currently being displayed */
  public Door thisDoor;           /* reference to the door */

  /***** Functions *****/


  /**
   * Start()
   *
   * Built in Unity function. Start is called before the first frame update
   */
  void Start()
  {
    /* get the sprite renderer */
    sprite = GetComponent<SpriteRenderer>();

    /* set the active state equal to the current value of the stored value */
    isActive = storedValue.runtimeValue;

    /* if it's active at the start, activate the lever. good for switching scenes */
    if(isActive)
    {
      activateLever();
    }
  }

  /**
   * OnTriggerEnter2D(Collider2d)
   *
   * Executes when this object is triggered. This function will check to see if the
   *  player is in the trigger zone and call activateLever() if so
   *
   * @param otherCollider: The thing that is colliding into the object this is
   *                        attached to
   */
  public void OnTriggerEnter2D(Collider2D otherCollider)
  {
    /* if the player is in the trigger zone */
    if(otherCollider.CompareTag("Player"))
    {
      activateLever();
    }
  }

  /**
   * activateLever()
   *
   * "Activates" the lever and opens the door that it is connected to.
   */
  public void activateLever()
  {
    /* set the lever to active */
    isActive = true;
    storedValue.runtimeValue = isActive;

    /*  open the door */
    thisDoor.openDoor();

    /* change sprite to the active sprite */
    sprite.sprite = activeSprite;
  }
}
