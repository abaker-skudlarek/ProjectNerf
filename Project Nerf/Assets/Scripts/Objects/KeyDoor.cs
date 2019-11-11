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
  enemy
}

public class KeyDoor : GameObjectParent
{
  /***** Variables *****/

  /* -- Private -- */

  /* -- Public -- */
  public DoorType doorType; /* the type of door this is, based on enum above */
  public bool isOpen;       /* if the door is open or not */

  
  /***** Functions *****/



  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }
}
