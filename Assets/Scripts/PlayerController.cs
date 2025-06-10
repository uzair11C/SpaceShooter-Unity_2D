using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
    private float xBoundary = 1.8f;
    private float yBoundary = 4.1f;
    private float moveSpeed;

    [SerializeField]
    private GameObject[] spawnPoints;

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            Vector3 targetPosition = Vector3.Lerp(
                transform.position,
                mousePosition,
                moveSpeed * Time.deltaTime
            );

            // Clamp the final position
            targetPosition.x = Mathf.Clamp(targetPosition.x, -xBoundary, xBoundary);
            targetPosition.y = Mathf.Clamp(targetPosition.y, -yBoundary, yBoundary);

            transform.position = targetPosition;
        }
#else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0f;
            Vector3 targetPosition = Vector3.Lerp(
                transform.position,
                touchPosition,
                moveSpeed * Time.deltaTime
            );

            // Clamp the final position
            targetPosition.x = Mathf.Clamp(targetPosition.x, -xBoundary, xBoundary);
            targetPosition.y = Mathf.Clamp(targetPosition.y, -yBoundary, yBoundary);

            transform.position = targetPosition;
        }
#endif
    }
}
