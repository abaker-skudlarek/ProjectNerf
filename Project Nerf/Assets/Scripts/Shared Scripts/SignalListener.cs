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

  /**
   * onSignalRaised()
   *
   * Invokes the signal using the signalEvent
   */
  public void onSignalRaised()
  {
    signalEvent.Invoke();
  }

  /**
   * OnEnable()
   *
   * Built in Unity function. Registers the signal that is passed
   */
  private void OnEnable()
  {
    signal.registerListener(this);
  }

  /**
   * OnDisable()
   *
   * Built in Unity function. Deregisters the signal that is passed
   */
  private void OnDisable()
  {
    signal.deRegisterListener(this);
  }


}
