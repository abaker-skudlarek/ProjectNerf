/**
 *  @file KeyDoor.cs
 *
 *  @brief Defines the way that key doors operate and interact with the player.
 *          Inherits from the GameObjectParent class.
 *
 *  @author: Alex Baker
 *  @date:   November 10 2019
 */

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
  key,
  enemy,
  lever
}

public class Door : GameObjectParent
{
  /***** Variables *****/

  /* -- Private -- */

  /* -- Public -- */
  public Inventory playerInventory;   /* reference to the player's inventory */
  public SpriteRenderer doorSprite;   /* reference to the door's sprite */
  public BoxCollider2D doorCollider;  /* reference to the door's box collider */
  public DoorType doorType;           /* the type of door this is, based on enum above */
  public bool isOpen;                 /* if the door is open or not */


  /***** Functions *****/



  /**
   * Update()
   *
   * Built in Unity function. Update is called every frame
   */
  private void Update()
  {
    /* if the player presses the space key */
    if(Input.GetKeyDown(KeyCode.Space))
    {
      /* if the player is in range of the door and the door is a key door */
      if(playerInRange && doorType == DoorType.key)
      {
        /* if the player has more than 0 keys */
        if(playerInventory.keyCount > 0)
        {
          /* take away a key from the player */
          playerInventory.keyCount--;

          /* open the door */
          openDoor();
        }
      }
    }

  }

  /**
   * openDoor()
   *
   * "Opens" the door that this script is attached to. Really, it just turns off
   *  the sprite because we don't have any animation from the sprite sheet we have
   */
  public void openDoor()
  {
    /* disable the doors sprite, making it disappear */
    doorSprite.enabled = false;

    /* set the door to being "open" */
    isOpen = true;

    /* disable the doors collider, so the player doesn't run into nothing */
    doorCollider.enabled = false;
  }




}
