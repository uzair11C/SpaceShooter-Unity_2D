using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private float fireRate = 1f;
    private float nextFireTime = 0f;
    private EnemySpawner enemySpawner;

    [SerializeField]
    private GameObject[] spawnPoints;

    [SerializeField]
    private GameObject bullet;

    void Awake()
    {
        enemySpawner = EnemySpawner.Instance;
        nextFireTime = fireRate;
    }

    void Start()
    {
        Debug.Log("Boss Spawned at: " + transform.position);
    }

    void OnDestroy()
    {
        Debug.Log("Boss Destroyed");
    }

    void Update() { }

    private void FireBullet(GameObject spawnPoint, GameObject bulletPrefab)
    {
        Instantiate(bulletPrefab, spawnPoint.transform.position, Quaternion.Euler(0f, 0f, -90f));
    }
}
