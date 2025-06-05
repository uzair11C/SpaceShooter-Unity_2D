using System.Collections.Generic;

[System.Serializable]
public class UserData
{
    public int coins;
    public List<PlaneData> ownedPlanes = new List<PlaneData>();
    public bool BGMusicOn;
    public string equippedPlaneName;
    public PlaneData equippedPlane;
}
