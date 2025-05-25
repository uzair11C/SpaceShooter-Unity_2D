using TMPro;
using UnityEngine;

public class LoadHighScore : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI highScoreText;

    void Start()
    {
        highScoreText.text =
            "High Score: " + PlayerPrefs.GetInt("BrickBreaker_highScore", 0).ToString();
    }
}
