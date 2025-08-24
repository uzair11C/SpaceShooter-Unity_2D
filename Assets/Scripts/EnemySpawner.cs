using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }
    public Transform[] pathGroups;

    [SerializeField]
    private PlaneDatabase planeDatabase;

    [SerializeField]
    private Transform startSpawnPoint;

    [SerializeField]
    private TextMeshProUGUI waveName;

    [SerializeField]
    private TextMeshProUGUI waveCountText;

    [SerializeField]
    private GameObject[] bossPrefabs;

    private GameManager gameManager;
    private int totalWaves = 10;
    private int currentWave = 9;
    public bool isSpawning = false;
    public bool isGridPattern = false;

    void Awake()
    {
        gameManager = GameManager.Instance;
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        waveName.gameObject.SetActive(true);
        // waveCountText.text = "Wave 1";
        waveCountText.text = $"Wave {currentWave + 1} / {totalWaves}";
        StartCoroutine(StartWaves());
    }

    IEnumerator StartWaves()
    {
        while (currentWave < totalWaves)
        {
            currentWave++;

            waveCountText.text = $"Wave {currentWave} / {totalWaves}";

            // Randomly choose between arc or grid
            bool spawnArc = Random.value > 0.5f;

            yield return new WaitForSeconds(2f);

            waveName.gameObject.SetActive(false);

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

            if (currentWave < totalWaves)
            {
                waveName.gameObject.SetActive(true);
                waveName.text = $"Wave {currentWave + 1}";
            }
            yield return new WaitForSeconds(2f); // small pause before next wave
        }

        waveName.gameObject.SetActive(true);
        waveName.text = "Boss Alert!!!";
        yield return new WaitForSeconds(2f);
        SpawnBoss();
    }

    public void SpawnArcWave(int enemyCount, float xSpacing)
    {
        gameManager.blockControl = true; // Block player control during spawn

        isSpawning = true;

        isGridPattern = false; // Reset grid pattern flag

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
        gameManager.blockControl = false; // Unblock player control after spawn
        isSpawning = false;
    }

    public void SpawnGridPattern()
    {
        isGridPattern = true;
        isSpawning = true;
        gameManager.blockControl = true; // Block player control during spawn

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
        isSpawning = false;
        gameManager.blockControl = false; // Unblock player control after spawn
    }

    private void SpawnBoss()
    {
        waveName.gameObject.SetActive(false);
        isSpawning = true;
        gameManager.blockControl = true; // Block player control during boss spawn
        GameObject randomBossPrefab = bossPrefabs[Random.Range(0, bossPrefabs.Length)];
        Instantiate(randomBossPrefab, new Vector3(0, 2, 0), Quaternion.Euler(0, 0, 180f));

        // Optional: add boss movement into scene
        // BossMover bossMover = boss.AddComponent<BossMover>();
        // bossMover.targetPosition = new Vector3(0, 2.5f, 0); // Where boss settles on screen
        isSpawning = false;
        gameManager.blockControl = false; // Unblock player control after boss spawn
    }
}
