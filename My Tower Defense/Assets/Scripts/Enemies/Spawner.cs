using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float spawnCounter = 2f;
    public GameObject enemy;
    public int enemySpawned = 0;
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int getEnemiesCount = GameController.enemiesCount;
        spawnCounter -= Time.deltaTime;
        if(spawnCounter <= 0 && enemySpawned < getEnemiesCount)
        {
            enemySpawned++;
            Spawn();
            spawnCounter = 2f;
        }
    }

    void Spawn()
    {
        GameObject enemySpawn = Instantiate(enemy, transform.position, transform.rotation);
    }
}
