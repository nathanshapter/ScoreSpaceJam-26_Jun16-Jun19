using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCanvas : MonoBehaviour
{
    Human[] humans;
    Bullet[] bullets;
    HumanReceivePoint humanReceivePoint;
  [SerializeField]  Beam beam;
    [SerializeField] PlayerHealth health;


    [SerializeField] GameObject gameOverMenu;

  [SerializeField]  EnemySpawner es;

    private void Start()
    {
        gameOverMenu.SetActive(false);
    }
    public void GameOver()
    {
        gameOverMenu.SetActive(true);
    }
    public void Reload()
    {
        if(health.health <= 0)
        {
            bullets = FindObjectsOfType<Bullet>();

            humans = FindObjectsOfType<Human>();

            foreach (var item in humans)
            {
                Destroy(item.gameObject);
            }
            foreach (var item in bullets)
            {
                Destroy(item.gameObject);
            }
            humanReceivePoint = FindObjectOfType<HumanReceivePoint>();
            humanReceivePoint.inGameScore = 0;
            humanReceivePoint.humansEaten= 0;
            PlayerHealth.instance.ResetHealth();
            PlayerHealth.instance.input.enabled = true;
            es.timeBetweenPoliceSpawn = 10;
            es.amountOfHumans= 0;
            beam.beamBattery = beam.beamBatteryStart;
            gameOverMenu.SetActive(false);
        }

       
        // dont reload scene but reset everything to 0
    }

    // player current score 0
    // playe health 1
    // beam battery 2
}
