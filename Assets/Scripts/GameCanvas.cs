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
    PlayerController playerController;
   
   public TextMeshProUGUI humanAmountText, copAmountText, comboText, scoreAddition;
    private void Start()
    {
        humanAmountText.text = "x 0";
        gameOverMenu.SetActive(false);
        playerController = FindObjectOfType<PlayerController>();
        scoreAddition.text = string.Empty;
    }
    public void GameOver()
    {
        gameOverMenu.SetActive(true);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
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
            playerController.moveSpeed = playerController.moveSpeedoriginal;
            health.health = health.startingHealth;
            beam.ReturnBeamToNormal();
            humanReceivePoint = FindObjectOfType<HumanReceivePoint>();
            humanReceivePoint.inGameScore = 0;
            humanReceivePoint.humansEaten= 0;
            PlayerHealth.instance.ResetHealth();
            PlayerHealth.instance.input.enabled = true;
            PlayerHealth.instance.DisplayHealth();
            es.timeBetweenPoliceSpawn = 10;
            es.amountOfHumans= 0;
         
            gameOverMenu.SetActive(false);
         
            humanReceivePoint.DisplayScore();
           
        }

       
        // dont reload scene but reset everything to 0
    }

    // player current score 0
    // playe health 1
    // beam battery 2
}
