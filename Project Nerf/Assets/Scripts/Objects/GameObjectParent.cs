/**
 *  @file GameObjectParent.cs
 *
 *  @brief Parent class for everything that will be an interactable game object
 *          in the world. Treasure chests, doors, ect.
 *
 *  @author: Alex Baker
 *  @date:   October 30 2019
 */

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectParent : MonoBehaviour
{
    /***** Variables *****/

    /* -- Private -- */


    /* -- Public -- */
    public Signal context;     /* object's signal */
    public bool playerInRange; /* true if the player is in range of the object */


    /***** Functions *****/


    /**
     * Start()
     *
     * Built in Unity function. Start is called before the first frame update
     */
    void Start()
    {

    }

    /**
     * Update()
     *
     * Built in Unity function. Update is called every frame
     */
    void Update()
    {

    }

    /**
     * OnTriggerEnter2D(Collider2d)
     *
     * Executes when this object is triggered. This function will raise the context
     *  signal and set playerInRange = true if the player is the one triggering it
     *
     * @param otherCollider: The thing that is colliding into the object this is
     *                        attached to
     */
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
      if(otherCollider.CompareTag("Player") && !otherCollider.isTrigger)
      {
        /* raise the context signal */
        context.Raise();

        /* set player in range to true */
        playerInRange = true;

      }

    }

    /**
     * OnTriggerExit2D(Collider2d)
     *
     * Executes when this object is done being triggered. Raises the signal again
     *  and sets the playerInRange = false
     *
     * @param otherCollider: The thing that is colliding into the object this is
     *                        attached to
     */
    private void OnTriggerExit2D(Collider2D otherCollider)
    {
      if(otherCollider.CompareTag("Player") && !otherCollider.isTrigger)
      {
        /* raise the context signal */
        context.Raise();

        /* set player in range to false */
        playerInRange = false;
      }    

    }



}
