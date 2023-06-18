using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    [SerializeField] GameObject policeMan;
    [SerializeField] GameObject human;

    [SerializeField] HumanReceivePoint hrs;


    [SerializeField] Transform[] spawnPoints;


   public float timeBetweenPoliceSpawn, timeBetweenHumanSpawn;

   public int maxHuman, amountOfHumans;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else { Destroy(this.gameObject); }
    }

    private void Start()
    {
        StartCoroutine(SpawnHuman());

       
    }

    public IEnumerator SpawnPoliceMen()
    {
        
        yield return new WaitForSeconds(timeBetweenPoliceSpawn);
        if (hrs.humansEaten >= hrs.beforePolice) { Instantiate(policeMan, spawnPoints[ChooseSpawnPoint()].position, Quaternion.identity); }
       
        StartCoroutine(SpawnPoliceMen());
    }

    public IEnumerator SpawnHuman()
    {
        yield return new WaitForSeconds(timeBetweenHumanSpawn);
        if (!TooManyHumans())
        {
            Instantiate(human, spawnPoints[ChooseSpawnPoint()].position, Quaternion.identity);
            amountOfHumans++;
        }
        
   
        StartCoroutine(SpawnHuman());
    }
  public  float CalculateNewPoliceSpawnTime()
    {
        if(timeBetweenPoliceSpawn <=  3)
        {
            timeBetweenPoliceSpawn = 3;
            return timeBetweenPoliceSpawn;
        }
        float newSpawnTime = timeBetweenPoliceSpawn / 2;
        timeBetweenPoliceSpawn= newSpawnTime;
        return timeBetweenPoliceSpawn;
    }
   int ChooseSpawnPoint()
    {
      int poop =   Random.Range(0,spawnPoints.Length);
        return poop;
    }

    bool TooManyHumans()
    {
        
        if(amountOfHumans >= maxHuman)
        {
            return true;
        }
        else { return false; }
    }
}
