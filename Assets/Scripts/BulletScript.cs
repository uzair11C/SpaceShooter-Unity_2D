using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    private int damage;

    [SerializeField]
    private Rigidbody2D rb;

    void Update()
    {
        rb.velocity = Vector2.up * 9;
        if (transform.position.y > 5f)
        {
            Destroy(gameObject);
        }
    }
}
