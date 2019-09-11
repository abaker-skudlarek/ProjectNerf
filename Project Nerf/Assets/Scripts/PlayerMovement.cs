using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    /***** Variables *****/

    /* -- Private -- */
    private Rigidbody2D playerRigidBody;
    private Vector3 speedChange;

    /* -- Public -- */
    //TODO decide on a good initial value for being powerful, should have room to scale down
    public float playerSpeed; /* this is public so we can make changes in the inspector */


    /***** Functions *****/

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update()
    {
        /* always start with the change at zero each frame */
        speedChange = Vector3.zero;

        /* get horizontal and vertical axis input */
        // TODO compare GetAxisRaw vs GetAxis
        // TODO prefer just GetAxis, but the longer the key is held
        //          the longer he continues to move after letting go. Need to fix that
        speedChange.x = Input.GetAxisRaw("Horizontal");
        speedChange.y = Input.GetAxisRaw("Vertical");

        /* move the player */
        movePlayer();


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
