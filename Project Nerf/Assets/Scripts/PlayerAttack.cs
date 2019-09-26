/**
 *  @file PlayerAttack.cs
 *
 *  @brief Defines the player's attack properties and what happens when the player
 *          hits something with an attack. Assigns damage from the attack
 *
 *  @author: Alex Baker
 *  @date:   September 26 2019
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    /***** Variables *****/

    /* -- Private -- */

    /* -- Public -- */


    /***** Functions *****/

    /**
     * Start()
     *
     * Built in Unity function. Start is called before the first frame update
     *
     */
    void Start()
    {
    }

    /**
     * Update()
     *
     * Built in Unity function. Update is called every frame
     *
     */
    void Update()
    {

    }

    /**
     * OnTriggerEnter2D(Collider2d)
     *
     * When the player attacks something tagged as an enemy, this will fire
     *
     * @param otherCollider: The thing that is being attacked
     *
     */
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
      /* if the thing that is being collided into is tagged as an enemy */
      if(otherCollider.CompareTag("enemy"))
      {
        /* call the death animation for the enemy slime */
        otherCollider.GetComponent<SlimeEnemy>().slimeDeath();

      }

    }




}
