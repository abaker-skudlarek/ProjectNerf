/**
 *  @file FloatValue.cs
 *
 *  @brief This file is going to be inherited from ScriptableObject (explained below).
 *          It will be used to transfer values between scenes and give me an easy
 *          way to assign things like HP to many object at once. For example,
 *          I can create a float value for all 3 HP enemies and attach it to every enemy
 *          that I want to have 3 HP.
 *
 *  @author: Alex Baker
 *  @date:   October 6 2019
 */

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class is inheriting from ScriptableObject, which means that this script
 *  cannot be attached to anything in the scene. Instead, it lives "outside"
 *  the scene, which means that we can assign values to it that can be read
 *  through MULTIPLE scenes, instead of being tied to just one. This will not
 *  be reset when the scene is reloaded. ScriptableObjects don't get start
 *  or update functions. */

[CreateAssetMenu] /* this allows us to create this as an object by right clicking */
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{

  /***** Variables *****/

  /* -- Private -- */

  /* -- Public -- */

  public float initialValue; /* initial value of the float */

  [HideInInspector] /* means it won't appear in inspector */
  public float runtimeValue; /* running value of the float */


  /***** Functions *****/

  /**
   * OnAfterDeserialize()
   *
   * Needed to inherit from ISerializationCallbackReceiver. Allows scriptable
   *  objects to be set from the start, similar to the start function on objects.
   * If we didn't do this, things like playerHP would stay 0 when they die,
   *  even after a new game has been started.
   */
  public void OnAfterDeserialize()
  {
    /* reset the float to it's initial value on every new game reset */
    runtimeValue = initialValue;
  }

  /**
   * OnBeforeSerialize()
   *
   * Needed to inherit from ISerializationCallbackReceiver. Allows scriptable
   *  objects to be set from the start, similar to the start function on objects.
   * If we didn't do this, things like playerHP would stay 0 when they die,
   *  even after a new game has been started.
   */
  public void OnBeforeSerialize()
  {

  }


}
