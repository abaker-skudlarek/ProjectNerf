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

public class RoomTransition : MonoBehaviour
{
    /***** Variables *****/

    /* -- Private -- */
    private CameraMovement cameraObject; /* reference to the camera object */

    /* -- Public -- */
    public Vector2 cameraChangeMax; /* how much to move the camera on room change in max position */
    public Vector2 cameraChangeMin; /* how much to move the camera on room change in min position */
    public Vector3 playerChange;    /* how much to move the player on room change */

    /***** Functions *****/

    // Start is called before the first frame update
    void Start()
    {
      cameraObject = Camera.main.GetComponent<CameraMovement>();
    }

    /**
     * OnTriggerEnter2D(Collider2d)
     *
     * Built in unity function. Invoked when something enters the collidable
     *  zone that this is attached to. In this case, when the player is exiting
     *  a room.
     * When the player is detected colliding, it will add the camera changes
     *  and the player changes so that the camera moves to the next room as well
     *  as the player
     *
     * @param otherCollider: The thing that is colliding into the object this is
     *                        attached to. The player in our most common use case
     *
     */
     // TODO This should be disabled until all the enemies in the current room are dead.
     //       So check that they are dead, if not, don't do anything on collision until they are
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
      /* if it's the player in the trigger */
      if(otherCollider.CompareTag("Player"))
      {
        /* adjust the camera max and min position. not just one variable because
         *  the rooms are not square */
        cameraObject.maxCamPosition += cameraChangeMax;
        cameraObject.minCamPosition += cameraChangeMin;

        /* adjust the player's position */
        otherCollider.transform.position += playerChange;


      }
    }

}
