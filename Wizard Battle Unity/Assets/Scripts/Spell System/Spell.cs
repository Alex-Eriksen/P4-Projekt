using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.VFX;
using System;

/// <summary>
/// <br>Base Class for all spells in the game.</br>
/// <br>The class is responsible for collision detection and handling whether the spell is being cast.</br>
/// <br>The class provides some easy properties that are essential for spells to function.</br>
/// <br>Like, <seealso cref="targetEntities"/> that holds a list of entities the spells has hit.</br>
/// <br>Also provides inherited methods that are virtual and can be overriden to gain functionalty.</br>
/// </summary>
public class Spell : NetworkBehaviour
{
    /// <summary>
    /// Get the spells current cast timer progress normalized between 0 to 1.
    /// </summary>
    public float CurrentCastTimerNormalized { get { return m_currentCastTimer / m_maxCastTimer; } }

    /// <summary>
    /// Called when a new collider enters the spell's collider.
    /// </summary>
    public event Action<Entity> OnTriggerEnter;
    /// <summary>
    /// Called when a collider that was present on the previous frame is still on inside the spell's collider.
    /// </summary>
    public event Action<Entity> OnTriggerStay;
    /// <summary>
    /// Called when a collider that was present on the previous frame is not present on the current frame.
    /// </summary>
    public event Action<Entity> OnTriggerExit;

    [SerializeField] protected SpellObject spellData = null;
    [SerializeField] protected VisualEffect vfx;
    [SerializeField] protected ContactFilter2D contactFilter;
    /// <summary>
    /// A list of entities that are currently colliding with the spell.
    /// </summary>
    protected List<Entity> targetEntities = new List<Entity>();
    /// <summary>
    /// The collider of the player that cast this spell.
    /// </summary>
    protected Collider2D ownerCollider;
    /// <summary>
    /// The collider of the this spell.
    /// </summary>
    protected Collider2D spellCollider;
    /// <summary>
    /// The <see cref="PlayerCombat"/> of the player that cast this spell.
    /// </summary>
    protected PlayerCombat ownerPlayerCombat;
    /// <summary>
    /// <br>The initial transform target of the spell, this is dependant of the type of spell.</br>
    /// <para>
    ///     <br><see cref="SpellType.Offensive"/>:</br>
    ///     <br>-> <see cref="OffensiveSpellBehaviour.Aura"/> = Owner's Transform</br>
    ///     <br>-> <see cref="OffensiveSpellBehaviour.Skillshot"/> = Owner's Attack Point Transform</br>
    ///     <br>-> <see cref="OffensiveSpellBehaviour.Target"/> = Owner's Target Point Transform</br>
    /// </para>
    /// <para>
    ///     <br><see cref="SpellType.Utility"/>:</br>
    ///     <br>-> <see cref="UtilitySpellBehaviour.Teleport"/> = Owner's Transform</br>
    ///     <br>-> <see cref="UtilitySpellBehaviour.Dash"/> = Owner's Graphics Transform</br>
    ///     <br>-> <see cref="UtilitySpellBehaviour.Invisibility"/> = Owner's Transform</br>
    /// </para>
    /// <para>
    ///     <br><see cref="SpellType.Defensive"/>:</br>
    ///     <br>-> <see cref="DefensiveSpellBehaviour.Block"/> = <see langword="null"/></br>
    ///     <br>-> <see cref="DefensiveSpellBehaviour.Absorb"/> = <see langword="null"/></br>
    ///     <br>-> <see cref="DefensiveSpellBehaviour.Deflect"/> = <see langword="null"/></br>
    /// </para>
    /// </summary>
    protected Transform initialTargetTransform;
    /// <summary>
    /// <br>Whether or not this spell has hit something.</br>
    /// <br>True when the first entity collides with this spell.</br>
    /// <br>Unless the <see cref="SC_OnHit"/> is overriden and the base method is not called.</br>
    /// </summary>
    protected bool hitSomething = false;

    private readonly List<Collider2D> m_overlappingColliders = new List<Collider2D>();
    private readonly List<Collider2D> m_previousFrameOverlappingColliders = new List<Collider2D>();
    private Coroutine m_deathRoutine;
    
    private float m_currentCastTimer = 0f;
    private float m_maxCastTimer = 0f;
    
    private void Awake()
    {
        spellCollider = GetComponent<Collider2D>();
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

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        ownerCollider.GetComponent<PlayerCombat>().OnCastingCanceled -= Spell_OnCastingCanceled;
    }

    private void Update()
    {
        if (m_currentCastTimer < m_maxCastTimer)
        {
            m_currentCastTimer += Time.deltaTime;
            vfx.SetFloat("Size", CurrentCastTimerNormalized);
            if(m_currentCastTimer >= m_maxCastTimer)
            {
                ownerPlayerCombat.IsCasting = IsCasting();
                OnFinishedCasting();
            }
        }

        OnUpdate();
    }

    private void FixedUpdate()
    {
        if (isServer)
        {
            spellCollider.OverlapCollider(contactFilter, m_overlappingColliders);
            CheckOverlappingColliders();
        }

        OnFixedUpdate();

        if (isServer)
        {
            m_previousFrameOverlappingColliders.Clear();
            m_overlappingColliders.CopyTo(m_previousFrameOverlappingColliders);
        }
    }

    #region Custom Collision

    /// <summary>
    /// Checks the previous- and current frame colliders to see if they; Entered, Stayed, or Exited.
    /// This is called every physics update.
    /// <para>-> <see cref="OnTriggerEnter"/> when a new collider enters this collider.</para>
    /// <para>-> <see cref="OnTriggerStay"/> when a collider was in the previous frame and this frame.</para>
    /// <para>-> <see cref="OnTriggerExit"/> when a collider was in the previous frame but not in this frame.</para>
    /// </summary>
    [ServerCallback]
    private void CheckOverlappingColliders()
    {
        // No reason to run the method if the current- and previous frame collider lists both don't contain any elements.
        if(m_overlappingColliders.Count == 0 && m_previousFrameOverlappingColliders.Count == 0)
        {
            return;
        }

        // Return the method if our own collider isnt set (This shouldn't happen, but you never know).
        if (ownerCollider == null)
        {
            Debug.LogError($"Spell::CheckOverlappingColliders() -> Failed: m_ownerCollider is NULL");
            return;
        }

        Entity entity;

        // TODO(idea): Events for -Enter, -Stay, and OnTriggerExit that contains colliders.
        // Loop through the previous frame colliders and check whether they are missing this frame.
        // If they aren't and it has a PlayerEntity component we can raise the OnTriggerExit event.
        for (int i = 0; i < m_previousFrameOverlappingColliders.Count; i++)
        {
            if(m_previousFrameOverlappingColliders[i] == null)
            {
                continue;
            }

            // Continue the loop if the collider is our own.
            if (m_previousFrameOverlappingColliders[i].Equals(ownerCollider))
            {
                continue;
            }

            // Check if the current overlapping colliders contains the previous frame collider.
            // If it does continue the loop, no need to act further on it.
            if (m_overlappingColliders.Contains(m_previousFrameOverlappingColliders[i]))
            {
                continue;
            }

            // If the current overlapping colliders does NOT contain the previous frame collider.
            // We can then check if it contains a player entity and if it does we can raise the OnTriggerExit event.
            if (m_previousFrameOverlappingColliders[i].TryGetComponent<Entity>(out entity))
            {
                Raise_OnTriggerExit(entity);
                continue;
            }
        }

        // TODO(idea): Events for -Enter, -Stay, and OnTriggerExit that contains colliders.
        // Loop through the current frame colliders and check if they stayed from last frame or if they are new.
        // Either way we check if it has a player entity component and if it stayed we invoke the OnTriggerStay event,
        // if its new we raise the OnTriggerEnter event.
        for (int i = 0; i < m_overlappingColliders.Count; i++)
        {
            if(m_overlappingColliders[i] == null)
            {
                continue;
            }

            // Check if the current frame collider is our own, continue if it is.
            if (m_overlappingColliders[i].Equals(ownerCollider))
            {
                continue;
            }

            // If the current frame collider doesnt NOT contain a player entity component continue the loop.
            if (!m_overlappingColliders[i].TryGetComponent<Entity>(out entity))
            {
                continue;
            }

            // If the player entity hit is invulnerable continue since the target isnt a valid one.
            if (entity.ContainsStatusEffect(StatusEffectType.Invulnerable))
            {
                continue;
            }

            // If the current frame collider was present in the previous frame colliders
            // we can raise the OnTriggerStay event, since it persisted through frames.
            if (m_previousFrameOverlappingColliders.Contains(m_overlappingColliders[i]))
            {
                Raise_OnTriggerStay(entity);
                continue;
            }

            // If we make it to this point it means its a new collider that we haven't seen yet
            // so we can raise the OnTriggerEnter event announcing the arrival of a new player entity.
            Raise_OnTriggerEnter(entity);
        }
    }

    [ServerCallback]
    private void Raise_OnTriggerEnter(Entity entity)
    {
        targetEntities.Add(entity);

        OnTriggerEnter?.Invoke(entity);
    }

    [ServerCallback]
    private void Raise_OnTriggerStay(Entity entity)
    {
        if (targetEntities.Contains(entity))
        {
            return;
        }

        targetEntities.Add(entity);

        OnTriggerStay?.Invoke(entity);
    }

    [ServerCallback]
    private void Raise_OnTriggerExit(Entity entity)
    {
        targetEntities.Remove(entity);

        OnTriggerExit?.Invoke(entity);
    }

    #endregion

    #region Setup
    /// <summary>
    /// Setup for when the spell is created, this is called on the client only.
    /// </summary>
    /// <param name="identity"></param>
    /// <param name="keepPositionOnInit"></param>
    [ClientRpc]
    public void Rpc_SetupSpell(NetworkIdentity identity)
    {
        ownerCollider = identity.GetComponent<Collider2D>();
        ownerPlayerCombat = identity.GetComponent<PlayerCombat>();

        ownerPlayerCombat.OnCastingCanceled += Spell_OnCastingCanceled;

        SetInitialTransform(identity);

        SetCastingTimer(spellData.CastTime);
        vfx.SetFloat("Lifetime", spellData.LifeTime + spellData.CastTime);

        OnClientSetup();
        OnSetup();
    }

    /// <summary>
    /// Setup for when the spell is created, this is called on the server only.
    /// </summary>
    /// <param name="identity"></param>
    [ServerCallback]
    public void SC_SetupSpell(NetworkIdentity identity)
    {
        ownerCollider = identity.GetComponent<Collider2D>();
        ownerPlayerCombat = identity.GetComponent<PlayerCombat>();

        SetInitialTransform(identity);

        SetCastingTimer(spellData.CastTime);

        OnServerSetup();
        OnSetup();
    }

    /// <summary>
    /// Sets the initial transform target depending on spell type.
    /// </summary>
    /// <param name="identity"></param>
    private void SetInitialTransform(NetworkIdentity identity)
    {
        if (spellData.SpellType == SpellType.Offensive)
        {
            switch (((OffensiveSpellObject)spellData).SpellBehaviour)
            {
                case OffensiveSpellBehaviour.Aura:
                    initialTargetTransform = identity.transform;
                    break;

                case OffensiveSpellBehaviour.Skillshot:
                    initialTargetTransform = identity.transform.Find("Graphics").Find("AttackPoint");
                    break;

                case OffensiveSpellBehaviour.Target:
                    initialTargetTransform = identity.transform.Find("Graphics").Find("TargetPoint");
                    break;

                default:
                    Debug.LogError($"For whatever reason the spell data of {name} did not have any spell behaviour.", this);
                    break;
            }
        }
        else if (spellData.SpellType == SpellType.Utility)
        {
            switch (((UtilitySpellObject)spellData).spellBehaviour)
            {
                case UtilitySpellBehaviour.Teleport:
                    initialTargetTransform = identity.transform;
                    break;

                case UtilitySpellBehaviour.Dash:
                    initialTargetTransform = identity.transform.Find("Graphics");
                    break;

                case UtilitySpellBehaviour.Invisibility:
                    initialTargetTransform = identity.transform;
                    break;

                default:
                    Debug.LogError($"For whatever reason the spell data of {name} did not have any spell behaviour.", this);
                    break;
            }
        }
    }

    /// <summary>
    /// Responsible for setting up the casting timer.
    /// </summary>
    /// <param name="castTime"></param>
    private void SetCastingTimer(float castTime)
    {
        m_maxCastTimer = castTime;
        m_currentCastTimer = 0f;
        ownerPlayerCombat.IsCasting = IsCasting();
    }
    #endregion

    #region Interruption
    /// <summary>
    /// <br>Method listening on the <seealso cref="PlayerCombat.OnCastingCanceled"/> event.</br>
    /// Will destroy the spells if called.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private void Spell_OnCastingCanceled(object sender, ActionEventArgs args)
    {
        if (!IsCasting())
        {
            return;
        }
        Cmd_CancelSelf(0f);
    }

    /// <summary>
    /// <br>A command that can be called from the authorative client and executed on the server.</br>
    /// Destroys the spell.
    /// </summary>
    /// <param name="delay"></param>
    [Command]
    protected void Cmd_CancelSelf(float delay)
    {
        SC_StartDeathTimer(delay);
    }
    #endregion

    #region On Hit Callbacks
    /// <summary>
    /// <br>Responsible for telling clients that this spell has hit something.</br>
    /// <br>Remember to call <seealso cref="SC_OnHit"/> when overriden.</br>
    /// This will only be called from the server and executed on the server.
    /// </summary>
    [ServerCallback]
    protected virtual void SC_OnHit()
    {
        hitSomething = true;
        Rpc_OnHit();
    }

    /// <summary>
    /// This will be executed on all the clients connected to the server and is called when the <seealso cref="SC_OnHit"/> is called.
    /// </summary>
    [ClientRpc]
    protected virtual void Rpc_OnHit()
    {
        hitSomething = true;
        vfx.SendEvent("OnHit");
    }
    #endregion

    #region Destruction
    /// <summary>
    /// Starts a timer that destroys the spell.
    /// </summary>
    /// <param name="delay"></param>
    [ServerCallback]
    protected virtual void SC_StartDeathTimer(float delay = 3f)
    {
        StopCoroutine(m_deathRoutine);
        m_deathRoutine = StartCoroutine(SC_DestroySpellObject(gameObject, delay, 0f));
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
    #endregion

    /// <summary>
    /// Checks wether the spell is currently being cast and returns true if it is.
    /// </summary>
    /// <returns></returns>
    protected bool IsCasting()
    {
        return m_currentCastTimer < m_maxCastTimer;
    }

    #region Protected Virtual Overrides
    /// <summary>
    /// Called on the server and client when this object is initialized.
    /// </summary>
    protected virtual void OnAwake() { }
    /// <summary>
    /// Called on the server and client when usual start would be called.
    /// </summary>
    protected virtual void OnStart() { }
    /// <summary>
    /// Called only on the client.
    /// </summary>
    protected virtual void OnClientSetup() { }
    /// <summary>
    /// Called only on the server.
    /// </summary>
    [ServerCallback] protected virtual void OnServerSetup() { }
    /// <summary>
    /// Called on the server and client when they have respectively finished their setup.
    /// </summary>
    protected virtual void OnSetup() { }
    /// <summary>
    /// Called on the server and client each frame.
    /// </summary>
    protected virtual void OnUpdate() { }
    /// <summary>
    /// Called on the server and client each physics update.
    /// </summary>
    protected virtual void OnFixedUpdate() { }
    /// <summary>
    /// Called on the server and client when a spell has finished casting.
    /// </summary>
    protected virtual void OnFinishedCasting() { }
    #endregion
}
