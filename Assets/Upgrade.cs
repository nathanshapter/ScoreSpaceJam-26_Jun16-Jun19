using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{

    [SerializeField] bool beamSize, movementSpeed, beamSpeed, health;
    [SerializeField] float beamSizeUpgrade, movementSpeedUpgrade, beamSpeedUpgrade, healthUpgrade;




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            if(beamSize)
            {
                player.GetComponentInChildren<Beam>().UpgradeBeam(beamSizeUpgrade);
            }
            if (beamSpeed)
            {
                player.GetComponentInChildren<Beam>().UpgradeBeamSpeed(beamSpeedUpgrade);
            }
            Destroy(this.gameObject);
        }
       
    }
}
