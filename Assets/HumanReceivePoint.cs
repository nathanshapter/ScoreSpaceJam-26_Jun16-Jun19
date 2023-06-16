using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanReceivePoint : MonoBehaviour
{
    BoxCollider2D boxCollider;



    private void Start()
    {
        boxCollider= GetComponent<BoxCollider2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Human"))
        {
            Destroy(collision.gameObject);

            print("human eaten");
        }
    }
}
