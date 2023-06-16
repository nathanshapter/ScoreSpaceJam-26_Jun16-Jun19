using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    [SerializeField] GameObject policeMan;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else { Destroy(this.gameObject); }
    }

    public IEnumerator SpawnPoliceMen()
    {
        yield return new WaitForSeconds(10);
        StartCoroutine(SpawnPoliceMen());
    }
}
