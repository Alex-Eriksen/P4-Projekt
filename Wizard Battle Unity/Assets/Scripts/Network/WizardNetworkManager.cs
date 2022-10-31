using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class WizardNetworkManager : NetworkManager
{
    [Header("Gameplay Prefabs")]
    [SerializeField] private GameObject m_wizardPlayerPrefab;

    public static float VelocityThreshold { get; private set; }
    [SerializeField] private float m_velocityThreshold;
    public static float PositionThreshold { get; private set; }
    [SerializeField] private float m_positionThreshold;

    public override void Awake()
    {
        base.Awake();
        VelocityThreshold = m_velocityThreshold;
        PositionThreshold = m_positionThreshold;
    }

    /// <summary>
    /// <para>Sets either the velocity- or position threshold.</para>
    /// <br>If <paramref name="isVelocity"/> is equal to <see langword="true"/> it will set the velocity threshold to the <paramref name="threshold"/> parameter.</br>
    /// <br>If <paramref name="isVelocity"/> is equal to <see langword="false"/> it will set the position threshold to the <paramref name="threshold"/> parameter.</br>
    /// </summary>
    /// <param name="threshold"></param>
    /// <param name="isVelocity"></param>
    [Server]
    public static void SetThreshold(float threshold, bool isVelocity = true)
    {
        if (isVelocity)
        {
            VelocityThreshold = threshold;
        }
        else
        {
            PositionThreshold = threshold;
        }
    }

    /// <summary>
    /// <para>Sets the velocity- and position threshold to their respective parameters.</para>
    /// </summary>
    /// <param name="velocityThreshold"></param>
    /// <param name="positionThreshold"></param>
    [Server]
    public static void SetThreshold(float velocityThreshold, float positionThreshold)
    {
        VelocityThreshold = velocityThreshold;
        PositionThreshold = positionThreshold;
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        GameObject wizardObj = Instantiate(m_wizardPlayerPrefab);
        NetworkServer.Spawn(wizardObj, conn);
        conn.identity.GetComponent<PlayerConnection>().wizardIdentity = wizardObj.GetComponent<NetworkIdentity>();
    }
}