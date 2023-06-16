using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    BoxCollider2D boxCollider;
    [SerializeField] GameObject humanReceivePoint;


    [SerializeField] float battery;

    Human h;
    private void Start()
    {
        boxCollider= GetComponent<BoxCollider2D>();

     
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
      
        if (collision.gameObject.CompareTag("Human"))
        {
            h = collision.GetComponent<Human>();

            h.rb.constraints = RigidbodyConstraints2D.FreezeAll;

            StartCoroutine(SuckPlayer());
           
            h.target = humanReceivePoint.transform;
            h.rb.gravityScale = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Human"))
        {
            Human h = collision.GetComponent<Human>();

            h.target = null;
            h.rb.gravityScale = 1;

        }
    }

    private IEnumerator SuckPlayer()
    {
        yield return new WaitForSeconds(1);
        h.rb.constraints = RigidbodyConstraints2D.None;
    }
}
