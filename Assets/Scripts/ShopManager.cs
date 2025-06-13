using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }

    [SerializeField]
    private GameObject planeCardPrefab;

    [SerializeField]
    private Transform shopContainer;

    [SerializeField]
    private GameObject colorBtnPrefab;

    private UserData userData;

    [SerializeField]
    private PlaneDatabase planeDatabase;

    [SerializeField]
    private GameObject notEnoughCoinsDialog;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // PlayerPrefs.DeleteKey("SpaceShooter_UserData"); // For testing, remove in production
        LoadData();
        PopulateShop();
    }

    void LoadData()
    {
        if (PlayerPrefs.HasKey("SpaceShooter_UserData"))
        {
            string json = PlayerPrefs.GetString("SpaceShooter_UserData");
            userData = JsonUtility.FromJson<UserData>(json);
            Debug.Log("User data: " + json);
            Debug.Log(userData);
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
        Debug.Log("Saving user data: " + userData);
        Debug.Log("User data: " + JsonUtility.ToJson(userData));
        string json = JsonUtility.ToJson(userData);
        PlayerPrefs.SetString("SpaceShooter_UserData", json);
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
            equippedPlane = planeDatabase.allPlanes[0],
        };
    }

    public void ShowNotEnoughCoinsDialog()
    {
        notEnoughCoinsDialog.SetActive(true);
    }
}
