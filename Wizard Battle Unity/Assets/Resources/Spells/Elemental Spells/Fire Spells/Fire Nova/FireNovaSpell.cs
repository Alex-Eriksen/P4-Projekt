using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FireNovaSpell : Spell
{
    [SerializeField] private Color m_dmgColor;
    [SerializeField] private float m_maxExpansion = 5f;
    [SerializeField] private StatusEffectObject statusEffect;
    private Transform m_transform;
    private CircleCollider2D m_collider2D;
    private NumberEffectData data;
    private float m_currentExpansion = 0f;
    private float m_expansionRate = 0f;

    protected override void OnSetup()
    {
        m_expansionRate = m_maxExpansion / spellData.LifeTime + spellData.CastTime;
    }

    protected override void OnServerSetup()
    {
        OnTriggerEnter += OnTriggerEnterCallback;
    }

    private void OnTriggerEnterCallback(Entity obj)
    {
        if (IsCasting())
        {
            return;
        }

        SC_OnHit();
    }

    protected override void OnAwake()
    {
        m_transform = transform;
        m_collider2D = GetComponent<CircleCollider2D>();
    }

    protected override void OnUpdate()
    {
        if (m_currentExpansion < m_maxExpansion && !IsCasting())
        {
            m_currentExpansion += m_expansionRate * Time.deltaTime;
        }

        m_collider2D.radius = m_currentExpansion;
        vfx.SetFloat("RingSize", m_currentExpansion);

        if (IsCasting())
        {
            m_transform.SetPositionAndRotation(initialTargetTransform.position, initialTargetTransform.rotation);
        }
    }

    [ServerCallback]
    protected override void SC_OnHit()
    {
        base.SC_OnHit();
        float dmg = ((ElementalSpellObject)spellData).DamageAmount;
        data.numberText = dmg.ToString();
        data.numberColor = m_dmgColor;
        Entity target = targetEntities[targetEntities.Count - 1];
        data.position = target.transform.position;

        GameEffectsManager.Instance.SC_CreateNumberEffect(data);
        target.SC_DrainHealth(dmg);
        target.SC_AddStatusEffect(statusEffect.GetStatusEffectStruct());
    }
}
