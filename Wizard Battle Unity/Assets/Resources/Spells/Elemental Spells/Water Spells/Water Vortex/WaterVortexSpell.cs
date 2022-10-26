using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterVortexSpell : Spell
{
    [SerializeField] private Color m_dmgColor;
    [SerializeField] private StatusEffectObject statusEffect;
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

    [ServerCallback]
    protected override void SC_OnHit()
    {
        base.SC_OnHit();
        data.numberColor = m_dmgColor;
        data.position = targetEntities[0].transform.position;
        data.numberText = m_castSpellData.DamageAmount.ToString();

        vfx.SendEvent("OnStop");

        GameEffectsManager.Instance.Cmd_CreateNumberEffect(data);
        targetEntities[0].SC_DrainHealth(m_castSpellData.DamageAmount);
        // TODO: Create water status effect.
        // opponentEntity.SC_AddStatusEffect(statusEffect.GetStatusEffectStruct());
    }
}