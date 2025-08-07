using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool blockControl = false;
    public float playerMoveSpeed;
    public float playerFireRate;
    public int coinsCollected = 0;
    public float bulletPowerUpTime = 5f;
    public GameObject bulletPowerUpGroup;
    public bool isSpecialActive = false;
    public TextMeshProUGUI coinsCountText;
    public int _playerHealth = 100;

    private UserData userData;
    private int bossHealth = 250;
    private Slider bulletPowerUpTimer;

    [SerializeField]
    private PlaneDatabase planeDatabase;

    [SerializeField]
    private Slider healthSlider;

    public int PlayerHealth
    {
        get => _playerHealth;
        set
        {
            if (_playerHealth != value)
            {
                _playerHealth = Mathf.Max(0, value); // prevent negative health
                UpdateHealthBar();

                if (_playerHealth <= 0)
                {
                    Debug.Log("You died!");
                    HandlePlayerDeath();
                }
            }
        }
    }

    void Awake()
    {
        if (PlayerPrefs.HasKey("SpaceShooter_UserData"))
        {
            string json = PlayerPrefs.GetString("SpaceShooter_UserData");
            userData = JsonUtility.FromJson<UserData>(json);

            SpawnPlayer();
        }

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        coinsCountText.text = $"{coinsCollected}";

        bulletPowerUpTimer = bulletPowerUpGroup.GetComponentInChildren<Slider>();
    }

    void Start()
    {
        healthSlider.maxValue = _playerHealth;
        UpdateHealthBar();
    }

    private void SpawnPlayer()
    {
        playerMoveSpeed = userData.equippedPlane.speed;
        playerFireRate = userData.equippedPlane.fireRate;

        var playerObj = Instantiate(
            planeDatabase
                .allPlanes.Find(plane => plane.planeName == userData.equippedPlaneName)
                .planePrefab,
            new Vector3(0, -3.3f, 0),
            Quaternion.identity
        );
        var sr = playerObj.GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.sprite = planeDatabase
                .allPlanes.Find(plane => plane.planeName == userData.equippedPlaneName)
                .selectedSprite;
    }

    private void HandlePlayerDeath()
    {
        // Block control
        blockControl = true;

        // Optionally:
        // - Trigger death animation
        // - Show game over UI
        // - Stop enemy spawns
        // - Offer retry options
    }

    private void UpdateHealthBar()
    {
        if (healthSlider == null)
            return;

        healthSlider.value = PlayerHealth;

        float healthPercent = healthSlider.value / healthSlider.maxValue;
        Color targetColor;

        if (healthPercent > 0.75f)
        {
            targetColor = Color.green;
        }
        else if (healthPercent > 0.3f)
        {
            targetColor = Color.yellow;
        }
        else
        {
            targetColor = Color.red;
        }

        if (healthSlider.fillRect != null)
        {
            if (healthSlider.fillRect.TryGetComponent<Image>(out var img))
                img.color = targetColor;
        }
    }

    public IEnumerator StartBulletPowerUpTimer(float duration)
    {
        bulletPowerUpGroup.SetActive(true);
        bulletPowerUpTimer.maxValue = duration;
        bulletPowerUpTimer.value = duration;
        isSpecialActive = true;
        while (bulletPowerUpTimer.value > 0)
        {
            yield return null;
            bulletPowerUpTimer.value -= Time.deltaTime;
        }
        isSpecialActive = false;
        bulletPowerUpGroup.SetActive(false);
    }
}
