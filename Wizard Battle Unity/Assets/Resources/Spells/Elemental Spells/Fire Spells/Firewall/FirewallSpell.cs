using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FirewallSpell : Spell
{
    [SerializeField] private Color m_dmgColor;
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
        StartCoroutine(SC_DamageTick());
    }

    [ServerCallback]
    private IEnumerator SC_DamageTick()
    {
        yield return new WaitForSeconds(m_tickRate);
        if(opponentEntity != null)
        {
            NumberEffectData data = new NumberEffectData();
            data.numberText = m_damagePerTick.ToString();
            data.numberColor = m_dmgColor;
            data.position = opponentEntity.transform.position;
            GameEffectsManager.Instance.Cmd_CreateNumberEffect(data);
            opponentEntity.SC_DrainHealth(m_damagePerTick);
        }
        StartCoroutine(SC_DamageTick());
    }
}
