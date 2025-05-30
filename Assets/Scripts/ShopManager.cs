using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private GameObject planeCardPrefab;

    [SerializeField]
    private Transform shopContainer;

    [SerializeField]
    private GameObject colorBtnPrefab;

    private UserData userData;

    void Start()
    {
        LoadData();
        PopulateShop();
    }

    void LoadData()
    {
        if (PlayerPrefs.HasKey("SpaceShooter_UserData"))
        {
            string json = PlayerPrefs.GetString("SpaceShooter_UserData");
            userData = JsonUtility.FromJson<UserData>(json);
        }
        else
        {
            userData = CreateDefaultUserData();
            SaveUserData();
        }
    }

    void SaveUserData()
    {
        string json = JsonUtility.ToJson(userData);
        PlayerPrefs.SetString("GameData", json);
        PlayerPrefs.Save();
    }

    void PopulateShop()
    {
        foreach (Transform child in shopContainer)
            Destroy(child.gameObject);

        foreach (PlaneData plane in userData.ownedPlanes)
        {
            GameObject cardObj = Instantiate(planeCardPrefab, shopContainer);
            PlaneCardUI cardUI = cardObj.GetComponent<PlaneCardUI>();
            cardUI.colorBtnPrefab = colorBtnPrefab;
            cardUI.Setup(plane, userData, PopulateShop);
        }
    }

    UserData CreateDefaultUserData()
    {
        return new UserData
        {
            coins = 0,
            BGMusicOn = true,
            equippedPlaneName = "Triangle",
            ownedPlanes = new List<PlaneData>
            {
                new PlaneData
                {
                    planeName = "Triangle",
                    speed = 7f,
                    gunsCount = 1,
                    selectedSprite = "spaceShooterSpriteSheet(1)_0",
                    isUnlocked = true,
                    colorOptions = new List<PlaneColorOption>
                    {
                        new()
                        {
                            colorName = "Red",
                            hexCode = "#FF0000",
                            spriteName = "spaceShooterSpriteSheet(1)_3",
                        },
                        new()
                        {
                            colorName = "Blue",
                            hexCode = "#0000FF",
                            spriteName = "spaceShooterSpriteSheet(1)_0",
                        },
                        new()
                        {
                            colorName = "Green",
                            hexCode = "#00FF00",
                            spriteName = "spaceShooterSpriteSheet(1)_1",
                        },
                        new()
                        {
                            colorName = "Orange",
                            hexCode = "#DE532C",
                            spriteName = "spaceShooterSpriteSheet(1)_2",
                        },
                    },
                },
            },
        };
    }
}
