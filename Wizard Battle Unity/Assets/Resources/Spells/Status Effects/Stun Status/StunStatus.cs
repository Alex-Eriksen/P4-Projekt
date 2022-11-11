using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;

public class StunStatus : Status
{
    private VisualEffect m_vfx;
    private PlayerMovement m_target;

    private void Awake()
    {
        m_vfx = GetComponent<VisualEffect>();
        m_target = GetComponentInParent<PlayerMovement>();
    }

    public override void OnStartServer()
    {
        opponentNetworkID = m_target.GetComponent<NetworkIdentity>().netId;
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
}
