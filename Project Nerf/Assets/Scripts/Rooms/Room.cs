using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
  /***** Variables *****/

  /* -- Private -- */

  /* -- Public -- */
  public EnemyParent[] enemies;   /* enemies in the room */


  /***** Functions *****/


  public virtual void OnTriggerEnter2D(Collider2D otherCollider)
  {
    if(otherCollider.CompareTag("Player") && !otherCollider.isTrigger)
    {
      /* activate all enemies */
      for(int i = 0; i < enemies.Length; i++)
      {
        flipActivation(enemies[i], true);
      }

    }
  }

  public virtual void OnTriggerExit2D(Collider2D otherCollider)
  {
    if(otherCollider.CompareTag("Player") && !otherCollider.isTrigger)
    {
      /* deactivate all enemies */
      for(int i = 0; i < enemies.Length; i++)
      {
        flipActivation(enemies[i], false);
      }
    }
  }

  public void flipActivation(Component component, bool isActive)
  {
    component.gameObject.SetActive(isActive);
  }



}
