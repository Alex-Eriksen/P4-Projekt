using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;

public class BurnStatus : Status
{
    public float damagePerTick = 1f;
    public float tickRate = 0.1f;
    public Color numberColor;

    private VisualEffect m_vfx;
    private PlayerEntity m_target;
    private NumberEffectData data = new NumberEffectData();

    private void Awake()
    {
        m_vfx = GetComponent<VisualEffect>();
        m_target = GetComponentInParent<PlayerEntity>();
    }

    public override void OnStartClient()
    {
        m_vfx.SetFloat("Lifetime", statusEffectData.effectLifetime);
        data.numberText = damagePerTick.ToString();
        data.numberColor = numberColor;

        // TODO: Figure out a better way of doing this.
        if(m_target == null)
        {
            transform.SetParent(FindObjectsOfType<NetworkIdentity>().Where(x => x.netId == opponentNetworkID).Single().transform, false);
        }
    }


    public override void OnStartServer()
    {
        StartCoroutine(DamageTick());
        opponentNetworkID = m_target.GetComponent<NetworkIdentity>().netId;
    }

    [ServerCallback]
    private IEnumerator DamageTick()
    {
        yield return new WaitForSeconds(tickRate);

        data.position = transform.position;

        GameEffectsManager.Instance.SC_CreateNumberEffect(data);

        Rpc_ClientTick();
        m_target.SC_DrainHealth(damagePerTick);

        StartCoroutine(DamageTick());
    }

    [ClientRpc]
    private void Rpc_ClientTick()
    {
        m_vfx.SendEvent("OnTick");
    }
}
