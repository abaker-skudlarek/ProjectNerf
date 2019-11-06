/**
 *  @file ScriptableSignal.cs
 *
 *  @brief Manages signal. Can raise signals, register signals, deregister signals.
 *
 *  @author: Alex Baker
 *  @date:   October 6 2019
 */
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScriptableSignal : ScriptableObject
{
  /***** Variables *****/

  /* -- Private -- */

  /* -- Public -- */
  public List<SignalListener> signalListeners = new List<SignalListener>();

  /***** Functions *****/

  /**
   * raise()
   *
   * Loops through the list of signals and raises them in reverse order
   */
  public void raise()
  {
    for(int i = signalListeners.Count - 1; i >= 0; i--)
    {
      signalListeners[i].onSignalRaised();
    }
  }

  /**
   * registerListener()
   *
   * Adds the signal onto the signal listener list
   */
  public void registerListener(SignalListener listener)
  {
    signalListeners.Add(listener);
  }

  /**
   * deRegisterListener()
   *
   * Removes the signal from the signal listener list
   */
  public void deRegisterListener(SignalListener listener)
  {
    signalListeners.Remove(listener);
  }

}
