/**
 *  @file RoomTransition.cs
 *
 *  @brief Defines the way the camera should move and follow the player when
 *          they move to a different room
 *
 *  @author: Alex Baker
 *  @date:   September 17 2019
 */

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    /***** Variables *****/

    /* -- Private -- */

    /* -- Public -- */
    public float thrust;        /* thrust force of the player's knockback */
    public float knockbackTime; /* amount of time the knockback lasts */

    /***** Functions *****/

    /**
     * OnTriggerEnter2D(Collider2d)
     *
     *
     *
     * @param otherCollider: The thing that is colliding into the object this is
     *                        attached to
     *
     */
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
      if(otherCollider.gameObject.CompareTag("enemy"))
      {
        Rigidbody2D enemy = otherCollider.GetComponent<Rigidbody2D>();
        if(enemy != null)
        {
          enemy.isKinematic = false;

          Vector2 locationDifference = enemy.transform.position - transform.position;

          locationDifference = locationDifference.normalized * thrust;

          enemy.AddForce(locationDifference, ForceMode2D.Impulse);

          StartCoroutine(knockbackCoroutine(enemy));
          
        }
      }
    }

    private IEnumerator knockbackCoroutine(Rigidbody2D enemy)
    {
      if(enemy != null)
      {
        yield return new WaitForSeconds(knockbackTime);

        enemy.velocity = Vector2.zero;

        enemy.isKinematic = true;
      }


    }

}
