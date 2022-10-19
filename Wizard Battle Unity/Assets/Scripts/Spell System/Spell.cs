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
    protected bool hitSomething = false;
    private Coroutine m_progressTimerRoutine;

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
        StartCoroutine(SCDestroySpellObject(gameObject, 3f, 0f));
    }

    [ClientRpc]
    private void Hit()
    {
        m_vfx.SendEvent("OnHit");
        hitSomething = true;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
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
        m_progressTimerRoutine = StartCoroutine(ProgressTimer());
    }

    private IEnumerator ProgressTimer()
    {
        yield return new WaitForEndOfFrame();
        if(m_currentCastTimer < m_maxCastTimer)
        {
            m_currentCastTimer += Time.deltaTime;
            m_vfx.SetFloat("Size", CurrentCastTimerNormalized);
            StartCoroutine(ProgressTimer());
        }
    }

    //[Command]
    //private void DestroySpellObjectRequest(GameObject obj)
    //{
    //    DestroySpellObject(obj);
    //}

    //[ServerCallback]
    //private void DestroySpellObject(GameObject obj)
    //{
    //    NetworkServer.Destroy(obj);
    //}

    protected bool IsBeingCast()
    {
        return CurrentCastTimerNormalized < m_maxCastTimer;
    }

    [ClientRpc]
    public void SetupSpell(NetworkIdentity identity)
    {
        m_ownerCollider = identity.GetComponent<Collider2D>();
        SetCastingTimer(m_spellData.CastTime);
    }
}
