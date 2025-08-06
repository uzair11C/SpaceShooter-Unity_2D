using UnityEngine;

public class BossMover : MonoBehaviour
{
    public Vector3 targetPosition;
    public float speed = 2f;

    private EnemySpawner enemySpawner;

    void Awake()
    {
        enemySpawner = EnemySpawner.Instance;
    }

    void Update()
    {
        // if (enemySpawner.isSpawning)
        // {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime
        );
        // }
    }
}
