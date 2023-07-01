using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{


    [SerializeField] float beamSizeUpgrade, movementSpeedUpgrade, beamSpeedUpgrade;
    [SerializeField] int healthIncrease;

    UpgradesManager uManager;

    [SerializeField] float selfDestructTimer = 10;

  [SerializeField]  PlayerController player;

    [SerializeField] float movementSpeedCap = 35, suckedSpeedCap = 6, beamSizecap = 5;

  [SerializeField]  Beam beam;
    private void Start()
    {
        uManager = GetComponentInParent<UpgradesManager>();

        StartCoroutine(SelfDestruct());
       
        player = FindObjectOfType<PlayerController>();
        beam = player.GetComponentInChildren<Beam>();
    }
    


    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(selfDestructTimer);
        Destroy(this.gameObject);
    }

    public void IncreaseBeamSize()
    {
        beam.UpgradeBeam(beamSizeUpgrade);
    }
    public void BeamSpeedUpgrade()
    {
        beam.UpgradeBeamSpeed(beamSpeedUpgrade);
    }
    public void MoveSpeedUpgrade()
    {
        player.moveSpeed *= movementSpeedUpgrade;
    }
    public void HealthIncrease()
    {
        PlayerHealth.instance.health += healthIncrease;
        PlayerHealth.instance.DisplayHealth();
    }
}
