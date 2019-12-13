using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
  public float speed;
  public Rigidbody2D arrowBody;


  // Start is called before the first frame update
  void Start()
  {

  }

  public void setup(Vector2 velocity, Vector3 direction)
  {
    arrowBody.velocity = velocity.normalized * speed;

    transform.rotation = Quaternion.Euler(direction);

  }

  public void OnTriggerEnter2D(Collider2D otherCollider)
  {
    if(otherCollider.gameObject.CompareTag("enemy"))
    {
      Destroy(this.gameObject);
    }
  }


}
