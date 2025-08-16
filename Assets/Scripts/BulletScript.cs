using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    private int damage;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private GameObject coinPrefab;

    [SerializeField]
    private GameObject[] collectables;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;

        rb.velocity = Vector2.up * 8f;
    }

    void Update()
    {
        // Destroy bullet if out of bounds
        if (transform.position.y > 5f || transform.position.y < -5f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            Destroy(collision.gameObject);

            if (Random.value <= 0.88f) // 88% chance to spawn coin
            {
                Instantiate(
                    coinPrefab,
                    collision.transform.position + Vector3.down * 0.5f,
                    Quaternion.identity
                );
            }

            if (collectables != null && collectables.Length > 0)
            {
                int randomChance = Random.Range(1, 101);

                if (randomChance <= 30)
                {
                    Instantiate(
                        collectables[Random.Range(0, collectables.Length)],
                        collision.transform.position + Vector3.up * 0.5f,
                        Quaternion.identity
                    );
                }
            }

            Destroy(gameObject);
        }

        if (GameObject.FindGameObjectsWithTag("enemy").Length == 0)
        {
            Destroy(gameObject);
        }
    }
}
