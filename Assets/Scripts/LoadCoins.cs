using TMPro;
using UnityEngine;

public class LoadCoins : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI coinsText;

    void Start()
    {
        coinsText.text = PlayerPrefs.GetInt("SpaceShooter_coins", 0).ToString();
    }
}
