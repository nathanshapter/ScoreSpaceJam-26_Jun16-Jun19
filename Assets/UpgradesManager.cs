using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    [SerializeField] GameObject[] upgrades;

    public Transform[] transformPoints;

    public float instantiateTimer = 10;

    private void Start()
    {
        StartCoroutine(SpawnNewUpgrade());
    }

    private void InstantiateRandomUpgrade()
    {
        int randomIndex = Random.Range(0, upgrades.Length); // Generate a random index

        GameObject newUpgrade = Instantiate(upgrades[randomIndex],this.transform); // Instantiate the random upgrade
        PlaceObjectRandomly(newUpgrade); // Place the upgrade randomly

        StartCoroutine(SpawnNewUpgrade());
    }

    public void PlaceObjectRandomly(GameObject go)
    {
        float minX = Mathf.Min(transformPoints[0].position.x, transformPoints[1].position.x, transformPoints[2].position.x, transformPoints[3].position.x);
        float maxX = Mathf.Max(transformPoints[0].position.x, transformPoints[1].position.x, transformPoints[2].position.x, transformPoints[3].position.x);
        float minY = Mathf.Min(transformPoints[0].position.y, transformPoints[1].position.y, transformPoints[2].position.y, transformPoints[3].position.y);
        float maxY = Mathf.Max(transformPoints[0].position.y, transformPoints[1].position.y, transformPoints[2].position.y, transformPoints[3].position.y);


        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        go.transform.position = new Vector3(randomX, randomY,go.transform.position.z);

      
    }

    private IEnumerator SpawnNewUpgrade()
    {
        yield return new WaitForSeconds(instantiateTimer);
        InstantiateRandomUpgrade();
    }
}
