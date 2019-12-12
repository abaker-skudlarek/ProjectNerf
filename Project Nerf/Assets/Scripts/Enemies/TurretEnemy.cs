using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : GreenSlime
{
  /***** Variables *****/

  /* -- Private -- */
  private float fireDelaySeconds;   /* fire delay in seconds

  /* -- Public -- */
  public GameObject projectile;     /* reference to the projectile's game object */
  public float fireDelay;           /* delay between shots */
  public bool canFire = true;       /* whether the projectile can fire or not */
  public Animator redSlimeAnimator; /* animator reference */

  /***** Functions *****/

  public override void Start()
  {
    redSlimeAnimator = GetComponent<Animator>();
  }

  public override void Update()
  {
    fireDelaySeconds -= Time.deltaTime;

    if(fireDelaySeconds <= 0)
    {
      canFire = true;
      fireDelaySeconds = fireDelay;
    }

    checkDistance();
  }

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
        /* if the slime can fire */
        if(canFire)
        {
          /* temp vector for the difference between projectile position and the target */
          Vector3 tempVector = target.transform.position - transform.position;

          /* instantiate the projectile at the position we give it */
          GameObject currProjectile = Instantiate(projectile, transform.position, Quaternion.identity);

          /* launch the projectile that we spawned using the temp vector */
          currProjectile.GetComponent<Projectile>().launch(tempVector.normalized);

          /* set can fire to false, because it is busy firing right now */
          canFire = false;

          /* set the bool to start spitting */
          redSlimeAnimator.SetBool("isSpitting", true);
        }
      }
    }
    else if(Vector3.Distance(target.position, transform.position) > chaseRadius)
    {
      /* turn the spitting animation off */
      redSlimeAnimator.SetBool("isSpitting", false);
    }
  }

  /**
   * deathHandler()
   *
   * Defines what happens when the slime dies. Death animation should play, it
   *  should stop moving, and be removed from the scene
   */
  public override void deathHandler()
  {
    /* can't fire if he's dead */
    canFire = false;

    /* set the boolean for the death animation to be true */
    //slimeAnimator.SetBool("isDead", true);
    redSlimeAnimator.SetBool("isDead", true);

    /* if the slime is dead we want his model to stop moving so he doesn't
        continue to follow the player even when it's dead */
    enemyBaseSpeed = 0;

    /* start coroutine to remove the slime */
    StartCoroutine(removeSlime());
  }

  /**
   * removeSlime()
   *
   * This will be used to set the enemy as inactive when it dies, which will make
   *  it disappear
   */
  private IEnumerator removeSlime()
  {
    /* wait for roughly the time of the animation */
    yield return new WaitForSeconds(1.3f);

    /* set the game object to inactive, making it invisible */
    //this.gameObject.SetActive(false);

    /* destroy the game object when it dies, so it can't come back */
    Destroy(this.gameObject);
  }

  /**
   * takeDamage()
   *
   * Called when the slime takes damage. Calls the death handler if the slime's
   *  HP is lower than 0
   *
   * @param damage: The amount of damage that the hit does, will be subtracted
   *                  from the HP of the enemy
   */
  public override void takeDamage(float damage)
  {
    /* subtract the damage from the current HP of the enemy */
    enemyCurrHealth -= damage;

    /* call the death function if the HP is at or below 0 */
    if(enemyCurrHealth <= 0)
    {
      roomSignal.raise();

      //this.GetComponent<TurretEnemy>().deathHandler();

      deathHandler();
    }
  }


}
