[System.Serializable]
public class PlayerData
{
    public string PlayerName = "PLAYER";
    public int PlayerSpellbookID = -1;
    public uint PlayerExperience = 0;
    public int PlayerSkinID = -1;

    public PlayerData() { }
    public PlayerData(string playerName, uint playerExperience, int playerSpellbookID, int playerSkinID)
    {
        PlayerSpellbookID = playerSpellbookID;
        PlayerExperience = playerExperience;
        PlayerSkinID = playerSkinID;
        PlayerName = playerName;
    }
}