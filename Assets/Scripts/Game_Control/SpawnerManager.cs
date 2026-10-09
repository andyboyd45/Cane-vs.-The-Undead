using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    private List<GameObject> spawners = new List<GameObject>();
    private GameObject[] enemies;
    private int timer = 0;
    private int spawnTimer = 0;
    private int spawnInterval = 5;
    private string mode = "Easy";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //Initalizes spawners 
    //Spawn interal is based on the fixed update time interval
    //(Example: FixedUpdate runs .2 seconds per call, so if spawnInterval is 5, it will spawn every 1 second)
    void Start()
    {
        foreach (Transform child in transform)
        {
            spawners.Add(child.gameObject);
        }
        spawnInterval *= 50;

    }

    //Gets reference to all enemies in the scene
    //Have entities with Enemy tag hiding off screne so spawners can work
    void Awake()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    //Handles spawning enemies based on the current mode and timers
    //(Game gets harder the more time passes by)
    void FixedUpdate()
    {
        Debug.Log("MODE: "+ mode);
        timer += 1;
        spawnTimer += 1;
        if (timer >= 0 && timer <= 3000 && spawnTimer >= spawnInterval)
        {
            mode = "Easy";
            SpawnEnemies();
            spawnTimer = 0;
        }
        if (timer > 3000 && timer <= 15000 && spawnTimer >= spawnInterval)
        {
            spawnInterval = 3 * 50;
            mode = "Normal";
            SpawnEnemies();
            spawnTimer = 0;
        }
        if (timer > 15000 && spawnTimer >= spawnInterval)
        {
            spawnInterval = 2 * 50;
            mode = "Hard";
            SpawnEnemies();
            spawnTimer = 0;
        }

    }

    //Spawns enemies at each spawner based on the current mode and random chance
    //Easy mode has a higher chance to skip spawning, Hard mode has a lower chance
    void SpawnEnemies()
    {
        foreach (GameObject spawner in spawners)
        {
            int spawntime = spawner.GetComponent<SpawnerScript>().spawntime;
            int rand = Random.Range(0, 100);

            if (spawntime < spawnInterval){continue;}
            else
            {
                if (mode == "Easy")
                {
                    if (rand < 75) {continue;}
                    else
                    {
                        Instantiate(enemies[0], spawner.transform.position, Quaternion.identity);
                        spawner.GetComponent<SpawnerScript>().spawntime = 0;                
                    }

                }
                if (mode == "Normal")
                {
                    if (rand < 50) {continue;}
                    else
                    {
                        int rand_enemy = Random.Range(0,enemies.Length);
                        Instantiate(enemies[rand_enemy], spawner.transform.position, Quaternion.identity);
                        spawner.GetComponent<SpawnerScript>().spawntime = 0;                    
                    }
                }
                if (mode == "Hard")
                {
                    if (rand < 30) {continue;}
                    else
                    {
                        Instantiate(enemies[2], spawner.transform.position, Quaternion.identity);
                        spawner.GetComponent<SpawnerScript>().spawntime = 0;                    
                    }
                }                
            }
        }
    }
}
