using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FirewallSpell : Spell
{
    [SerializeField] private Color m_dmgColor;
    [SerializeField] private StatusEffectObject statusEffect;
    private NumberEffectData data;
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

    protected override void OnFinishedCasting()
    {
        if (!isServer)
        {
            return;
        }
        StartCoroutine(SC_DamageTick());
    }

    [ServerCallback]
    private IEnumerator SC_DamageTick()
    {
        yield return new WaitForSeconds(m_tickRate);
        foreach (var playerEntity in targetEntities)
        {
            data.numberText = m_damagePerTick.ToString();
            data.numberColor = m_dmgColor;
            data.position = playerEntity.transform.position;

            GameEffectsManager.Instance.SC_CreateNumberEffect(data);
            playerEntity.SC_DrainHealth(m_damagePerTick);
            playerEntity.SC_AddStatusEffect(statusEffect.GetStatusEffectStruct());
        }
        StartCoroutine(SC_DamageTick());
    }
}
