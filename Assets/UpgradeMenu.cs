using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{


    [SerializeField] GameObject[] allUpgradeUI;
    HumanReceivePoint hrs;


    PlayerHealth player;
    Beam beam;

    [SerializeField] int healthUpgrade;
    private void Start()
    {
        SwitchMenu(false);
        hrs = FindObjectOfType<HumanReceivePoint>();
        player = FindObjectOfType<PlayerHealth>();
        beam= player.GetComponentInChildren<Beam>();
    }
  

    public void SwitchMenu(bool on)
    {
       
        
            foreach (var item in allUpgradeUI)
            {
                item.SetActive(on);
            }
        

    }

    public void RestoreHealth()
    {
        player.health = player.startingHealth;
        player.DisplayHealth();
        SwitchMenu(false);
    }


}
