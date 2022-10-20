using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FirewallSpell : Spell
{
    private Transform m_transform;
    private ElementalSpellObject m_castSpellData;
    private float m_tickRate = 0f;
    private float m_damagePerTick = 0f;

    protected override void OnAwake()
    {
        m_transform = transform;
        m_castSpellData = (ElementalSpellObject)spellData;
        m_tickRate = m_castSpellData.LifeTime / 10;
        m_damagePerTick = m_castSpellData.DamageAmount / 10;
    }

    protected override void OnSetup()
    {
        m_transform.SetPositionAndRotation(initialTargetTransform.position, initialTargetTransform.rotation);
    }

    [ServerCallback]
    public override void OnServerSetup()
    {
        base.OnServerSetup();
        StartCoroutine(SCDamageTick());
    }

    [ServerCallback]
    private IEnumerator SCDamageTick()
    {
        yield return new WaitForSeconds(m_tickRate);
        if(opponentEntity != null)
        {
            opponentEntity.SCDrainHealth(m_damagePerTick);
        }
        StartCoroutine(SCDamageTick());
    }
}
