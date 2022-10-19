using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FireballSpell : Spell
{
    [SerializeField] private float m_speed = 10f;
    private Rigidbody2D m_rigidbody2D;
    private Transform m_transform;

    private void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_transform = transform;
    }

    private void FixedUpdate()
    {
        if (IsBeingCast() || hitSomething)
        {
            m_rigidbody2D.velocity = Vector2.zero;
            return;
        }

        m_rigidbody2D.velocity = m_speed * Time.deltaTime * m_transform.up;
    }
}
