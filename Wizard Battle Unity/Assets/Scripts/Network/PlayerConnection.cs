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

    
    [SerializeField] private PlayerData m_playerData = new PlayerData();

    private void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();
    }

    public override void OnStartAuthority()
    {
        // TODO: Get player data from API.
        m_playerData.PlayerName += " - " + GetComponent<NetworkIdentity>().netId;

        Cmd_IdentifySelf(m_playerData.GetDataStruct());
    }

    [Command]
    private void Cmd_IdentifySelf(PlayerDataStruct data)
    {
        m_playerData = new PlayerData(data);
        Rpc_RegisterNewIdentity(data);
    }

    [ClientRpc(includeOwner = false)]
    private void Rpc_RegisterNewIdentity(PlayerDataStruct data)
    {
        m_playerData = new PlayerData(data);
    }
}
