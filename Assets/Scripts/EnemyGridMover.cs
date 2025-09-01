using UnityEngine;

public class EnemyGridMover : MonoBehaviour
{
    private Vector2 targetPosition;
    private float speed = 3f; // You can expose in Inspector if you want
    private bool movingIn = true;
    private GameManager gameManager;

    void Awake()
    {
        gameManager = GameManager.Instance;
    }

    public void SetTargetPosition(Vector2 target)
    {
        targetPosition = target;
    }

    void Update()
    {
        if (movingIn)
        {
            gameManager.blockControl = true;
            transform.position = Vector2.MoveTowards(
                transform.position,
                targetPosition,
                speed * Time.deltaTime
            );

            if (Vector2.Distance(transform.position, targetPosition) < 0.05f)
            {
                transform.position = targetPosition;
                movingIn = false; // Stop further movement once reached
                gameManager.blockControl = false;
            }
        }
    }
}
