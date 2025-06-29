using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float speed = 1f;
    private bool hasStopped = false;

    void Update()
    {
        if (!hasStopped)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);

            if (transform.position.y <= 2f) // Stop at y = 2
                hasStopped = true;
        }
    }
}
