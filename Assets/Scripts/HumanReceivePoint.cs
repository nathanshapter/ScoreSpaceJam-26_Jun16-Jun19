using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class HumanReceivePoint : MonoBehaviour
{
    BoxCollider2D boxCollider;
    [SerializeField] int inGameScore = 5;
    [SerializeField] TextMeshProUGUI scoreText;


   [SerializeField] int scoreForEatingHuman = 5;
    public int humansEaten;
    private void Start()
    {
        boxCollider= GetComponent<BoxCollider2D>();
        scoreText.text = $"Score: {inGameScore}";
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Human"))
        {
            Destroy(collision.gameObject);

            print("human eaten");

            inGameScore += scoreForEatingHuman;
            scoreText.text = $"Score: {inGameScore}";

            // when player dies pass this through to submitscore
            EnemySpawner.instance.amountOfHumans--;
            //  StartCoroutine(LeaderBoard.instance.FetchTopHighScoresRoutine());

          
        }
    }

 

    public void SubmitScore()
    {
        StartCoroutine(LeaderBoard.instance.SubmitScoreRoutine(inGameScore));
    }

    public int AmountOfHumansEaten()
    {
        humansEaten = scoreForEatingHuman / 5;
        return humansEaten;
      
    }
}
