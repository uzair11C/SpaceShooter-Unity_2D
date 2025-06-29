using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform pathGroup; // Assign PathGroup_Arc1 in Inspector

    void Start()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        GameObject enemy = Instantiate(
            enemyPrefab,
            pathGroup.GetChild(0).position,
            Quaternion.Euler(0, 0, 180f)
        );

        EnemyPathFollower follower = enemy.GetComponent<EnemyPathFollower>();
        List<Transform> pathPoints = new();

        foreach (Transform point in pathGroup)
        {
            pathPoints.Add(point);
        }

        follower.pathPoints = pathPoints;
    }
}
