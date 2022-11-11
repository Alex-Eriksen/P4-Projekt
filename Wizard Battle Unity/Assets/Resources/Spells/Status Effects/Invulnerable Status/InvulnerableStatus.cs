using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;

public class InvulnerableStatus : Status
{
    private VisualEffect m_vfx;
    private PlayerEntity m_target;

    private void Awake()
    {
        m_vfx = GetComponent<VisualEffect>();
        m_target = GetComponentInParent<PlayerEntity>();
    }

    public override void OnStartClient()
    {
        m_vfx.SetFloat("Lifetime", statusEffectData.effectLifetime);

        // TODO: Figure out a better way of doing this.
        if (m_target == null)
        {
            transform.SetParent(FindObjectsOfType<NetworkIdentity>().Where(x => x.netId == opponentNetworkID).Single().transform, false);
        }
    }

    public override void OnStartServer()
    {
        opponentNetworkID = m_target.GetComponent<NetworkIdentity>().netId;
    }
}
