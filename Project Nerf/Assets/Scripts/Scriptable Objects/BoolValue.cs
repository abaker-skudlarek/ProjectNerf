/**
 *  @file BoolValue.cs
 *
 *  @brief Same idea as FloatValue, but for bools instead of floats
 *
 *  @author: Alex Baker
 *  @date:   November 15 2019
 */

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoolValue : ScriptableObject, ISerializationCallbackReceiver
{

  /***** Variables *****/

  /* -- Private -- */

  /* -- Public -- */

  public bool initialValue; /* initial value of the bool */
  [HideInInspector]
  public bool runtimeValue; /* running value of the bool */

  /***** Functions *****/

  /**
   * OnAfterDeserialize()
   *
   * Needed to inherit from ISerializationCallbackReceiver. Allows scriptable
   *  objects to be set from the start, similar to the start function on objects.
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
   */
  public void OnBeforeSerialize()
  {

  }



}
