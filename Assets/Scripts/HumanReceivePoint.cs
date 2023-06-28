using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class HumanReceivePoint : MonoBehaviour
{
    BoxCollider2D boxCollider;
    public int inGameScore = 5;
    [SerializeField] TextMeshProUGUI scoreText;


   [SerializeField] int scoreForEatingHuman = 5;
    public int humansEaten;

   public int beforePolice = 10;
  [SerializeField]  Beam beam;

   [SerializeField] GameObject ground;
    [SerializeField] AudioClip eat, bloodSound;

    [SerializeField] ParticleSystem blood;

   EnemySpawner es;
   
    private void Start()
    {
        es = FindObjectOfType<EnemySpawner>();

        boxCollider= GetComponent<BoxCollider2D>();
        DisplayScore();
     

    }
   public void DisplayScore()
    {
        scoreText.text = $"Score: {inGameScore}";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Human") && !PlayerHealth.instance.dead)
        {
            Destroy(collision.gameObject);
            AudioManager.instance.PlaySound(eat, false);
            
            humansEaten++;
          
            es.UpdateCanvas();
            es.amountOfHumans--;
            blood.Play();
            StartCoroutine(PlayBloodSound());
           
            if(collision.GetComponent<Human>().isPoliceman)
            {
                scoreForEatingHuman = 25;
                
            }
            else
            {
                scoreForEatingHuman = 5;
            }

            CalculateScore();
           
            DisplayScore();
            // when player dies pass this through to submitscore
            EnemySpawner.instance.amountOfHumans--;
            //  StartCoroutine(LeaderBoard.instance.FetchTopHighScoresRoutine());

          if(humansEaten >= beforePolice)
            {
                
              StartCoroutine(EnemySpawner.instance.SpawnPoliceMen());
              //  EnemySpawner.instance.CalculateNewPoliceSpawnTime();
              // humansEaten = 0;
            }
        }
    }
    private IEnumerator PlayBloodSound()
    {
        yield return new WaitForSeconds(1);
        AudioManager.instance.PlaySound(bloodSound, false);
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
        StartCoroutine(FindObjectOfType<LeaderBoard>().SubmitScoreRoutine(inGameScore));

        
    }

   
}
