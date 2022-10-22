using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FireballSpell : Spell
{
    [SerializeField] private Color m_dmgColor;
    [SerializeField] private float m_speed = 10f;
    [SerializeField] private StatusEffectObject statusEffect;
    private Rigidbody2D m_rigidbody2D;
    private Transform m_transform;
    private NumberEffectData data;

    protected override void OnAwake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_transform = transform;
    }

    private void FixedUpdate()
    {
        if (IsBeingCast())
        {
            m_transform.SetPositionAndRotation(initialTargetTransform.position, initialTargetTransform.rotation);
            m_rigidbody2D.velocity = Vector2.zero;
            return;
        }

        if (opponentEntity != null || hitSomething)
        {
            m_rigidbody2D.velocity = Vector2.zero;
            return;
        }

        m_rigidbody2D.velocity = m_speed * Time.fixedDeltaTime * m_transform.up;
    }

    [ServerCallback]
    protected override void SC_OnHit()
    {
        SC_StartDeathTimer();
        base.SC_OnHit();
        float dmg = ((ElementalSpellObject)spellData).DamageAmount;

        data.numberText = dmg.ToString();
        data.numberColor = m_dmgColor;
        data.position = opponentEntity.transform.position;

        GameEffectsManager.Instance.Cmd_CreateNumberEffect(data);
        opponentEntity.SC_DrainHealth(dmg);
        opponentEntity.SC_AddStatusEffect(statusEffect.GetStatusEffectStruct());
    }
}
