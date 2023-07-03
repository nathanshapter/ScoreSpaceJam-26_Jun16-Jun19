using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{


    [SerializeField] GameObject[] allUpgradeUI;
    HumanReceivePoint hrs;


    PlayerHealth player;
    Beam beam;

    [SerializeField] int healthUpgrade;
    [SerializeField] int healthIncrease = 250;
    [SerializeField] float beamSizeUpgrade = 1.1f;

  [SerializeField]  Button[] button;
    private void Start()
    {
      //  SwitchMenu(false);
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

        if(on)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    void RestoreHealth()
    {
        player.health = player.startingHealth;
        player.DisplayHealth();
        SwitchMenu(false);
    }

    void UpgradeHealthMax()
    {
        PlayerHealth.instance.health += healthIncrease;
        PlayerHealth.instance.DisplayHealth();
    }
    void UpgradeBeamSize()
    {
        beam.UpgradeBeam(beamSizeUpgrade);
    }

    public void Upgrade(int i)
    {
        switch(i)
        {
            case 0:
                RestoreHealth();
                break;
                case 1:
                UpgradeHealthMax();
               
                break;
                case 2: UpgradeBeamSize();
                break;

        }
    }
}
