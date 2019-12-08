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
     enemyCount = enemies.Length - 1;
   }

  public void checkEnemies()
  {
    Debug.Log("checkenemies called");
    Debug.Log("enemies: " + enemyCount);

    /* check if all enemies in the room are dead */
    for(int i = 0; i < enemies.Length; i++)
    {
      if(enemies[i].gameObject.activeInHierarchy && i < enemies.Length - 1)
      {
        Debug.Log("returning");
        enemyCount--;

        if(enemyCount < 0)
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
    Debug.Log("openDoors called");

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
