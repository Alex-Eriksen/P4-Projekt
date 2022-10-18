using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FireballSpell : NetworkBehaviour
{
    public ElementalSpellObject spellData = null;
    public Vector2 movementDirection = Vector2.zero;

    [SerializeField] private float m_speed = 10f;

    private Rigidbody2D m_rigidbody2D;

    private void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        m_rigidbody2D.velocity = m_speed * Time.deltaTime * movementDirection;
    }
}
