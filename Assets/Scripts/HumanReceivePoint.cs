using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class HumanReceivePoint : MonoBehaviour
{
    BoxCollider2D boxCollider;
    [SerializeField] int inGameScore = 5;
    [SerializeField] TextMeshProUGUI scoreText;


   [SerializeField] int scoreForEatingHuman = 5;
    public int humansEaten;

    [SerializeField] int beforePolice = 10;
    Beam beam;

   [SerializeField] GameObject ground;
    private void Start()
    {
        boxCollider= GetComponent<BoxCollider2D>();
        scoreText.text = $"Score: {inGameScore}";
        beam = FindObjectOfType<Beam>();
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Human"))
        {
            Destroy(collision.gameObject);
            beam.beamBattery++;
            
            humansEaten++;
           
            if(collision.GetComponent<Human>().isPoliceman)
            {
                scoreForEatingHuman = 25;
                
            }
            else
            {
                scoreForEatingHuman = 5;
            }

            CalculateScore();
            scoreText.text = $"Score: {inGameScore}";

            // when player dies pass this through to submitscore
            EnemySpawner.instance.amountOfHumans--;
            //  StartCoroutine(LeaderBoard.instance.FetchTopHighScoresRoutine());

          if(humansEaten >= beforePolice)
            {
                
              StartCoroutine(EnemySpawner.instance.SpawnPoliceMen());
                EnemySpawner.instance.CalculateNewPoliceSpawnTime();
                humansEaten = 0;
            }
        }
    }

    private int CalculateScore()
    {
        inGameScore += scoreForEatingHuman;
        var distance =  Vector2.Distance(this.transform.position,ground.transform.position);
       int yDistance = (-Mathf.RoundToInt(Mathf.Abs(transform.position.y - ground.transform.position.y)) *2);
        inGameScore -= yDistance;
       
        return inGameScore;

    }

    public void SubmitScore()
    {
        StartCoroutine(LeaderBoard.instance.SubmitScoreRoutine(inGameScore));
    }

   
}
