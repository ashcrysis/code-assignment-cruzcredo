using Player;
using Player.Player;

namespace Settings
{
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Player")]
    public GameObject player; 
    public float safeDistance = 5f;
    public PlayerStats playerStats;
    [Header("Enemies")]
    public GameObject enemyPrefab;
    public int enemiesPerWave = 3; 
    public float spawnRadius = 10f;

    [Header("Wave Settings")]
    public int totalWaves = 3;
    public float timeBetweenSpawns = 0.5f;

    private int currentWave = 0;
    private List<GameObject> activeEnemies = new List<GameObject>();
    private Animator playerAnimator;
    
    

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        if (playerStats == null)
            playerStats = player.GetComponent<PlayerStats>();

        playerAnimator = player.GetComponent<Animator>();

        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        currentWave = 1;

        while (currentWave <= totalWaves || GameSettings.InfiniteWaves)
        {
            int enemiesThisWave;

            if (currentWave <= 3)
            {
                enemiesThisWave = currentWave; 
            }
            else
            {
                enemiesThisWave = 3; 
            }

            for (int i = 0; i < enemiesThisWave; i++)
            {
                Vector3 spawnPos = GetRandomNavmeshPosition(spawnRadius);

                while (Vector3.Distance(spawnPos, player.transform.position) < safeDistance)
                {
                    spawnPos = GetRandomNavmeshPosition(spawnRadius);
                }

                GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
                activeEnemies.Add(enemy);

                yield return new WaitForSeconds(timeBetweenSpawns);
            }

            while (activeEnemies.Count > 0)
            {
                activeEnemies.RemoveAll(e => e == null);
                yield return null;
            }

            currentWave++;
            playerStats.currentHealth = playerStats.maxHealth;
        }

        if (!GameSettings.InfiniteWaves)
        {
            yield return StartCoroutine(VictorySequence());
        }
    }

    Vector3 GetRandomNavmeshPosition(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return transform.position;
    }

    IEnumerator VictorySequence()
    {
        if (playerAnimator != null)
            playerAnimator.Play("Victory");
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
        
        yield return new WaitForSeconds(3.5f);

        SceneManager.LoadScene("Creditos");
    }
}
}