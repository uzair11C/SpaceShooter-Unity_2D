using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlaneColorOption
{
    public string colorName;
    public string hexCode;
    public Sprite spriteName;
}

[System.Serializable]
public class PlaneData
{
    public string planeName;
    public float speed;
    public int gunsCount;
    public Sprite selectedSprite;
    public List<PlaneColorOption> colorOptions = new();
    public bool isUnlocked;
    public int price;
}

public class EnemyData { }

[CreateAssetMenu(fileName = "PlaneDatabase", menuName = "Game Data/Plane Database")]
public class PlaneDatabase : ScriptableObject
{
    public List<PlaneData> allPlanes;

    public List<EnemyData> enemyPlanes;
}
