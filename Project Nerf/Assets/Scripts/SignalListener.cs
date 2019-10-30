/**
 *  @file SignalListener.cs
 *
 *  @brief Listens for signals and does things with them based on what type of
 *          signal it is and what it needs.
 *
 *  @author: Alex Baker
 *  @date:   October 6 2019
 */
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{
  /***** Variables *****/

  /* -- Private -- */

  /* -- Public -- */
  public ScriptableSignal signal; /* signal that is being listened to */
  public UnityEvent signalEvent;  /* event that will be called when the event occurs */

  /***** Functions *****/

  public void onSignalRaised()
  {
    Debug.Log("onSignalRaised called");

    signalEvent.Invoke();
  }

  private void onEnable()
  {
    Debug.Log("onEnable called");

    signal.registerListener(this);
  }

  private void onDisable()
  {
    Debug.Log("onDisable called");

    signal.deRegisterListener(this);
  }


}
