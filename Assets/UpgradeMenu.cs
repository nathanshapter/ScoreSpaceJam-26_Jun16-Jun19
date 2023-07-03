using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] TextMeshProUGUI[] text;
    private void Start()
    {
       SwitchMenu(false);
        hrs = FindObjectOfType<HumanReceivePoint>();
        player = FindObjectOfType<PlayerHealth>();
        beam= player.GetComponentInChildren<Beam>();




     

    }


    private int GetUniqueRandomValue(HashSet<int> usedValues)
    {
        int randomValue;
        do
        {
            randomValue = Random.Range(0, 6);

        } while (usedValues.Contains(randomValue));
        return randomValue;
    }

    public void SwitchMenu(bool on)
    {
        int textNumber = 0;
        foreach (var item in allUpgradeUI)
        {
            item.SetActive(on);
        }

        if (on)
        {
            Time.timeScale = 0;

            HashSet<int> usedRandomValues = new HashSet<int>();

            foreach (Button btn in button)
            {
                Button currentButton = btn;

                int randomValue = GetUniqueRandomValue(usedRandomValues);
                usedRandomValues.Add(randomValue); // Add the random value to the used set

                currentButton.onClick.RemoveAllListeners();
                currentButton.onClick.AddListener(() => Upgrade(randomValue));

                switch (randomValue)
                {
                    case 0:
                        text[textNumber].text = "Restore Health";
                        break;
                    case 1:
                        text[textNumber].text = "Upgrade Health";
                        break;
                    case 2:
                        text[textNumber].text = "Upgrade Beam";
                        break;
                    case 3:
                        text[textNumber].text = "Kill all Police";
                        break;
                    case 4:
                        text[textNumber].text = "Upgrade Speed";
                        break;
                    case 5:
                        text[textNumber].text = "Invincibility\n(5s)";
                        break;
                }

                textNumber++;
            }
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
        
    }

    void UpgradeHealthMax()
    {
        PlayerHealth.instance.health += healthIncrease;
        PlayerHealth.instance.startingHealth += healthIncrease;
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
                SwitchMenu(false);
                break;
                case 1:
                UpgradeHealthMax();
                SwitchMenu(false);
                break;
                case 2: UpgradeBeamSize();
                SwitchMenu(false);
                break;
            case 3:
                FindObjectOfType<EnemySpawner>().DestroyAllGuns();
                SwitchMenu(false);
                break;
                case 4:
                SwitchMenu(false);
                // upgrade beam speed
                break;
                case 5:
                SwitchMenu(false);
                // give an invincibility shield for 5 seconds
                break;

        }
    }
}
