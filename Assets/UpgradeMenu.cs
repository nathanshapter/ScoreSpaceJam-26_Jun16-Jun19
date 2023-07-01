using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{


    [SerializeField] GameObject[] allUpgradeUI;
    HumanReceivePoint hrs;

    private void Start()
    {
        SwitchMenu(false);
        hrs = FindObjectOfType<HumanReceivePoint>();
    }
  

    public void SwitchMenu(bool on)
    {
       
        
            foreach (var item in allUpgradeUI)
            {
                item.SetActive(on);
            }
        

    }

    
}
