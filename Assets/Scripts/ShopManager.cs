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

    [SerializeField]
    private PlaneDatabase planeDatabase;

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

        foreach (PlaneData plane in planeDatabase.allPlanes)
        {
            plane.isUnlocked = userData.ownedPlanes.Exists(ownedPlane =>
                ownedPlane.planeName == plane.planeName
            );
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

        foreach (PlaneData plane in planeDatabase.allPlanes)
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
            ownedPlanes = new List<PlaneData> { planeDatabase.allPlanes[0] },
        };
    }
}
