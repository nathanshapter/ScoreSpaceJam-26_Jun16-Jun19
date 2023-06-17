using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{

   public PoliceGun gunref;
   public Transform target;
    public float suckedMoveSpeed = 5f;
    public float moveSpeed = 5f;

  public  Rigidbody2D rb;
 public   bool canMove = true;
  [SerializeField]  bool isMovingRight = true;


    public Transform leftPosition;   
    public Transform rightPosition;

    Animator anim;

   public bool isPoliceman;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        leftPosition = FindObjectOfType<HumanLimit>().leftLimit;
        rightPosition = FindObjectOfType<HumanLimit>().rightLimit;
        anim = GetComponent<Animator>();
        if(GetComponentInChildren<PoliceGun>() != null)
        {
            isPoliceman = true;
            gunref= GetComponentInChildren<PoliceGun>();
        }

        if (Random.Range(0, 2) > 0)
        {
            isMovingRight = false;
            FlipCharacter();
        }
      
        
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
                    FlipCharacter();
                }
            }
            else
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                if (transform.position.x <= leftPosition.position.x)
                {
                    isMovingRight = true;
                    FlipCharacter();
                }
            }
        }
      
      

        if (target == null) return;

        Vector3 newPosition = Vector3.MoveTowards(transform.position, target.transform.position, suckedMoveSpeed * Time.deltaTime);
        transform.position = newPosition;
    }

    private void FlipCharacter()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canMove = true;
          
        }


       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("sucked");

        }
    }
}
