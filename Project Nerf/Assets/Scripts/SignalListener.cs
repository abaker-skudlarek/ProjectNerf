using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{

  public ScriptableSignal signal;
  public UnityEvent signalEvent;

  public void onSignalRaised()
  {
    signalEvent.Invoke();
  }

  private void onEnable()
  {
    signal.registerListener(this);
  }

  private void onDisable()
  {
    signal.deRegisterListener(this);
  }


}
