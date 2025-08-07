using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private UserData userData;
    private GameManager gameManager;
    private float xBoundary = 1.8f;
    private float yBoundary = 4.1f;
    private float moveSpeed;
    private float fireRate;
    private float nextFireTime = 0f;

    [SerializeField]
    private GameObject[] spawnPoints;

    [SerializeField]
    private GameObject specialSpawnPoint;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private GameObject specialBullet;

    void Start()
    {
        gameManager = GameManager.Instance;
        moveSpeed = gameManager.playerMoveSpeed;
        fireRate = gameManager.playerFireRate;
    }

    void Update()
    {
#if UNITY_EDITOR
        if (gameManager.blockControl)
            return;
        if (Input.GetMouseButton(0))
        {
            UpdateTouchPosition(Input.mousePosition);
        }
#else
        if (gameManager.blockControl)
            return;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            UpdateTouchPosition(touch);
        }
#endif
    }

    private void UpdateTouchPosition(Vector3 position)
    {
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(position);
        touchPosition.z = 0f;
        Vector3 targetPosition = Vector3.Lerp(
            transform.position,
            touchPosition,
            moveSpeed * Time.deltaTime
        );

        // Clamp the final position
        targetPosition.x = Mathf.Clamp(targetPosition.x, -xBoundary, xBoundary);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -3.3f, yBoundary);

        transform.position = targetPosition;

        nextFireTime -= Time.deltaTime;

        if (nextFireTime <= 0f)
        {
            foreach (var spawnPoint in spawnPoints)
            {
                FireBullet(spawnPoint, bullet);
            }
            if (gameManager.isSpecialActive)
            {
                FireBullet(specialSpawnPoint, specialBullet);
            }
            nextFireTime = fireRate;
        }
    }

    void FireBullet(GameObject spawnPoint, GameObject bulletPrefab)
    {
        Instantiate(bulletPrefab, spawnPoint.transform.position, Quaternion.Euler(0f, 0f, 90f));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("coin"))
        {
            gameManager.coinsCollected++;
            gameManager.coinsCountText.text = $"{gameManager.coinsCollected}";
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("health"))
        {
            if (gameManager.PlayerHealth < gameManager._playerHealth)
            {
                gameManager.PlayerHealth += 10;
            }
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("specialBulletPowerUp"))
        {
            StartCoroutine(gameManager.StartBulletPowerUpTimer(5f));
            Destroy(collision.gameObject);
        }
    }
}
