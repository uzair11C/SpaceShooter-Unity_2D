using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private UserData userData;
    private GameManager gameManager;
    private float xBoundary = 1.8f;
    private float yBoundary = 4.1f;
    private float moveSpeed;
    private float fireRate;
    private float nextFireTime = 0f;
    private bool isSpecialActive = false;

    [SerializeField]
    private GameObject[] spawnPoints;

    [SerializeField]
    private GameObject specialSpawnPoint;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private GameObject specialBullet;

    void Awake()
    {
        if (PlayerPrefs.HasKey("SpaceShooter_UserData"))
        {
            string json = PlayerPrefs.GetString("SpaceShooter_UserData");
            userData = JsonUtility.FromJson<UserData>(json);
            moveSpeed = userData.equippedPlane.speed;
            fireRate = userData.equippedPlane.fireRate;
        }
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            UpdateTouchPosition(Input.mousePosition);
        }
#else
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
        targetPosition.y = Mathf.Clamp(targetPosition.y, -yBoundary, yBoundary);

        transform.position = targetPosition;

        nextFireTime -= Time.deltaTime;

        if (nextFireTime <= 0f)
        {
            foreach (var spawnPoint in spawnPoints)
            {
                FireBullet(spawnPoint, bullet);
            }
            if (isSpecialActive)
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
}
