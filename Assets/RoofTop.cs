using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofTop : MonoBehaviour
{
    [SerializeField] Transform leftPosition, rightPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Human"))
        {
            Human human= collision.gameObject.GetComponent<Human>();
            human.leftPosition = leftPosition;
            human.rightPosition = rightPosition;
        }
    }
}
