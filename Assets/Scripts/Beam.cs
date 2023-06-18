using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Beam : MonoBehaviour
{
    BoxCollider2D boxCollider;
    [SerializeField] GameObject humanReceivePoint;

    [SerializeField] TextMeshProUGUI beamBatteryText;




    Human h;

    public float beamBattery = 1000;
    public float beamBatteryStart = 25;
    private void Start()
    {
        ResetBeam();
        boxCollider= GetComponent<BoxCollider2D>();

     
    }
    public void ResetBeam()
    {
        beamBattery = beamBatteryStart;
    }

    private void Update()
    {
        beamBattery -= Time.deltaTime;
        beamBatteryText.text = $"Battery: {beamBattery}";
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
      
        if (collision.gameObject.CompareTag("Human"))
        {
            h = collision.GetComponent<Human>();
           if(h.GetComponent<Human>().isPoliceman)
            {
                h.gunref.canShoot= false;
            }

            h.rb.constraints = RigidbodyConstraints2D.FreezeAll;

          
           
            h.target = humanReceivePoint.transform;
            h.rb.gravityScale = 0;
            h.canMove= false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Human"))
        {
            Human h = collision.GetComponent<Human>();
            if (h.GetComponent<Human>().isPoliceman)
            {
                h.gunref.canShoot = true;
            }
            h.target = null;
            h.rb.gravityScale = 1;
            h.rb.constraints = RigidbodyConstraints2D.None;
        }
    }


}
