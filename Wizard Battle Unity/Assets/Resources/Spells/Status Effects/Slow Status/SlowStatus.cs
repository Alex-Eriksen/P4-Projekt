using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;

public class SlowStatus : Status
{
    [SerializeField, Range(0, 100)] private int m_slowPercent = 30;
    private VisualEffect m_vfx;
    private PlayerMovement m_target;

    private void Awake()
    {
        m_vfx = GetComponent<VisualEffect>();
        m_target = GetComponentInParent<PlayerMovement>();
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
        m_target.speedMultiplier -= m_slowPercent / 100f;
    }

    private void OnDestroy()
    {
        if (isServer)
        {
            m_target.speedMultiplier += m_slowPercent / 100f;
        }
    }
}
