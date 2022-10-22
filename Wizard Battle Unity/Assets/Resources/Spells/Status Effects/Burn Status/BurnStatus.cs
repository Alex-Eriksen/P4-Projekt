using Mirror;
using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        m_vfx.SetFloat("Lifetime", statusEffectData.effectLifetime);
        data.numberText = damagePerTick.ToString();
        data.numberColor = numberColor;
    }

    public override void OnStartServer()
    {
        StartCoroutine(DamageTick());
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
