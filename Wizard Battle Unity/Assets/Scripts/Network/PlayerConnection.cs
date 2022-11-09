using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;

public class PlayerConnection : NetworkBehaviour
{
    public PlayerInput PlayerInput { get; private set; }
    public NetworkIdentity wizardIdentity;

    public string PlayerName { get { return m_playerData.PlayerName; } }
    public string PlayerLevel { get { return m_playerData.PlayerExperience.ToString(); } } // TODO: Convert experience to LEVEL.
    // TODO: Make a public getter for the players spellbooks.

    private PlayerData m_playerData = new PlayerData();

    private void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();
    }

    public override void OnStartClient()
    {
        // TODO: Get player data from API.
    }
}
