using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    /***** Variables *****/

    /* -- Private -- */
    private Rigidbody2D playerRigidBody;
    private Vector3 speedChange;
    private Animator playerAnimator;
    private bool facingRight = true; /* player always starts facing right */

    /* -- Public -- */
    //TODO decide on a good initial value for being powerful, should have room to scale down
    public float playerSpeed; /* this is public so we can make changes in the inspector */


    /***** Functions *****/

    // Start is called before the first frame update
    void Start()
    {
        /* get the components and set them to their variables */
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
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

        /* call function to play animation and move character */
        animationAndMovementHandler();

    }

    /* function to determine what animation to play based on what the character
     *  is doing. Calls movePlayer() */
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

    /* function to flip the character model based on what way the player is
     *  moving */
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

    /* function to move player. This is it's own function so we can call it from other
     *  places if we want. For example, adding on screen buttons */
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
