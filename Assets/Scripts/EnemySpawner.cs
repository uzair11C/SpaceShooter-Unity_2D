using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private PlaneDatabase planeDatabase;

    [SerializeField]
    private Transform startSpawnPoint;

    public Transform[] pathGroups; // Assign PathGroup_Arc1 in Inspector
    private int totalWaves = 10;
    private int currentWave = 0;

    void Start()
    {
        StartCoroutine(StartWaves());
    }

    IEnumerator StartWaves()
    {
        while (currentWave < totalWaves)
        {
            currentWave++;
            Debug.Log($"Spawning Wave {currentWave}");

            // Randomly choose between arc or grid
            bool spawnArc = Random.value > 0.5f;

            if (spawnArc)
            {
                int enemyCount = Mathf.Min(4 + currentWave, 10); // increase enemies over waves
                float xSpacing = 1.0f;
                SpawnArcWave(enemyCount, xSpacing);
            }
            else
            {
                SpawnGridPattern();
            }

            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("enemy").Length == 0
            );
            yield return new WaitForSeconds(2f); // small pause before next wave
        }

        Debug.Log("All waves spawned.");
    }

    public void SpawnArcWave(int enemyCount, float xSpacing)
    {
        Transform randomPathGroup = pathGroups[Random.Range(0, pathGroups.Length)];
        List<Transform> pathPoints = new();
        foreach (Transform point in randomPathGroup)
        {
            pathPoints.Add(point);
        }

        float totalWidth = (enemyCount - 1) * xSpacing;
        float xStart = -totalWidth / 2f; // center align

        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPos = new(
                xStart + i * xSpacing,
                pathPoints[0].position.y,
                pathPoints[0].position.z
            );

            GameObject enemy = Instantiate(
                planeDatabase
                    .enemyPlanes[Random.Range(0, planeDatabase.enemyPlanes.Count)]
                    .enemyPrefab,
                spawnPos,
                Quaternion.Euler(0, 0, 180f)
            );

            EnemyPathFollower follower = enemy.GetComponent<EnemyPathFollower>();
            follower.pathPoints = pathPoints;
        }
    }

    public void SpawnGridPattern()
    {
        int[] enemiesPerRow = { 4, 3, 2 };
        float xSpacing = 1.2f;
        float ySpacing = 1.2f;

        Vector2 targetStartPos = new(0, 4); // Where grid should settle
        float spawnHeight = 6f; // Spawn off-screen

        for (int row = 0; row < enemiesPerRow.Length; row++)
        {
            int count = enemiesPerRow[row];
            float rowWidth = (count - 1) * xSpacing;
            float xStart = -rowWidth / 2f; // Center the row

            for (int col = 0; col < count; col++)
            {
                Vector2 targetPos = new(xStart + col * xSpacing, targetStartPos.y - row * ySpacing);
                Vector2 spawnPos = new(targetPos.x, spawnHeight);

                GameObject enemy = Instantiate(
                    planeDatabase
                        .enemyPlanes[Random.Range(0, planeDatabase.enemyPlanes.Count)]
                        .enemyPrefab,
                    spawnPos,
                    Quaternion.Euler(0, 0, 180f)
                );

                // Dynamically add movement only for grid pattern
                EnemyGridMover mover = enemy.AddComponent<EnemyGridMover>();
                mover.SetTargetPosition(targetPos);
            }
        }
    }
}
