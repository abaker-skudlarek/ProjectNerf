/**
 *  @file PlayerMovement.cs
 *
 *  @brief Defines the player's movement. Including the speed the player moves
 *          and the animations to play while they are moving or not moving.
 *          Also includes implementations and animations for attacking.
 *
 *  @author: Alex Baker
 *  @date:   September 10 2019
 */

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* state machine for our player that defines the states the player can be in */
public enum PlayerState
{
  walking,
  attacking,
  idle
}

public class PlayerMovement : MonoBehaviour
{
    /***** Variables *****/

    /* -- Private -- */
    private Rigidbody2D playerRigidBody; /* player's Rigidbody2D */
    private Vector3 speedChange;         /* change in speed for the player */
    private Animator playerAnimator;     /* player's animator */
    private bool facingRight = true;     /* whether the player is facing right or not */

    /* -- Public -- */
    //TODO decide on a good initial value for being powerful, should have room to scale down
    public float playerSpeed;              /* the speed the player moves at */
    public PlayerState playerCurrentState; /* the current state the player is in */


    /***** Functions *****/

    /**
     * Start()
     *
     * Built in Unity function. Start is called before the first frame update
     *
     */
    void Start()
    {
        /* get the components and set them to their variables */
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        /* set the starting state of the player */
        playerCurrentState = PlayerState.idle;

        /* be explicit about making sure the player doesn't start the game moving */
        playerAnimator.SetBool("isMoving", false);

    }

    /**
     * Update()
     *
     * Built in Unity function. Update is called every frame
     *
     */
    void Update()
    {
        /* always start with the change at zero each frame */
        speedChange = Vector3.zero;

        /* get horizontal and vertical axis input */
        // TODO compare GetAxisRaw vs GetAxis
        // FIXME prefer just GetAxis, but the longer the key is held
        //          the longer he continues to move after letting go. Need to fix that
        speedChange.x = Input.GetAxisRaw("Horizontal");
        speedChange.y = Input.GetAxisRaw("Vertical");


        /* if the player is pressing j, flip the model to the left and start
         *  the attack coroutine */
        if(Input.GetKeyDown("j") && playerCurrentState != PlayerState.attacking)
        {
          flipModel(-1); /* send -1 for left */
          StartCoroutine(attackCoroutine());
        }
        /* else if the player is pressing l, flip the model to the right and start
         *  start the attack coroutine */
        else if(Input.GetKeyDown("l") && playerCurrentState != PlayerState.attacking)
        {
          flipModel(1); /* send +1 for right */
          StartCoroutine(attackCoroutine());
        }
        /* if the current state is walking, play the animation and do the movement */
        else if(playerCurrentState == PlayerState.idle)
        {
          animationAndMovementHandler();
        }
    }

    /**
     * attackCoroutine()
     *
     * Coroutines run parallel to the main process and allows us to build in
     *  specific delays.
     *
     * This coroutine is for when the player attacks. It will change the player
     *  state and tell the animation to fire. Also includes a very small delay
     *  between animations
     *
     */
    private IEnumerator attackCoroutine()
    {
        /* tell the attacking animation to fire */
        playerAnimator.SetBool("isAttacking", true);

        /* set our current state to attacking */
        playerCurrentState = PlayerState.attacking;

        /* wait 1 frame */
        yield return null;

        /* turn the attacking animation off */
        playerAnimator.SetBool("isAttacking", false);

        /* wait the length of the attack animation */
        yield return new WaitForSeconds(.92f);

        /* set the player state back to idle */
        playerCurrentState = PlayerState.idle;

    }

    /**
     * animationAndMovementHandler()
     *
     * Determines what animation to play based on what the character is doing.
     *  Calls movePlayer() after setting the animation if the player is moving
     *
     */
    void animationAndMovementHandler()
    {
        /* if we are moving, set moving bool to true and move the player */
        if(speedChange != Vector3.zero)
        {
            /* set bool saying that we are moving, so transition to walking
             *  animation from idle animation */
            playerAnimator.SetBool("isMoving", true);

            /* call function to move player */
            movePlayer();
        }
        else /* if we aren't moving, play the idle animation */
        {
            /* set bool saying that we aren't moving, so transition to idle
             *  animation from walking animation */
            playerAnimator.SetBool("isMoving", false);
        }

        /* after moving, flip the model based on what direction the player is
         *  moving */
        flipModel(speedChange.x);
    }

    /**
     * flipModel(float)
     *
     * Flips the character model based on what way the player is moving
     *
     * @param horizontal: The player's speed change on the X axis
     *
     */
    void flipModel(float horizontal)
    {
        if(horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            /* changes the state of facingRight */
            facingRight = !facingRight;

            /* this will change the player scale to reflect which way they are
             *  moving. Essentially changing the x value in the inspector of the
             *  model */
            Vector3 playerScale = transform.localScale;
            playerScale.x *= -1;
            transform.localScale = playerScale;
        }
    }

    /**
     * movePlayer()
     *
     * Moves the player. This is it's own function so we can call it from other
     *  places if we want. For example, adding on screen buttons
     *
     */
    void movePlayer()
    {
        /* adding the current position, the change of the speed and multiplying
         *  by the player speed and time delta.
         * Normalizing the speedChange because otherwise diagonal movement is
         *  twice as fast
         * Multiplying by the time delta will massively slow down the movement
         *  so the player is only moving a fraction of the speed per frame,
         *  making it look less choppy */
        playerRigidBody.MovePosition(
            transform.position + speedChange.normalized * playerSpeed * Time.deltaTime
        );
    }
}
