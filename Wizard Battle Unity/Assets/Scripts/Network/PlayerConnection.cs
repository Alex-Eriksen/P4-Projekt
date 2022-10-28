using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerConnection : NetworkBehaviour
{
    public string PlayerName { get { return m_playerName; } }
    private string m_playerName = "NO NAME";

    // Add a class or object that contains the player's data - Name, Level, Spells, etc.

    public override void OnStartClient()
    {
        
    }
}
