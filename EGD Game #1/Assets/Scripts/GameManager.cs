using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [Header("Enemy Spawning")]
    public GameObject enemyPrefab; // -> for one type of enemy
    public float spawnRange = 10f;
    [Tooltip("How many seconds in between waves to wait before more spawns")] 
    public float waveCooldown = 4f;
    [Tooltip("How many enemies spawn per wave")]
    public int howManyEnemiesToSpawn = 5;
    // [HideInInspector] public int enemiesKilled = 0;
    // public GameObject[] enemies; // -> for in the event that we want multiple types of enemies
    
    private bool waveRunning = false;
    private float waveTimer;

    #region Private references

    public GameObject player;
    private PlayerController pc;

    #endregion
    
    [HideInInspector] public float timer;

    private void Start()
    {
        pc = player.GetComponent<PlayerController>();

        waveTimer = waveCooldown;
        timer = 0f;

        waveRunning = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // wave spawn timer
        if (!waveRunning)
        {
            waveTimer -= Time.deltaTime;

            if (waveTimer <= -0.05f)
            {
                for (int i = 0; i < howManyEnemiesToSpawn; i++)
                {
                    SpawnEnemy();
                }

                waveTimer = waveCooldown;

                Debug.Log("Spawned " + howManyEnemiesToSpawn + " enemies");

                waveRunning = true;
            }
        }
    }
    
    // Spawn enemies
    private void SpawnEnemy()
    {
        Vector3 spawnPoint = GetRandomSpawnPoint(spawnRange);

        Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
    }

    // Get spawn point to spawn enemy
    private Vector3 GetRandomSpawnPoint(float range)
    {
        Vector3 playerPos = player.transform.position;

        float pointX = Random.Range(-playerPos.x - range, playerPos.x + range);
        float pointY = Random.Range(-playerPos.y - range, playerPos.y + range);

        return new Vector3(pointX, pointY, playerPos.z);
    }
}
