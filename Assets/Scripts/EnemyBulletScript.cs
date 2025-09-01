using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    [SerializeField]
    private int damage;

    [SerializeField]
    private Rigidbody2D rb;

    private GameManager gameManager;
    private float speed;
    private float upperBound = 5f;
    private float lowerBound = -5f;

    void Start()
    {
        gameManager = GameManager.Instance;

        speed = 2f;
        rb.velocity = Vector2.down * speed;
    }

    void Update()
    {
        // Destroy bullet if out of bounds
        if (transform.position.y > upperBound || transform.position.y < lowerBound)
        {
            Destroy(gameObject);
        }

        if (GameObject.FindGameObjectsWithTag("Boss") != null)
            return;

        if (GameObject.FindGameObjectsWithTag("enemy").Length == 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            gameManager.PlayerHealth -= damage;
        }
    }
}
