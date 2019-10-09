/**
 *  @file PlayerMovement.cs
 *
 *  @brief Defines everything that makes the player a player. Movement, attacking,
 *          animations, state, ect.
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
  idle,
  staggered
}

public class Player : MonoBehaviour
{
    /***** Variables *****/

    /* -- Private -- */
    private Rigidbody2D playerRigidBody; /* player's Rigidbody2D */
    private Vector3 speedChange;         /* change in speed for the player */
    private Animator playerAnimator;     /* player's animator */
    private bool facingRight = true;     /* whether the player is facing right or not */

    /* -- Public -- */
    //TODO decide on a good initial values for being powerful, should have room to scale down
    public float playerSpeed;                   /* the speed the player moves at */
    public PlayerState currentState;            /* the current state the player is in */
    public FloatValue currHealth;               /* current HP of the player */
    public ScriptableSignal playerHealthSignal; /* signal for the player's health */
    public double baseDamage;                   /* base damage for the player */
    public double currDamage;                   /* current damage for the player,
                                                    could be changed by multiple things */

    /***** Functions *****/

    /**
     * Start()
     *
     * Built in Unity function. Start is called before the first frame update
     */
    void Start()
    {
        /* get the components and set them to their variables */
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        /* set the starting state of the player */
        currentState = PlayerState.idle;

        /* be explicit about making sure the player doesn't start the game moving */
        playerAnimator.SetBool("isMoving", false);
    }

    /**
     * Update()
     *
     * Built in Unity function. Update is called every frame
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

        /* determine if the player wants to move or attack and what to do based
         *  on that */
        processPlayerActions();
    }

    /**
     * processPlayerActions()
     *
     * Process what type of movement the player wants to do based on the state
     *  and the current buttons being pressed. Will call the animation handler
     *  and flip the player model if it needs to.
     */
    void processPlayerActions()
    {
        /* if the player is pressing j, and not attacking, and not staggered */
        if(Input.GetKeyDown("j") && currentState != PlayerState.attacking
            && currentState != PlayerState.staggered)
        {
          /* send -1 to face the player to the left */
          flipModel(-1);

          /* start the coroutine for attacking */
          StartCoroutine(attackCoroutine());
        }
        /* if the player is pressing l, and not attacking, and not staggered */
        else if(Input.GetKeyDown("l") && currentState != PlayerState.attacking
                 && currentState != PlayerState.staggered)
        {
          /* send 1 to face the player to the right */
          flipModel(1);

          /* start the coroutine for attacking */
          StartCoroutine(attackCoroutine());
        }
        /* if the player is walking */
        else if(currentState == PlayerState.walking || currentState == PlayerState.idle)
        {
          /* play the animation and do the movement */
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
     */
    private IEnumerator attackCoroutine()
    {
        /* tell the attacking animation to fire */
        playerAnimator.SetBool("isAttacking", true);

        /* set our current state to attacking */
        currentState = PlayerState.attacking;

        /* wait 1 frame */
        yield return null;

        /* turn the attacking animation off */
        playerAnimator.SetBool("isAttacking", false);

        /* wait the length of the attack animation */
        yield return new WaitForSeconds(.92f);

        /* set the player state back to idle */
        currentState = PlayerState.idle;

    }

    /**
      //NOTE this function isn't being used right now. It's functionality has
              been relocated to the knockback script, which is attached to the
              player's hitbox, so it made more sense to put it there. I'm leaving
              this here for now just in case I decide to revert back to how it was

     * OnTriggerEnter2D(Collider2d)
     *
     * When the player attacks something tagged as an enemy, this will fire
     *
     * @param otherCollider: The thing that is being attacked
     *
     */
    //private void OnTriggerEnter2D(Collider2D otherCollider)
    //{
      /* if the thing that is being collided into is tagged as an enemy */
    //  if(otherCollider.gameObject.CompareTag("enemy"))
    //  {
        /* call the death animation for the enemy slime */
        //otherCollider.GetComponent<GreenSlime>().slimeDeath();
     // }
    //}

    /**
     * animationAndMovementHandler()
     *
     * Determines what animation to play based on what the character is doing.
     *  Calls movePlayer() after setting the animation if the player is moving
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
     */
    void flipModel(float horizontal)
    {
        if(horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            /* changes the state of facingRight */
            facingRight = !facingRight;

            /* this will change the player scale to reflect which way they are
             *  moving. Essentially flipping the x value in the inspector of the
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

    /**
     * startKnockback(float)
     *
     * Applies damage after getting hit and decides whether to start the knockback
     *  coroutine. If the player is still alive, then start the coroutine
     *
     * @param knockbackTime:  The amount of time that we want the enemy be knocked back for
     */
    public void startKnockback(float knockbackTime, float damage)
    {
      /* take damage from the hit no matter what */
      currHealth.initialValue -= damage;

      if(currHealth.initialValue > 0)
      {
        /* raise the flag to let anything that subscribes to this signal know that
            the player got hit. This can later be something like a sound, a screen shake, ect */
        playerHealthSignal.raise();

        /* run the coroutine for the knockback */
        StartCoroutine(knockbackCoroutine(knockbackTime));

      }

    }

    /**
     * knockbackCoroutine(float)
     *
     * This is the knockback coroutine for the player. Same as the enemy knockback
     *  but just tailored for the player
     *
     * @param knockbackTime: The amount of time the player should be knocked back for
     */
    private IEnumerator knockbackCoroutine(float knockbackTime)
    {
      if(playerRigidBody != null)
      {
        /* wait for the amount of time for the knockback */
        yield return new WaitForSeconds(knockbackTime);

        /* set the player's velocity to zero after the knockback is done */
        playerRigidBody.velocity = Vector2.zero;

        /* after the knockback the player should be idle */
        currentState = PlayerState.idle;

        /* this prevents the player from sliding back forever */
        playerRigidBody.velocity = Vector2.zero;
      }
    }
}
