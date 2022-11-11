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

    private NumberEffectData data = new NumberEffectData();

    public override void OnStartClient()
    {
        base.OnStartClient();
        data.numberText = damagePerTick.ToString();
        data.numberColor = numberColor;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        StartCoroutine(DamageTick());
    }

    [ServerCallback]
    private IEnumerator DamageTick()
    {
        yield return new WaitForSeconds(tickRate);

        if (!target.ContainsStatusEffect(StatusEffectType.Invulnerable))
        {
            data.position = transform.position;

            GameEffectsManager.Instance.SC_CreateNumberEffect(data);

            Rpc_ClientTick();
            target.SC_DrainHealth(damagePerTick);
        }

        StartCoroutine(DamageTick());
    }

    [ClientRpc]
    private void Rpc_ClientTick()
    {
        vfx.SendEvent("OnTick");
    }
}
