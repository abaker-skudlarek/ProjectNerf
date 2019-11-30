/**
 *  @file Projectile.cs
 *
 *  @brief Base class for all projectiles in the game. Deines move speed, time to live,
 *          destroying the object after collisions, ect.
 *         Damage is not defined in this script because the Kncokback script has
 *          damage attached to it.
 *
 *  @author: Alex Baker
 *  @date:   Novermber 17 2019
 */

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

  /***** Variables *****/

  /* -- Private -- */
  private float timeToLiveSeconds;    /* time in seconds the projectile will be active */

  /* -- Public -- */
  public Vector2 moveDirection;       /* direction for the projectile to move */
  public float timeToLive;            /* amount of time the projectile will be active */
  public float moveSpeed;             /* move speed of the projectile */
  public Rigidbody2D projectileBody;  /* reference to the rigidbody */

  /***** Functions *****/

  /**
   * Start()
   *
   * Built in Unity function. Start is called before the first frame update
   */
  void Start()
  {
    /* get reference to the rigidbody2D */
    projectileBody = GetComponent<Rigidbody2D>();

    /* set the private float to the public float */
    timeToLiveSeconds = timeToLive;
  }

  /**
   * Update()
   *
   * Built in Unity function. Update is called every frame
   */
  void Update()
  {
    /* every frame, take a second away from the time to live based on the time delta */
    timeToLiveSeconds -= Time.deltaTime;

    /* if the time to live is up, destroy the projectile */
    if(timeToLiveSeconds <= 0)
    {
      Destroy(this.gameObject);
    }

  }

  public void launch(Vector2 initialVelocity)
  {
    /* projectile's velocity is initial * the move speed */
    projectileBody.velocity = initialVelocity * moveSpeed;
  }

  public void OnTriggerEnter2D(Collider2D otherCollider)
  {
    /* after hitting something, destroy the object */
    Destroy(this.gameObject);
  }


}
