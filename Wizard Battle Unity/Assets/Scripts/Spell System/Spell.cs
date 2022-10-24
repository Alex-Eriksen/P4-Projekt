using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.VFX;

public class Spell : NetworkBehaviour
{
    [SerializeField] protected SpellObject spellData = null;
    [SerializeField] protected VisualEffect m_vfx;
    private Collider2D m_ownerCollider;
    private Coroutine m_deathRoutine;
    
    public float CurrentCastTimerNormalized { get { return m_currentCastTimer / m_maxCastTimer; } }
    private float m_currentCastTimer = 0f;
    private float m_maxCastTimer = 0f;
    
    private bool m_shouldUpdate = false;

    protected Transform initialTargetTransform;
    protected PlayerEntity opponentEntity;
    protected bool hitSomething = false;

    private void Awake()
    {
        OnAwake();
    }

    private void Start()
    {
        if (isServer)
        {
            m_deathRoutine = StartCoroutine(SC_DestroySpellObject(gameObject, spellData.LifeTime, spellData.CastTime));
        }

        OnStart();
    }

    [ServerCallback]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isServer || m_ownerCollider == null)
        {
            return;
        }

        if (collision.Equals(m_ownerCollider) || CurrentCastTimerNormalized < 1f)
        {
            return;
        }

        if (collision.TryGetComponent<PlayerEntity>(out opponentEntity))
        {
            SC_OnHit();
        }
    }

    [ServerCallback]
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isServer || m_ownerCollider == null)
        {
            return;
        }

        if (collision.Equals(m_ownerCollider) || CurrentCastTimerNormalized < 1f)
        {
            return;
        }

        collision.TryGetComponent<PlayerEntity>(out opponentEntity);
    }

    [ServerCallback]
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isServer || m_ownerCollider == null)
        {
            return;
        }

        if (collision.Equals(m_ownerCollider) || CurrentCastTimerNormalized < 1f)
        {
            return;
        }

        if (collision.TryGetComponent<PlayerEntity>(out _))
        {
            opponentEntity = null;
            SC_OnNoHit();
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        m_shouldUpdate = false;
    }

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

        OnUpdate();
    }

    /// <summary>
    /// Sets up a spell when it is created.
    /// </summary>
    /// <param name="identity"></param>
    /// <param name="keepPositionOnInit"></param>
    [ClientRpc]
    public void RpcSetupSpell(NetworkIdentity identity)
    {
        m_ownerCollider = identity.GetComponent<Collider2D>();

        if(spellData.SpellType == SpellType.Offensive)
        {
            switch (((OffensiveSpellObject)spellData).SpellBehaviour)
            {
                case SpellBehaviour.Aura:
                    initialTargetTransform = identity.transform;
                    break;

                case SpellBehaviour.Skillshot:
                    initialTargetTransform = identity.transform.Find("Graphics").Find("AttackPoint");
                    break;

                case SpellBehaviour.Target:
                    initialTargetTransform = identity.transform.Find("Graphics").Find("TargetPoint");
                    break;

                default:
                    Debug.LogError($"For whatever reason the spell data of {name} did not have any spell behaviour.", this);
                    break;
            }
        }

        SetCastingTimer(spellData.CastTime);
        m_vfx.SetFloat("Lifetime", spellData.LifeTime + spellData.CastTime);

        OnSetup();
    }

    /// <summary>
    /// Responsible for telling clients that this spell has hit something.
    /// </summary>
    [ServerCallback]
    protected virtual void SC_OnHit()
    {
        if (opponentEntity == null)
        {
            return;
        }
        hitSomething = true;
        RpcOnHit();
    }

    [ClientRpc]
    protected virtual void RpcOnHit()
    {
        hitSomething = true;
        m_vfx.SendEvent("OnHit");
    }

    [ServerCallback]
    protected virtual void SC_OnNoHit() { }

    [ServerCallback]
    protected void SC_StartDeathTimer()
    {
        StopCoroutine(m_deathRoutine);
        m_shouldUpdate = false;
        m_deathRoutine = StartCoroutine(SC_DestroySpellObject(gameObject, 3f, 0f));
    }

    /// <summary>
    /// A coroutine running on the server, that destroys the spell object after its lifetime has run out.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="lifeTime"></param>
    /// <param name="castTime"></param>
    /// <returns></returns>
    [ServerCallback]
    private IEnumerator SC_DestroySpellObject(GameObject obj, float lifeTime, float castTime)
    {
        yield return new WaitForSeconds(lifeTime + castTime);
        NetworkServer.Destroy(obj);
    }

    /// <summary>
    /// Responsible for setting up the casting timer.
    /// </summary>
    /// <param name="castTime"></param>
    private void SetCastingTimer(float castTime)
    {
        m_maxCastTimer = castTime;
        m_currentCastTimer = 0f;
        m_shouldUpdate = true;
    }

    /// <summary>
    /// Checks wether the spell is currently being cast and returns true if it is.
    /// </summary>
    /// <returns></returns>
    protected bool IsBeingCast()
    {
        return CurrentCastTimerNormalized < 1f;
    }

    protected virtual void OnAwake() { }
    protected virtual void OnStart() { }
    protected virtual void OnSetup() { }
    [ServerCallback] public virtual void OnServerSetup() { }
    protected virtual void OnUpdate() { }
}
