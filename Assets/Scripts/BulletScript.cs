using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    private int damage;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private GameObject[] collectables;

    private GameManager gameManager;
    private float speed;
    private float upperBound = 5f;
    private float lowerBound = -5f;

    void Start()
    {
        gameManager = GameManager.Instance;

        speed = 9f;
        rb.velocity = Vector2.up * speed;
    }

    void Update()
    {
        // Destroy bullet if out of bounds
        if (transform.position.y > upperBound || transform.position.y < lowerBound)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            Destroy(collision.gameObject);

            if (collectables != null && collectables.Length > 0)
            {
                int randomChance = Random.Range(1, 101);

                if (randomChance <= 30)
                {
                    Instantiate(
                        collectables[Random.Range(0, collectables.Length)],
                        collision.transform.position,
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
