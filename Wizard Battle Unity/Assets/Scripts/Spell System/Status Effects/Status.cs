using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;

public class Status : NetworkBehaviour
{
    protected PlayerEntity target;

    public StatusEffectObject StatusEffectData { get => m_statusEffectData; }
    [SerializeField] private StatusEffectObject m_statusEffectData;
    protected VisualEffect vfx;
    [SyncVar] public uint opponentNetworkID;
    protected float remainingLifetime = 0f, maxLifetime = 0f;
    public float RemainingLifetimeNormalized { get => remainingLifetime / maxLifetime; }

    private void Awake()
    {
        vfx = GetComponent<VisualEffect>();
        target = GetComponentInParent<PlayerEntity>();

        OnAwake();
    }

    private void Update()
    {
        if(remainingLifetime > 0)
        {
            remainingLifetime -= Time.deltaTime;
            vfx.SetFloat("Lifetime", RemainingLifetimeNormalized);
        }

        OnUpdate();
    }

    [ClientRpc]
    public void Rpc_ResetStatus()
    {
        maxLifetime = StatusEffectData.effectLifetime;
        remainingLifetime = StatusEffectData.effectLifetime;
        vfx.SetFloat("Lifetime", RemainingLifetimeNormalized);

        OnReset();
    }

    [ServerCallback]
    public void SC_ServerSetup(Transform parent)
    {
        transform.SetParent(parent, false);
        target = GetComponentInParent<PlayerEntity>();
        opponentNetworkID = target.GetComponent<NetworkIdentity>().netId;

        OnSetup();
        OnServerSetup();
    }

    [ClientRpc]
    public void Rpc_ClientSetup(Transform parent)
    {
        transform.SetParent(parent, false);
        target = GetComponentInParent<PlayerEntity>();

        OnSetup();
        OnClientSetup();
    }

    protected virtual void OnReset() { }
    protected virtual void OnAwake() { }
    protected virtual void OnUpdate() { }
    protected virtual void OnSetup() { }
    [ServerCallback] protected virtual void OnServerSetup() { }
    [ClientCallback] protected virtual void OnClientSetup() { }
}
