using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpearSpell : Spell
{
    [SerializeField] private Color m_dmgColor;
    [SerializeField] private float m_speed = 10f, m_pushForce = 1f;
    [SerializeField] private StatusEffectObject statusEffect;
    private Rigidbody2D m_rigidbody2D;
    private Transform m_transform;
    private NumberEffectData data;

    protected override void OnAwake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_transform = transform;
    }

    public override void OnServerSetup()
    {
        OnTriggerEnter += OnTriggerEnterCallback;
    }

    private void OnTriggerEnterCallback(PlayerEntity obj)
    {
        if (IsCasting())
        {
            return;
        }

        SC_OnHit();
    }

    protected override void OnFixedUpdate()
    {
        if (IsCasting())
        {
            m_transform.SetPositionAndRotation(initialTargetTransform.position, initialTargetTransform.rotation);
            m_rigidbody2D.velocity = Vector2.zero;
            return;
        }

        if (targetEntities.Count > 0 || hitSomething)
        {
            m_rigidbody2D.velocity = Vector2.zero;
            return;
        }

        m_rigidbody2D.velocity = m_speed * Time.fixedDeltaTime * m_transform.up;
    }

    [ServerCallback]
    protected override void SC_OnHit()
    {
        spellCollider.enabled = false;
        SC_StartDeathTimer();
        base.SC_OnHit();
        float dmg = ((ElementalSpellObject)spellData).DamageAmount;

        data.numberText = dmg.ToString();
        data.numberColor = m_dmgColor;
        data.position = targetEntities[0].transform.position;

        GameEffectsManager.Instance.Cmd_CreateNumberEffect(data);
        targetEntities[0].SC_DrainHealth(dmg);
        targetEntities[0].SC_AddStatusEffect(statusEffect.GetStatusEffectStruct());
        targetEntities[0].GetComponent<Rigidbody2D>().AddForceAtPosition(m_transform.up * m_pushForce, m_transform.position, ForceMode2D.Impulse);
    }
}