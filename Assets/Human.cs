using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
   public Transform target;
    public float moveSpeed = 5f;


  public  Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }



    private void Update()
    {
        if(target == null) return;

     Vector3 newPosition =   Vector3.MoveTowards(this.transform.position, target.transform.position,moveSpeed * Time.deltaTime );
        transform.position = newPosition;
  
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            print("hit ground");
        }


       
    }
}
