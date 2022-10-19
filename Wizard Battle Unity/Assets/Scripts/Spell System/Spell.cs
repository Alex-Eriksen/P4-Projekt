using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.VFX;

public class Spell : NetworkBehaviour
{
    [SerializeField] private SpellObject m_spellData = null;
    [SerializeField] private VisualEffect m_vfx;
    private Collider2D m_ownerCollider;
    
    public float CurrentCastTimerNormalized { get { return m_currentCastTimer / m_maxCastTimer; } }
    private float m_currentCastTimer = 0f;
    private float m_maxCastTimer = 0f;

    protected bool keepPositionOnInit = false;
    protected Transform initialTargetTransform;
    
    private bool m_shouldUpdate = false;
    
    protected bool hitSomething = false;

    [ServerCallback]
    private void Start()
    {
        StartCoroutine(SCDestroySpellObject(gameObject, m_spellData.LifeTime, m_spellData.CastTime));
    }

    [ServerCallback]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isServer || m_ownerCollider == null)
        {
            return;
        }

        if (collision.Equals(m_ownerCollider) || CurrentCastTimerNormalized < m_maxCastTimer || hitSomething)
        {
            return;
        }

        if (collision.TryGetComponent<PlayerEntity>(out var opponentEntity))
        {
            opponentEntity.SCDrainHealth(m_spellData.DamageAmount);
            Hit();
        }

        StopAllCoroutines();
        m_shouldUpdate = false;
        StartCoroutine(SCDestroySpellObject(gameObject, 3f, 0f));
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        m_shouldUpdate = false;
    }

    [ClientCallback]
    private void Update()
    {
        if (!m_shouldUpdate)
        {
            return;
        }

        if (m_currentCastTimer < m_maxCastTimer)
        {
            m_currentCastTimer += Time.deltaTime;
            m_vfx.SetFloat("Size", CurrentCastTimerNormalized);
        }
    }

    [ClientRpc]
    public void SetupSpell(NetworkIdentity identity, bool keepPositionOnInit)
    {
        m_ownerCollider = identity.GetComponent<Collider2D>();

        initialTargetTransform = identity.transform.Find("Graphics").Find("AttackPoint");
        this.keepPositionOnInit = keepPositionOnInit;

        SetCastingTimer(m_spellData.CastTime);
    }

    [ClientRpc]
    private void Hit()
    {
        m_vfx.SendEvent("OnHit");
        hitSomething = true;
    }

    [ServerCallback]
    private IEnumerator SCDestroySpellObject(GameObject obj, float lifeTime, float castTime)
    {
        yield return new WaitForSeconds(lifeTime + castTime);
        NetworkServer.Destroy(obj);
    }

    private void SetCastingTimer(float castTime)
    {
        m_maxCastTimer = castTime;
        m_currentCastTimer = 0f;
        m_shouldUpdate = true;
    }

    protected bool IsBeingCast()
    {
        return CurrentCastTimerNormalized < m_maxCastTimer;
    }
}
