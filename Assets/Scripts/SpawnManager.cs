using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float spawnDelayMin = 4, spawnDelayMax = 6;
    private float xBound = 11, zBound = 9;
    public GameObject enemyPrefab;

    //Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnEnemy", 5);
    }

    //Spawns an Enemy in a random position off the screen
    void SpawnEnemy()
    {
        //Choose a random side of the screen, then spawn an enemy in a random spot on that side
        int side = Random.Range(0, 4);
        if (side == 0)
        {
            Instantiate(enemyPrefab, new Vector3(Random.Range(-xBound, xBound), 0, zBound), Quaternion.identity);
        }
        else if (side == 1)
        {
            Instantiate(enemyPrefab, new Vector3(xBound, 0, Random.Range(-zBound, zBound)), Quaternion.identity);
        }
        else if (side == 2)
        {
            Instantiate(enemyPrefab, new Vector3(Random.Range(-xBound, xBound), 0, -zBound), Quaternion.identity);
        }
        else
        {
            Instantiate(enemyPrefab, new Vector3(-xBound, 0, Random.Range(-zBound, zBound)), Quaternion.identity);
        }

        //Invoke this function after enough time passes (becomes more frequent over time)
        //On average, for every minute of gameplay, spawns enemies 1 second faster (minimum .5 second delay).
        float delay = Random.Range(spawnDelayMin - Time.realtimeSinceStartup / 60,
                                   spawnDelayMax - Time.realtimeSinceStartup / 60);
        if (delay < .5f)
        {
            delay = .5f;
        }
        Invoke("SpawnEnemy", delay);
    }
}
