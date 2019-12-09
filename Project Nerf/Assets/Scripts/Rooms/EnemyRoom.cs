using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoom : Room
{
  /***** Variables *****/

  /* -- Private -- */

  /* -- Public -- */
  public Door[] doors;  /* doors in the room */
  public int enemyCount;


  /***** Functions *****/


  void Start()
  {
    enemyCount = enemies.Length;
  }

  public void checkEnemies()
  {
    /* check if all enemies in the room are dead */
    for(int i = 0; i < enemies.Length; i++)
    {
      if(enemies[i].gameObject.activeInHierarchy)
      {
        enemyCount--;

        if(enemyCount <= 0)
        {
          openDoors();
          return;
        }
        else
        {
          return;
        }

      }
    }
  }

  public void closeDoors()
  {
    for(int i = 0; i < doors.Length; i++)
    {
      doors[i].closeDoor();
    }
  }

  public void openDoors()
  {
    for(int i = 0; i < doors.Length; i++)
    {
      doors[i].openDoor();
    }
  }

  public override void OnTriggerEnter2D(Collider2D otherCollider)
  {
    if(otherCollider.CompareTag("Player") && !otherCollider.isTrigger)
    {
      /* activate all enemies */
      for(int i = 0; i < enemies.Length; i++)
      {
        if(enemies[i] != null)
        {
          flipActivation(enemies[i], true);
        }
      }

      /* don't close the doors if there aren't any enemies */
      if(enemyCount > 0)
      {
        /* close doors upon entering */
        closeDoors();
      }

    }
  }

  public override void OnTriggerExit2D(Collider2D otherCollider)
  {
    /*
    if(otherCollider.CompareTag("Player") && !otherCollider.isTrigger)
    {
      for(int i = 0; i < enemies.Length; i++)
      {
        if(enemies[i] != null)
        {
          flipActivation(enemies[i], false);
        }
      }
    }
    */
  }


}
