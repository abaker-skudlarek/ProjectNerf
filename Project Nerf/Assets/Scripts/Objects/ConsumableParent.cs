/**
 *  @file ConsumableParent.cs
 *
 *  @brief Parent class for everything that will be a consumable in the game
 *          world. Mainly used for potions but could be for other things later.
 *
 *  @author: Alex Baker
 *  @date:   Novermber 13 2019
 */

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableParent : MonoBehaviour
{
  /***** Variables *****/

  /* -- Private -- */

  /* -- Public -- */
  public ScriptableSignal consumableSignal; /* signal for the consumable */

  /***** Functions *****/


  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }
}
