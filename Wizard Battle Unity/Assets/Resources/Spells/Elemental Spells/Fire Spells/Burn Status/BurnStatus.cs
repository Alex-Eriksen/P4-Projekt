using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BurnStatus : NetworkBehaviour
{
    public float lifeTime = 8f;
    public float damagePerTick = 1f;
    public float tickRate = 0.1f;
    private VisualEffect m_vfx;

    private void Awake()
    {
        m_vfx = GetComponent<VisualEffect>();
    }

    private void Start()
    {
        m_vfx.SetFloat("Lifetime", lifeTime);
    }

    public override void OnStartServer()
    {
        StartCoroutine(DamageTick());
    }

    [ServerCallback]
    private IEnumerator DamageTick()
    {
        yield return new WaitForSeconds(tickRate);
        m_vfx.SendEvent("OnTick");
    }
}
