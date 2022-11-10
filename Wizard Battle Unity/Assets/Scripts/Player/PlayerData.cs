[System.Serializable]
public class PlayerData
{
    public string PlayerName = "PLAYER";
    public int PlayerSpellbookID = -1;
    public uint PlayerExperience = 0;
    public int PlayerSkinID = -1;

    public PlayerData() { }
    public PlayerData(PlayerDataStruct data)
    {
        PlayerName = data.PlayerName;
        PlayerSpellbookID = data.PlayerSpellbookID;
        PlayerSkinID = data.PlayerSkinID;
        PlayerExperience = data.PlayerExperience;
    }
    public PlayerData(string playerName, uint playerExperience, int playerSpellbookID, int playerSkinID)
    {
        PlayerSpellbookID = playerSpellbookID;
        PlayerExperience = playerExperience;
        PlayerSkinID = playerSkinID;
        PlayerName = playerName;
    }

    public PlayerDataStruct GetDataStruct()
    {
        return new PlayerDataStruct()
        {
            PlayerExperience = PlayerExperience,
            PlayerSkinID = PlayerSkinID,
            PlayerName = PlayerName,
            PlayerSpellbookID = PlayerSpellbookID
        };
    }
}

[System.Serializable]
public struct PlayerDataStruct
{
    public string PlayerName;
    public int PlayerSpellbookID;
    public uint PlayerExperience;
    public int PlayerSkinID;
}