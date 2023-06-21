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

   [SerializeField] PoliceGun[] gunsCheck;
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

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.V))
        {
            print("stopped");
            StopCoroutine(SpawnPoliceMen());
        }

        
    }
    bool coroutineRunning;
    public IEnumerator SpawnPoliceMen()
    {
        if (coroutineRunning)
        {
            yield break; // Exit the coroutine if it is already running
        }

        coroutineRunning = true; // Set the flag to indicate that the coroutine is running

        yield return new WaitForSeconds(timeBetweenPoliceSpawn);

        // Add logic to check if it's time to spawn police and prevent instant spawning

        if (amountOfHumans >= 15)
        {
            Instantiate(policeMan, spawnPoints[ChooseSpawnPoint()].position, Quaternion.identity);
            CalculateNewPoliceSpawnTime();
        }
        else
        {
            timeBetweenPoliceSpawn = 20;
        }

        coroutineRunning = false;

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

        gunsCheck = FindObjectsOfType<PoliceGun>();
        if(gunsCheck.Length <= 5 && amountOfHumans >= 15 && coroutineRunning == false)
        {
            print("COROUTINE RESTARTED");
            StartCoroutine(SpawnPoliceMen());
        }
    }
  public  float CalculateNewPoliceSpawnTime()
    {
       
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
