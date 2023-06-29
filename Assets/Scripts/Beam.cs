using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Beam : MonoBehaviour
{
    BoxCollider2D boxCollider;
    [SerializeField] GameObject humanReceivePoint;

    [SerializeField] TextMeshProUGUI beamBatteryText;

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;

    public float suckedMoveSpeed = 5f;
    public float suckedMoveSpeedOriginal = 5f;
    Human h;
    public float beamSizeX;
    public float beamSizeXOriginal;
  
    private void Start()
    {
        suckedMoveSpeedOriginal = suckedMoveSpeed;
        beamSizeXOriginal= beamSizeX;
        boxCollider= GetComponent<BoxCollider2D>();
        spriteRenderer= GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        TurnOffBeam(false);
    }
  
    public void UpgradeBeam(float upgradeSize)
    {
        Transform beamTransform = this.transform;

        Vector3 scale = beamTransform.localScale;
        scale.y *= upgradeSize;
        beamTransform.localScale = scale;
    }
    public void ReturnBeamToNormal()
    {
        Transform beamTransform = this.transform;
        Vector3 scale = beamTransform.localScale;
        scale.y = 1;
        beamTransform.localScale = scale;
    }
  
    public void UpgradeBeamSpeed(float upgradeSpeed)
    {
        suckedMoveSpeed *= upgradeSpeed;
    }

   public void TurnOffBeam(bool swiff)
    {
        spriteRenderer.enabled = swiff;
        boxCollider.enabled = swiff;
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
            collision.gameObject.GetComponent<Human>().isBeingSucked = true;
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
            collision.gameObject.GetComponent<Human>().isBeingSucked = false;
            collision.gameObject.GetComponent<Human>().moveSpeed *= 1.3f;
        }
    }


}
