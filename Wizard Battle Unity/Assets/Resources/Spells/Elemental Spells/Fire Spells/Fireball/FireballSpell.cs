using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FireballSpell : Spell
{
    [SerializeField] private float m_speed = 10f;
    private Rigidbody2D m_rigidbody2D;
    private Transform m_transform;

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
    protected override void SCOnHit()
    {
        SCStartDeathTimer();
        base.SCOnHit();
        opponentEntity.SCDrainHealth(((ElementalSpellObject)spellData).DamageAmount);
    }
}
