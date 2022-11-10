using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : NetworkBehaviour
{
    public StatusEffectObject statusEffectData;
    [SyncVar] public uint opponentNetworkID;
}
