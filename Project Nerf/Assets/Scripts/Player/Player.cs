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
  staggered,
  interacting,
  dead
}

public class Player : MonoBehaviour
{
  /***** Variables *****/

  /* -- Private -- */
  private Rigidbody2D playerRigidBody; /* player's Rigidbody2D */
  private Vector3 speedChange;         /* change in speed for the player */
  private Animator playerAnimator;     /* player's animator */
  private bool facingRight;            /* whether the player is facing right or not */

  /* -- Public -- */
  //TODO decide on a good initial values for being powerful, should have room to scale down
  public float playerSpeed;                   /* the speed the player moves at */
  public PlayerState currentState;            /* the current state the player is in */
  public FloatValue currHealth;               /* current HP of the player */
  public ScriptableSignal playerHealthSignal; /* signal for the player's health */
  public Inventory playerInventory;           /* player's inventory */
  public double baseDamage;                   /* base damage for the player */
  public double currDamage;                   /* current damage for the player */
  public SpriteRenderer itemSprite;           /* sprite for the item being received */
  public ScriptableSignal playerHit;          /* signal for when the player is hit */
  public GameObject projectile;

  /***** Functions *****/

  /**
   * Start()
   *
   * Built in Unity function. Start is called before the first frame update
   */
  void Start()
  {
    /* start the player facing right */
    facingRight = true;

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
    /* if the player is interacting with something we don't want to be able to
     *  move, so return from the function and don't process any actions */
    if(currentState == PlayerState.interacting || currentState == PlayerState.dead)
    {
      return;
    }


    /* always start with the change at zero each frame */
    speedChange = Vector3.zero;

    /* get horizontal and vertical axis input */
    // TODO compare GetAxisRaw vs GetAxis
    // FIXME prefer just GetAxis, but the longer the key is held
    //          the longer he continues to move after letting go. Need to fix that
    speedChange.x = Input.GetAxisRaw("Horizontal");
    speedChange.y = Input.GetAxisRaw("Vertical");

    /* determine if the player wants to move or attack and what to do based on that */
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
    else if(Input.GetKeyDown("i") && currentState != PlayerState.attacking
             && currentState != PlayerState.staggered)
    {
      StartCoroutine(secondAttackCoroutine());
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

    /* set the player state back to idle as long as they are not currently interacting
     *  with something */
    if(currentState != PlayerState.interacting)
    {
      currentState = PlayerState.idle;
    }
  }

  private IEnumerator secondAttackCoroutine()
  {
    /* tell the attacking animation to fire */
    //playerAnimator.SetBool("isAttacking", true);

    /* set our current state to attacking */
    currentState = PlayerState.attacking;

    /* wait 1 frame */
    yield return null;

    makeArrow();

    /* turn the attacking animation off */
    //playerAnimator.SetBool("isAttacking", false);

    /* wait the length of the attack animation */
    //yield return new WaitForSeconds(.92f);

    /* set the player state back to idle as long as they are not currently interacting
     *  with something */
    if(currentState != PlayerState.interacting)
    {
      currentState = PlayerState.idle;
    }
  }

  private void makeArrow()
  {
    int direction = 0;

    if(facingRight)
      direction = 1;
    else
      direction = -1;

    Vector2 temp = new Vector2(direction, 0);

    Arrow arrow = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Arrow>();

    arrow.setup(temp, Vector3.zero);


  }

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
    currHealth.runtimeValue -= damage;

    /* raise the flag to let anything that subscribes to this signal know that
        the player got hit. This can later be something like a sound, a screen shake, ect */
    playerHealthSignal.raise();

    /* knockback if the player is still alive */
    if(currHealth.runtimeValue > 0)
    {
      /* run the coroutine for the knockback */
      StartCoroutine(knockbackCoroutine(knockbackTime));
    }
    /* player is dead, HP <= 0 */
    else
    {
      deathHandler();
    }

  }

  /**
   * deathHandler()
   *
   * Defines what happens when the player dies. Death animation should play, it
   *  should stop moving, and be removed from the scene
   */
  public void deathHandler()
  {
    /* set state equal to dead */
    currentState = PlayerState.dead;

    /* prevent the player from moving after death */
    if(transform.position.x > 0)
    {
      playerRigidBody.velocity = Vector3.zero;
    }

    if(transform.position.y > 0)
    {
      playerRigidBody.velocity = Vector3.zero;
    }

    /* set speed to 0 so we can't move after death */
    playerSpeed = 0;

    /* make the speed change zero after death */
    speedChange = Vector3.zero;

    /* set boolean for death animation to be true */
    playerAnimator.SetBool("isDead", true);

    /* start coroutine to remove the player */
    StartCoroutine(removePlayer());
  }

  /**
   * removePlayer()
   *
   * This will be used to set the player as inactive when it dies, which will make
   *  it disappear
   */
  private IEnumerator removePlayer()
  {
    /* wait for roughly the time of the animation */
    yield return new WaitForSeconds(1.3f);

    /* set the game object to inactive, making it invisible */
    this.gameObject.SetActive(false);
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
    /* raise the signal to let everyone subscribed know the player was hit */
    playerHit.raise();

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

  public void raiseItem()
  {
    /* only do this if there is an item to receive */
    if(playerInventory.currentItem != null)
    {
      if(currentState != PlayerState.interacting)
      {
        //TODO this is for adding an interacting animation to the player,
        //      there was none provided, so I'm not sure if it will be added
        //TODO playerAnimator.SetBool("receiveItem", true)

        /* set the state to interacting */
        currentState = PlayerState.interacting;

        /* set the sprite of the received item to the sprite of the inventory's current item */
        itemSprite.sprite = playerInventory.currentItem.itemSprite;
      }
      else
      {
        //TODO playerAnimator.SetBool("receiveItem", false);

        /* reset player to idle */
        currentState = PlayerState.idle;

        /* reset sprite to null so the item preview goes away */
        itemSprite.sprite = null;
      }
    }
  }


}
