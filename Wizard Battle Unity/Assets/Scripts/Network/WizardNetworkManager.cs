using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WizardNetworkManager : NetworkManager
{
    // Called when the server is turned on.
    public override void OnStartServer()
    {
        Debug.Log("Server started..");
    }

    // Called when the server is turned off.
    public override void OnStopServer()
    {
        Debug.Log("Server stopped..");
    }

    // Called on the client when the client connects to the server.
    public override void OnClientConnect()
    {
        Debug.Log("Connected to the server..");
    }

    // Called on the client when the client disconnects from the server.
    public override void OnClientDisconnect()
    {
        Debug.Log("Disconnected from the server..");
    }
}