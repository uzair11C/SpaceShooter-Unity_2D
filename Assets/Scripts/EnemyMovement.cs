using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float fireRate = 0.7f;
    private float nextFireTime = 0f;
    private EnemySpawner enemySpawner;

    [SerializeField]
    private GameObject[] spawnPoints;

    [SerializeField]
    private GameObject bullet;

    void Awake()
    {
        enemySpawner = EnemySpawner.Instance;
        if (enemySpawner.isGridPattern)
        {
            fireRate = 2f;
        }
        else
        {
            fireRate = 1f;
        }
    }

    void Update()
    {
        if (!enemySpawner.isSpawning)
        {
            nextFireTime -= Time.deltaTime;
            if (nextFireTime <= 0f)
            {
                foreach (var spawnPoint in spawnPoints)
                {
                    FireBullet(spawnPoint, bullet);
                }
                nextFireTime = fireRate;
            }
        }
    }

    private void FireBullet(GameObject spawnPoint, GameObject bulletPrefab)
    {
        Instantiate(bulletPrefab, spawnPoint.transform.position, Quaternion.identity);
    }
}
