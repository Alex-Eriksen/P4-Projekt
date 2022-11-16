using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class Entity : NetworkBehaviour
{
    public string entityName = "Unnamed";
    protected Coroutine regenRoutine;
    protected Transform m_transform;
    protected bool isReadyToDestroy = false;

    private void Awake()
    {
        m_transform = base.transform;

        OnAwake();
    }

    private void Start()
    {
        // Load the status effect prefabs from Resources.
        GameObject[] statusEffectPrefabs = Resources.LoadAll<GameObject>("Spells/Status Effects");
        foreach (GameObject statusEffectPrefab in statusEffectPrefabs)
        {
            Status status = statusEffectPrefab.GetComponent<Status>();
            m_statusEffectPrefabs.Add(status.StatusEffectData.effectType, statusEffectPrefab);
        }

        OnStart();
    }

    private void Update()
    {
        OnUpdate();
    }

    #region Health
    public event EventHandler OnHealthDrained;
    public event EventHandler OnHealthGained;

    public float Health { get { return m_health; } }
    public float HealthNormalized { get { return m_health / m_maxHealth; } }
    public float MaxHealth { get { return m_maxHealth; } }
    [SyncVar(hook = nameof(Raise_HealthChanged)), SerializeField] private float m_health = 100f;
    [SyncVar] private float m_maxHealth = 100f;
    private float m_healthRegenRate = 2f;

    [ServerCallback]
    public void SC_GainHealth(float amount)
    {
        m_health += amount;
        if (m_health > m_maxHealth)
        {
            m_health = m_maxHealth;
        }
    }

    [ServerCallback]
    public void SC_DrainHealth(float amount)
    {
        if (SC_ContainsStatusEffect(StatusEffectType.Invulnerable))
        {
            return;
        }
        m_health -= amount;
        if (m_health <= 0f)
        {
            m_health = 0f;
            SC_Die();
        }
    }

    private void Raise_HealthChanged(float oldHealth, float newHealth)
    {
        if (oldHealth > newHealth)
        {
            OnHealthDrained?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            OnHealthGained?.Invoke(this, EventArgs.Empty);
        }
    }
    #endregion

    #region Mana
    public event EventHandler OnManaDrained;
    public event EventHandler OnManaGained;

    public float Mana { get { return m_mana; } }
    public float MaxMana { get { return m_maxMana; } }
    public float ManaNormalized { get { return m_mana / m_maxMana; } }
    [SyncVar(hook = nameof(Raise_ManaChanged)), SerializeField] private float m_mana = 100f;
    [SyncVar] private float m_maxMana = 100f;
    private float m_manaRegenRate = 6f;

    [ServerCallback]
    public void SC_GainMana(float amount)
    {
        m_mana += amount;
        if (m_mana > m_maxMana)
        {
            m_mana = m_maxMana;
        }
    }

    [Command]
    public void Cmd_DrainMana(float amount)
    {
        if (!CheckMana(amount))
        {
            return;
        }
        m_mana -= amount;
        OnManaDrained?.Invoke(this, EventArgs.Empty);
    }

    public bool CheckMana(float amount)
    {
        return (m_mana - amount) >= 0;
    }

    private void Raise_ManaChanged(float oldMana, float newMana)
    {
        if (oldMana > newMana)
        {
            OnManaDrained?.Invoke(this, ActionEventArgs.Empty);
        }
        else if (newMana > oldMana)
        {
            OnManaGained?.Invoke(this, ActionEventArgs.Empty);
        }
    }
    #endregion

    #region Status Effects
    protected readonly Dictionary<StatusEffectType, GameObject> m_statusEffectPrefabs = new Dictionary<StatusEffectType, GameObject>();
    protected readonly Dictionary<StatusEffectType, GameObject> m_activeStatusEffectObjects = new Dictionary<StatusEffectType, GameObject>();
    protected readonly Dictionary<StatusEffectType, GameObject> m_activeUIStatusEffects = new Dictionary<StatusEffectType, GameObject>();
    protected readonly Dictionary<StatusEffectType, Coroutine> m_activeStatusEffectsRoutines = new Dictionary<StatusEffectType, Coroutine>();
    protected readonly SyncList<StatusEffect> m_statusEffects = new SyncList<StatusEffect>();

    /// <summary>
    /// Adds a status effect to the player entity.
    /// </summary>
    /// <param name="statusEffect"></param>
    [ServerCallback]
    public void SC_AddStatusEffect(StatusEffect statusEffect)
    {
        // Checks if the player entity already has the status effect.
        if (SC_ContainsStatusEffect(statusEffect))
        {
            m_statusEffects.Remove(statusEffect);
            m_statusEffects.Add(statusEffect);

            // Stops the destruction coroutine of the status effect and starts a new one.
            StopCoroutine(m_activeStatusEffectsRoutines[statusEffect.effectType]);
            m_activeStatusEffectsRoutines[statusEffect.effectType] = StartCoroutine(StatusEffectDeathTimer(m_activeStatusEffectObjects[statusEffect.effectType], statusEffect));
            Status statusScript = m_activeStatusEffectObjects[statusEffect.effectType].GetComponent<Status>();
            statusScript.Rpc_ResetStatus();
        }
        else
        {
            m_statusEffects.Add(statusEffect);

            // Spawns the status effect on and sets it in the relative dictionaries for tracking.
            GameObject obj = Instantiate(m_statusEffectPrefabs[statusEffect.effectType]);
            m_activeStatusEffectsRoutines.Add(statusEffect.effectType, StartCoroutine(StatusEffectDeathTimer(obj, statusEffect)));
            m_activeStatusEffectObjects.Add(statusEffect.effectType, obj);
            NetworkServer.Spawn(obj);
            Status statusScript = m_activeStatusEffectObjects[statusEffect.effectType].GetComponent<Status>();
            statusScript.SC_ServerSetup(m_transform);
            statusScript.Rpc_ClientSetup(m_transform);
            statusScript.Rpc_ResetStatus();
        }
    }

    /// <summary>
    /// Coroutine responsible for destroying the status effect gameobject when it has depleted.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="statusEffect"></param>
    /// <returns></returns>
    [ServerCallback]
    protected IEnumerator StatusEffectDeathTimer(GameObject obj, StatusEffect statusEffect)
    {
        yield return new WaitForSeconds(statusEffect.effectLifetime);
        m_statusEffects.RemoveAt(SC_GetStatusEffectIndex(statusEffect));
        m_activeStatusEffectsRoutines.Remove(statusEffect.effectType);
        m_activeStatusEffectObjects.Remove(statusEffect.effectType);
        NetworkServer.Destroy(obj);
    }

    /// <summary>
    /// Method for checking if the status effect is already on the player entity.
    /// </summary>
    /// <param name="newStatusEffect"></param>
    /// <returns></returns>
    [ServerCallback]
    protected bool SC_ContainsStatusEffect(StatusEffect newStatusEffect)
    {
        foreach (StatusEffect statusEffect in m_statusEffects)
        {
            if (statusEffect.effectType == newStatusEffect.effectType)
            {
                return true;
            }
        }
        return false;
    }

    [ServerCallback]
    protected bool SC_ContainsStatusEffect(StatusEffectType statusEffectType)
    {
        foreach (StatusEffect statusEffect in m_statusEffects)
        {
            if (statusEffect.effectType == statusEffectType)
            {
                return true;
            }
        }
        return false;
    }

    public bool ContainsStatusEffect(StatusEffectType effectType)
    {
        foreach (StatusEffect statusEffect in m_statusEffects)
        {
            if (statusEffect.effectType == effectType)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Gets the status effect index in the m_statusEffects SyncList
    /// </summary>
    /// <param name="newStatusEffect"></param>
    /// <returns></returns>
    [ServerCallback]
    protected int SC_GetStatusEffectIndex(StatusEffect newStatusEffect)
    {
        for (int i = 0; i < m_statusEffects.Count; i++)
        {
            if (m_statusEffects[i].effectType == newStatusEffect.effectType)
            {
                return i;
            }
        }
        return -1;
    }
    #endregion

    [ServerCallback]
    protected IEnumerator SC_RegenTicker()
    {
        yield return new WaitForSeconds(1f);

        if (m_mana < m_maxMana)
        {
            SC_GainMana(m_manaRegenRate);
        }

        if (m_health < m_maxHealth)
        {
            SC_GainHealth(m_healthRegenRate);
        }

        regenRoutine = StartCoroutine(SC_RegenTicker());
    }

    [ClientRpc]
    private void Rpc_Die()
    {
        OnClientDeath();
        StartCoroutine(CC_DestroyWhenReady());
    }

    [ServerCallback]
    private void SC_Die()
    {
        OnServerDeath();
        Rpc_Die();
        StartCoroutine(SC_DestroyWhenReady());
        isReadyToDestroy = true;
    }

    [ServerCallback]
    private IEnumerator SC_DestroyWhenReady()
    {
        yield return new WaitUntil(() => isReadyToDestroy == true);
        NetworkServer.Destroy(gameObject);
    }

    [ClientCallback]
    private IEnumerator CC_DestroyWhenReady()
    {
        yield return new WaitUntil(() => isReadyToDestroy == true);
        Destroy(gameObject);
    }

    #region Virtual Methods
    protected virtual void OnAwake() { }
    protected virtual void OnStart() { }
    protected virtual void OnUpdate() { }
    [ClientCallback] protected virtual void OnClientDeath() { }
    [ServerCallback] protected virtual void OnServerDeath() { }
    #endregion
}
