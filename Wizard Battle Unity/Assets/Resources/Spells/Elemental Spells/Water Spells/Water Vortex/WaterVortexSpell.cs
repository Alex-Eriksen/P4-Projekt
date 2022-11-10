using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterVortexSpell : Spell
{
    [SerializeField] private Color m_dmgColor;
    [SerializeField] private StatusEffectObject statusEffect;
    [SerializeField] private float m_pullForce = 1f;
    private ElementalSpellObject m_castSpellData;
    private Transform m_transform;
    private NumberEffectData data;

    protected override void OnAwake()
    {
        m_transform = transform;
        vfx.playRate = 2.0f;
        m_castSpellData = (ElementalSpellObject)spellData;
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
        SC_OnHit();
    }

    [ServerCallback]
    protected override void SC_OnHit()
    {
        SC_StartDeathTimer(1.5f);
        base.SC_OnHit();
        foreach (var entity in targetEntities)
        {
            data.numberColor = m_dmgColor;
            data.position = entity.transform.position;
            data.numberText = m_castSpellData.DamageAmount.ToString();

            GameEffectsManager.Instance.SC_CreateNumberEffect(data);
            entity.SC_DrainHealth(m_castSpellData.DamageAmount);
            entity.SC_AddStatusEffect(statusEffect.GetStatusEffectStruct());
            Vector2 force = m_transform.position - entity.transform.position;
            entity.GetComponent<PlayerMovement>().SC_AddForceAtPosition(force * m_pullForce, m_transform.position, ForceMode2D.Impulse);
        }
    }
}