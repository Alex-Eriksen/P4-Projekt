using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class WizardNetworkManager : NetworkManager
{
    [Header("Gameplay Prefabs")]
    [SerializeField] private GameObject m_wizardPlayerPrefab;

    //// Called when the server is turned on.
    //public override void OnStartServer()
    //{
    //    Debug.LogWarning("Server started..");
    //}

    //// Called when the server is turned off.
    //public override void OnStopServer()
    //{
    //    Debug.LogWarning("Server stopped..");
    //}

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        GameObject wizardObj = Instantiate(m_wizardPlayerPrefab);
        NetworkServer.Spawn(wizardObj, conn);
        conn.identity.GetComponent<PlayerConnection>().wizardIdentity = wizardObj.GetComponent<NetworkIdentity>();
    }

    //// Called on the client when the client connects to the server.
    //public override void OnClientConnect()
    //{
    //    base.OnClientConnect();
    //    Debug.LogWarning("Client Connected..");
    //    FindObjectOfType<ChatBoxUI>().SetPlayerConnection();
    //}

    //// Called on the client when the client disconnects from the server.
    //public override void OnClientDisconnect()
    //{
    //    base.OnClientDisconnect();
    //    Debug.LogWarning("Disconnected from the server..");
    //}
}