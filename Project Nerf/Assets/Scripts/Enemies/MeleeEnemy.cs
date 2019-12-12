using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : GreenSlime
{
    Vector3 prevLoc = Vector3.zero;
    Vector3 curVel = Vector3.zero;
    private bool facingRight;


    // Start is called before the first frame update
    public override void Start()
    {
      facingRight = true;

      /* complete the references */
      slimeAnimator = GetComponent<Animator>();
      slimeBody = GetComponent<Rigidbody2D>();

      /* start the enemy in the idle state */
      currentState = EnemyState.idle;

      /* get the location of the player (.transform just gets the location) */
      target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    public override void Update()
    {
      checkDistance();
    }

    /**
     * checkDistance()
     *
     * This function checks the enemy distance with the player's (AKA target) and
     *  moves towards the player if they are within range.
     */
    public override void checkDistance()
    {
      /* if the distance is less than the chase radius AND greater than the attack radius */
      if(Vector3.Distance(target.position, transform.position) <= chaseRadius
          && Vector3.Distance(target.position, transform.position) > attackRadius)
      {
        /* if the enemy is idling or walking and not staggered then we want to move */
        if(currentState == EnemyState.idle || currentState == EnemyState.walking
            && currentState != EnemyState.staggered)
        {
          /* move towards the player at a rate of the enemy's speed */
          Vector3 tempVector = Vector3.MoveTowards(transform.position,
                                                   target.position,
                                                   enemyBaseSpeed * Time.deltaTime);

          curVel = (transform.position - prevLoc) / Time.deltaTime;

          flipModel(curVel.x);

          prevLoc = transform.position;

          /* apply the position change to the rigid body */
          slimeBody.MovePosition(tempVector);

          /* try to change the current state to walking after moving, won't change
              if it's already in the walking state */
          currentState = changeState(currentState, EnemyState.walking);

          /* set the moving animator bool to true, so we can play the moving animation */
          slimeAnimator.SetBool("isMoving", true);
        }
      }
      else if(Vector3.Distance(target.position, transform.position) <= chaseRadius
                && Vector3.Distance(target.position, transform.position) <= attackRadius)
      {
        if(currentState == EnemyState.walking
            && currentState != EnemyState.staggered)
        {
          StartCoroutine(attackCoroutine());
        }
      }
      else
      {
        /* set the moving animator to false, because our slime isn't moving if
            he isn't within the chase radius */
        slimeAnimator.SetBool("isMoving", false);
      }
    }

    public IEnumerator attackCoroutine()
    {
      currentState = EnemyState.attacking;
      slimeAnimator.SetBool("isAttacking", true);
      yield return new WaitForSeconds(1.1f);
      currentState = EnemyState.walking;
      slimeAnimator.SetBool("isAttacking", false);
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
        facingRight = !facingRight;

        /* this will change the player scale to reflect which way they are
         *  moving. Essentially flipping the x value in the inspector of the
         *  model */
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
      }
    }

}
