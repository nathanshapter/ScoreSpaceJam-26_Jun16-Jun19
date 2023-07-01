using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    [SerializeField] GameObject policeMan;
    [SerializeField] GameObject[] human;

    [SerializeField] HumanReceivePoint hrs;


    [SerializeField] Transform[] spawnPoints;
    GameCanvas canvas;
   

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
        canvas = FindObjectOfType<GameCanvas>();
        StartCoroutine(SpawnHuman());
        amountOfHumans = FindObjectsOfType<Human>().Length;
    
    }

    public void UpdateCanvas()
    {
        amountOfHumans = FindObjectsOfType<Human>().Length;

        gunsCheck = FindObjectsOfType<PoliceGun>();
        canvas.humanAmountText.text = $"x {amountOfHumans}";
        canvas.copAmountText.text = $"x {gunsCheck.Length}";
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

    int enemyToSpawn = 0;
    public IEnumerator SpawnHuman()
    {
        

        if(enemyToSpawn > human.Length -1)
        {
            enemyToSpawn = 0;
        }

        yield return new WaitForSeconds(timeBetweenHumanSpawn);
        if (!TooManyHumans())
        {
            Instantiate(human[enemyToSpawn], spawnPoints[ChooseSpawnPoint()].position, Quaternion.identity);
            enemyToSpawn++;
            UpdateCanvas();
        }
        
   
        StartCoroutine(SpawnHuman());

       
        if(amountOfHumans >= 15 && coroutineRunning == false)
        {
          
            StartCoroutine(SpawnPoliceMen());
        }
    }
  public  float CalculateNewPoliceSpawnTime()
    {
       if(timeBetweenPoliceSpawn <= 1)
        {
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
