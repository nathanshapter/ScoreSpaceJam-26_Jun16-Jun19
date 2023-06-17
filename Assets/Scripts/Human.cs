using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
   public Transform target;
    public float suckedMoveSpeed = 5f;
    public float moveSpeed = 5f;

  public  Rigidbody2D rb;
 public   bool canMove = true;
    bool isMovingRight = true;


    public Transform leftPosition;   
    public Transform rightPosition;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }



    private void Update()
    {
        if (canMove)
        {
            if (isMovingRight)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                if (transform.position.x >= rightPosition.position.x)
                {
                    isMovingRight = false;
                }
            }
            if (!isMovingRight)
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                if (transform.position.x <= leftPosition.position.x)
                {
                    isMovingRight = true;
                }
            }
        }



        if (target == null) return;

     Vector3 newPosition =   Vector3.MoveTowards(this.transform.position, target.transform.position,suckedMoveSpeed * Time.deltaTime );
        transform.position = newPosition;
  
    }
   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canMove = true;
        }


       
    }
}
