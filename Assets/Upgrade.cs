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
    private void Start()
    {
        uManager = GetComponentInParent<UpgradesManager>();

        StartCoroutine(SelfDestruct());
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            if(beamSize)
            {
                if(player.GetComponentInChildren<Beam>() != null)
                {
                    player.GetComponentInChildren<Beam>().UpgradeBeam(beamSizeUpgrade);
                }
               
            }
            if (beamSpeed)
            {
                if (player.GetComponentInChildren<Beam>() != null)
                { player.GetComponentInChildren<Beam>().UpgradeBeamSpeed(beamSpeedUpgrade); }

               
            }
            if(movementSpeed)
            {
                player.moveSpeed *= movementSpeedUpgrade;
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
