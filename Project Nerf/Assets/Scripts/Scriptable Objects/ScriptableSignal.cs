using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScriptableSignal : ScriptableObject
{
  public List<SignalListener> signalListeners = new List<SignalListener>();


  public void raise()
  {
    for(int i = signalListeners.Count - 1; i >= 0; i--)
    {
      signalListeners[i].onSignalRaised();
    }
  }


  public void registerListener(SignalListener listener)
  {
    signalListeners.Add(listener);
  }

  public void deRegisterListener(SignalListener listener)
  {
    signalListeners.Remove(listener);
  }

}
