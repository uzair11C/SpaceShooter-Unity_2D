using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    private int damage;

    [SerializeField]
    private Rigidbody2D rb;

    private float speed;
    private float upperBound = 5f;
    private float lowerBound = -5f;

    void Start()
    {
        // Set speed and direction based on tag
        if (gameObject.CompareTag("playerBullet"))
        {
            speed = 9f;
            rb.velocity = Vector2.up * speed;
        }
        else
        {
            speed = 2f;
            rb.velocity = Vector2.down * speed;
        }
    }

    void Update()
    {
        // Destroy bullet if out of bounds
        if (transform.position.y > upperBound || transform.position.y < lowerBound)
        {
            Destroy(gameObject);
        }

        if (
            GameObject.FindGameObjectsWithTag("enemy").Length == 0
            && gameObject.CompareTag("enemyBullet")
        )
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("playerBullet") && collision.CompareTag("enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else
        {
            // decrease player health
        }
    }
}
