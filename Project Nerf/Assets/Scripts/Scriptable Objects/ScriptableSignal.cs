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
    Debug.Log("scriptable signal raise() called");
    Debug.Log("Signal listener count: " + signalListeners.Count);

    for(int i = signalListeners.Count - 1; i >= 0; i--)
    {
      Debug.Log("before onSignalRaised");
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
    //TODO this is never called, could be the reason that the signal listeners list is empty
    
    Debug.Log("scriptable signal registerListener() called");

    signalListeners.Add(listener);
  }

  /**
   * deRegisterListener()
   *
   * Removes the signal from the signal listener list
   */
  public void deRegisterListener(SignalListener listener)
  {
    Debug.Log("deRegisterListener called");

    signalListeners.Remove(listener);
  }

}
