using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{

    [SerializeField] bool beamSize, movementSpeed, beamSpeed, health;
    [SerializeField] float beamSizeUpgrade, movementSpeedUpgrade, beamSpeedUpgrade;
    [SerializeField] int healthIncrease;

    UpgradesManager uManager;

    [SerializeField] float selfDestructTimer = 10;

    PlayerController player;

    [SerializeField] float movementSpeedCap = 35, suckedSpeedCap = 6, beamSizecap = 5;
    private void Start()
    {
        uManager = GetComponentInParent<UpgradesManager>();

        StartCoroutine(SelfDestruct());

        player = FindObjectOfType<PlayerController>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           Beam beam = player.GetComponentInChildren<Beam>();

            if(beamSize && beam.beamSizeX <= beamSizecap)
            {
                if(beam != null)
                {
                    beam.UpgradeBeam(beamSizeUpgrade);
                }
               
            }
            if (beamSpeed && beam.suckedMoveSpeed <= suckedSpeedCap)
            {
                if (beam != null)
                { beam.UpgradeBeamSpeed(beamSpeedUpgrade); }

               
            }
            if(movementSpeed && player.moveSpeed <=movementSpeedCap)
            {
                if(player != null)
                {
                    player.moveSpeed *= movementSpeedUpgrade;
                }
                
            }
            if (health)
            {
                PlayerHealth.instance.health += healthIncrease;
                PlayerHealth.instance.DisplayHealth();
            }
            Destroy(this.gameObject);
        }
       
    }

    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(selfDestructTimer);
        Destroy(this.gameObject);
    }
}
