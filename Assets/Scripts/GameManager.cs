using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Rigidbody2D fighter;
    public int coinsCount;
    public bool blockControl = false;

    private float playerHealth = 100f;

    [SerializeField]
    private Slider healthSlider;

    [SerializeField]
    private TextMeshProUGUI coinsCountText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
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
            healthSlider.fillRect.GetComponent<Image>().color = targetColor;
        }
    }
}
