using System.Collections.Generic;

[System.Serializable]
public class PlaneColorOption
{
    public string colorName;
    public string hexCode;
    public string spriteName;
}

[System.Serializable]
public class PlaneData
{
    public string planeName;
    public float speed;
    public int gunsCount;
    public string selectedSprite;
    public List<PlaneColorOption> colorOptions = new();
    public bool isUnlocked;
    public int price;
}
