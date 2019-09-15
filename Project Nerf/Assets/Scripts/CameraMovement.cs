/**
 *  @file CameraMovement.cs
 *
 *  @brief Defines the movement of the main game camera. This will allow the
 *          camera to follow the player as the player moves.
 *
 *  @author: Alex Baker
 *  @date:   September 14 2019
 */

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    /***** Variables *****/

    /* -- Private -- */


    /* -- Public -- */
    public Transform cameraTarget;
    public float cameraSmoothing;

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
     * LateUpdate()
     *
     * Built in Unity function. LateUpdate is called every frame, but after all
     *  Update() functions are called. We will use it for the follow camera
     *
     */
    void LateUpdate()
    {
      /* if the player position and the camera position are not equal */
      if(transform.position != cameraTarget.position)
      {
        /* we need this so the camera stays at the same Z value, that way it
         *  doesn't go through the plane of the floor and we don't see anything */
        Vector3 targetPosition = new Vector3(
                                  cameraTarget.position.x,
                                  cameraTarget.position.y,
                                  transform.position.z);

        /* setting the camera position to the player's position, applies smoothing
         *  Lerp interpolates between two vectors */
        transform.position = Vector3.Lerp(
                              transform.position,
                              targetPosition,
                              cameraSmoothing);
      }
    }
}
