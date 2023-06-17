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
                       
                inGameScore += 5;
            scoreText.text = $"Score: {inGameScore}";

            // when player dies pass this through to submitscore
            //     StartCoroutine(LeaderBoard.instance.SubmitScoreRoutine(inGameScore));
            //  StartCoroutine(LeaderBoard.instance.FetchTopHighScoresRoutine());

            if(inGameScore >= 10)
            {
               StartCoroutine( EnemySpawner.instance.SpawnPoliceMen());
                print("call in the cavalry");
            }
        }
    }
}
