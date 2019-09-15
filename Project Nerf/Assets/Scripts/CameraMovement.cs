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
    public Transform cameraTarget; /* where the camera should be */
    public float cameraSmoothing;  /* delay at which the camera moves */
    public Vector2 maxCamPosition; /* maximun the camera can go */
    public Vector2 minCamPosition; /* minimum the camera can go */


    /***** Functions *****/

    /**
     * LateUpdate()
     *
     * Built in Unity function. LateUpdate is called every frame, but after all
     *  Update() functions are called.
     * We will use it for the camera to follow the player. It also keeps the
     *  camera in the bounds of the room
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

        /* clamp the X position to the min and max camera positions */
        targetPosition.x = Mathf.Clamp(
                            targetPosition.x,
                            minCamPosition.x,
                            maxCamPosition.x);

        /* clamp the Y position to the min and max camera positions */
        targetPosition.y = Mathf.Clamp(
                            targetPosition.y,
                            minCamPosition.y,
                            maxCamPosition.y);

        /* setting the camera position to the player's position, applies smoothing
         *  Lerp interpolates between two vectors */
        transform.position = Vector3.Lerp(
                              transform.position,
                              targetPosition,
                              cameraSmoothing);
      }
    }
}
