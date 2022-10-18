using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FireballSpell : NetworkBehaviour
{
    public ElementalSpellObject spellData = null;

    [SerializeField] private float m_speed = 10f;

    private Rigidbody2D m_rigidbody2D;
    private Transform m_transform;

    private void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_transform = transform;
    }

    private void Start()
    {
        StartCoroutine(DestroyDelay());
    }

    private IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(spellData.LifeTime);
        DestroySelf(gameObject);
    }

    [Command]
    private void DestroySelf(GameObject obj)
    {
        NetworkServer.Destroy(obj);
    }

    private void FixedUpdate()
    {
        m_rigidbody2D.velocity = m_speed * Time.deltaTime * m_transform.up;
    }
}
