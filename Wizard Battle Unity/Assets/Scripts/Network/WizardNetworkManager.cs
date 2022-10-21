using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WizardNetworkManager : NetworkManager
{
    // Called when the server is turned on.
    public override void OnStartServer()
    {
        Debug.LogWarning("Server started..");
    }

    // Called when the server is turned off.
    public override void OnStopServer()
    {
        Debug.LogWarning("Server stopped..");
    }

    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        Debug.Log("Just joined: " + conn.connectionId);
    }

    // Called on the client when the client connects to the server.
    public override void OnClientConnect()
    {
        base.OnClientConnect();
        Debug.LogWarning("Connected to the server..");
    }

    // Called on the client when the client disconnects from the server.
    public override void OnClientDisconnect()
    {
        base.OnClientDisconnect();
        Debug.LogWarning("Disconnected from the server..");
    }
}