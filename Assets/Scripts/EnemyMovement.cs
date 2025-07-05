using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float xBoundary = 1.8f;
    private float yBoundary = 4.1f;
    private float moveSpeed;
    private float fireRate;
    private float nextFireTime = 0f;

    [SerializeField]
    private GameObject[] spawnPoints;

    [SerializeField]
    private GameObject bullet;

    void Start() { }

    void Update() { }
}
