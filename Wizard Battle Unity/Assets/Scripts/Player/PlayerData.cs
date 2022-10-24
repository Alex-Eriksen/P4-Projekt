[System.Serializable]
public class PlayerData
{
    public string PlayerName = "NO NAME";
    public uint PlayerExperience = 0;
    public int PlayerSpellbookID = -1;

    public PlayerData() { }
    public PlayerData(string playerName, uint playerExperience, int playerSpellbookID)
    {
        PlayerName = playerName;
        PlayerExperience = playerExperience;
        PlayerSpellbookID = playerSpellbookID;
    }
}