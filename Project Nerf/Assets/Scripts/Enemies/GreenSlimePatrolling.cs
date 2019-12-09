/**
 *  @file GreenSlimePatrolling.cs
 *
 *  @brief Defines the way a gree slime enemy can patroll between two points
 *
 *
 *  @author: Alex Baker
 *  @date:   November 6 2019
 */

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSlimePatrolling : GreenSlime
{

    /***** Variables *****/

    /* -- Private -- */

    /* -- Public -- */
    public Transform[] path;     /* array of patrol points */
    public Transform nextPoint;  /* next point to travel to */
    public int currPoint;        /* current point the slime is at */
    public float roundingError;  /* the rounding "error" that we can have with our vector math */

    /***** Functions *****/

    /**
     * checkDistance()
     *
     * This funtion checks the enemy distance with the player's (AKA target) and
     *  moves towards the player if they are within range. If the player isn't
     *  in range, the slime will move between it's patrolling points
     */
    public override void checkDistance()
    {
      /* if the distance is less than the chase radius AND greater than the attack radius */
      if(Vector3.Distance(target.position, transform.position) <= chaseRadius
          && Vector3.Distance(target.position, transform.position) > attackRadius)
      {
        /* if the enemy is idling or walking and not staggered then we want to move */
        if(currentState == EnemyState.idle || currentState == EnemyState.walking
            && currentState != EnemyState.staggered)
        {
          /* move towards the player at a rate of the enemy's speed */
          Vector3 tempVector = Vector3.MoveTowards(transform.position,
                                                   target.position,
                                                   enemyBaseSpeed * Time.deltaTime);

          /* apply the position change to the rigid body */
          slimeBody.MovePosition(tempVector);

          //IDEA this slime jumps when he moves, but others could roll, just play the idle animation, or do a combination
          /* set the moving animator bool to true, so we can play the moving animation */
          slimeAnimator.SetBool("isMoving", true);
        }
      }
      /* if the slime isn't in range of the player, go to patrolling */
      else
      {
        /* if the distance between the slime and the point is within the amount of error,
            move towards it */
        if(Vector3.Distance(transform.position, path[currPoint].position) > roundingError)
        {
          /* move towards the next point at a rate of the enemy's speed */
          Vector3 tempVector = Vector3.MoveTowards(transform.position,
                                                    path[currPoint].position,
                                                    enemyBaseSpeed * Time.deltaTime);

          /* apply the position change to the rigid body */
          slimeBody.MovePosition(tempVector);

          /* set the moving animator bool to true, so we can play the moving animation */
          slimeAnimator.SetBool("isMoving", true);
        }
        /* now we must be at the point, so change the destination */
        else
        {
          changeDestination();
        }
      }
    }

    /**
     * changeDestination()
     *
     * Check what point we are at and change the next destination to the next
     *  next point in the array of paths
     */
    private void changeDestination()
    {
      /* if we are at the end of the path, reset it */
      if(currPoint == path.Length - 1)
      {
        currPoint = 0;
        nextPoint = path[0];
      }
      /* if we aren't at the end of the path, move to the next one */
      else
      {
        currPoint++;
        nextPoint = path[currPoint];
      }
    }

    /**
     * takeDamage()
     *
     * Called when the slime takes damage. Calls the death handler if the slime's
     *  HP is lower than 0
     *
     * @param damage: The amount of damage that the hit does, will be subtracted
     *                  from the HP of the enemy
     */
    public override void takeDamage(float damage)
    {
      /* subtract the damage from the current HP of the enemy */
      enemyCurrHealth -= damage;

      /* call the death function if the HP is at or below 0 */
      if(enemyCurrHealth <= 0)
      {
        roomSignal.raise();

        //this.GetComponent<GreenSlimePatrolling>().deathHandler();

        deathHandler();
      }
    }


}
