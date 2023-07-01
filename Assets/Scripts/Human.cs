using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Human : MonoBehaviour
{

   public PoliceGun gunref;
   public Transform target;
   
    public float moveSpeed = 5f;

  public  Rigidbody2D rb;
    public bool isBeingSucked = false;
 public   bool canMove = true;
  [SerializeField]  bool isMovingRight = true;


    public Transform leftPosition;   
    public Transform rightPosition;

   [SerializeField] Animator anim;

   public bool isPoliceman;

    Beam beam;

    private void Start()
    {
        this.transform.SetParent(EnemySpawner.instance.transform, false);
        rb = GetComponent<Rigidbody2D>();
        leftPosition = FindObjectOfType<HumanLimit>().leftLimit;
        rightPosition = FindObjectOfType<HumanLimit>().rightLimit;
        anim = GetComponentInChildren<Animator>();
        beam = FindObjectOfType<Beam>();
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
        ResetRotation();
        if (!canMove)
        {
            anim.SetBool("sucked", true);
        }
        else if(canMove)
        {
            anim.SetBool("sucked", false);

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
        if(beam == null)
        {
            beam = FindObjectOfType<Beam>();
        }
        Vector3 newPosition = Vector3.MoveTowards(transform.position, target.transform.position, beam.suckedMoveSpeed * Time.deltaTime);
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
        if (collision.gameObject.CompareTag("Ground") && !isBeingSucked)
        {
          
            canMove = true;
        
        }
    }

 
    private void ResetRotation()
    {
        transform.rotation = Quaternion.identity;
    }
}
