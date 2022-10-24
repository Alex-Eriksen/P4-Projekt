using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class PlayerEntity : NetworkBehaviour
{
    public PlayerCombat PlayerCombat { get { return m_playerCombat; } }
    [SerializeField] private PlayerCombat m_playerCombat;
    private Coroutine m_regenRoutine;
    private Transform m_transform;

    private void Awake()
    {
        m_transform = transform;
    }

    private void Start()
    {
        // Load the status effect prefabs from Resources.
        GameObject[] statusEffectPrefabs = Resources.LoadAll<GameObject>("Spells/Status Effects");
        foreach (GameObject statusEffectPrefab in statusEffectPrefabs)
        {
            Status status = statusEffectPrefab.GetComponent<Status>();
            m_statusEffectPrefabs.Add(status.statusEffectData.effectType, statusEffectPrefab);
        }
    }

    public override void OnStartServer()
    {
        m_regenRoutine = StartCoroutine(SC_RegenTicker());
    }

    public override void OnStartClient()
    {
        // When subscribing to the SyncList callback you dont get the initial values of the SyncList.
        m_statusEffects.Callback += OnStatusEffectsChanged;

        // Because we dont get the initial values we update them here manually instead.
        for(int index = 0; index < m_statusEffects.Count; index++)
        {
            OnStatusEffectsChanged(SyncList<StatusEffect>.Operation.OP_ADD, index, new StatusEffect(), m_statusEffects[index]);
        }
    }

    #region Status Effects
    private readonly Dictionary<StatusEffectType, GameObject> m_statusEffectPrefabs = new Dictionary<StatusEffectType, GameObject>();
    private readonly Dictionary<StatusEffectType, GameObject> m_activeStatusEffectObjects = new Dictionary<StatusEffectType, GameObject>();
    private readonly Dictionary<StatusEffectType, Coroutine> m_activeStatusEffects = new Dictionary<StatusEffectType, Coroutine>();
    private readonly SyncList<StatusEffect> m_statusEffects = new SyncList<StatusEffect>();

    /// <summary>
    /// Call back for when the SyncList m_statusEffects changes.
    /// </summary>
    /// <param name="op"></param>
    /// <param name="index"></param>
    /// <param name="oldStatusEffect"></param>
    /// <param name="newStatusEffect"></param>
    private void OnStatusEffectsChanged(SyncList<StatusEffect>.Operation op, int index, StatusEffect oldStatusEffect, StatusEffect newStatusEffect)
    {
        switch (op)
        {
            case SyncList<StatusEffect>.Operation.OP_ADD:
                break;

            case SyncList<StatusEffect>.Operation.OP_INSERT:
                break;

            case SyncList<StatusEffect>.Operation.OP_REMOVEAT:
                break;

            case SyncList<StatusEffect>.Operation.OP_SET:
                break;

            case SyncList<StatusEffect>.Operation.OP_CLEAR:
                break;
        }
    }

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
            m_statusEffects[SC_GetStatusEffectIndex(statusEffect)] = statusEffect;

            // Stops the destruction coroutine of the status effect and starts a new one.
            StopCoroutine(m_activeStatusEffects[statusEffect.effectType]);
            m_activeStatusEffects[statusEffect.effectType] = StartCoroutine(StatusEffectDeathTimer(m_activeStatusEffectObjects[statusEffect.effectType], statusEffect));
        }
        else
        {
            m_statusEffects.Add(statusEffect);

            // Spawns the status effect on and sets it in the relative dictionaries for tracking.
            GameObject obj = Instantiate(m_statusEffectPrefabs[statusEffect.effectType], m_transform);
            m_activeStatusEffects.Add(statusEffect.effectType, StartCoroutine(StatusEffectDeathTimer(obj, statusEffect)));
            m_activeStatusEffectObjects.Add(statusEffect.effectType, obj);
            NetworkServer.Spawn(obj);
        }
    }

    /// <summary>
    /// Coroutine responsible for destroying the status effect gameobject when it has depleted.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="statusEffect"></param>
    /// <returns></returns>
    [ServerCallback]
    private IEnumerator StatusEffectDeathTimer(GameObject obj, StatusEffect statusEffect)
    {
        yield return new WaitForSeconds(statusEffect.effectLifetime);
        m_statusEffects.RemoveAt(SC_GetStatusEffectIndex(statusEffect));
        m_activeStatusEffects.Remove(statusEffect.effectType);
        m_activeStatusEffectObjects.Remove(statusEffect.effectType);
        NetworkServer.Destroy(obj);
    }

    /// <summary>
    /// Method for checking if the status effect is already on the player entity.
    /// </summary>
    /// <param name="newStatusEffect"></param>
    /// <returns></returns>
    [ServerCallback]
    private bool SC_ContainsStatusEffect(StatusEffect newStatusEffect)
    {
        foreach (StatusEffect statusEffect in m_statusEffects)
        {
            if(statusEffect.effectType == newStatusEffect.effectType)
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
    private int SC_GetStatusEffectIndex(StatusEffect newStatusEffect)
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

    #region Health
    public event EventHandler OnHealthDrained; 
    public event EventHandler OnHealthGained;

    public float Health { get { return m_health; } }
    public float HealthNormalized { get { return m_health / m_maxHealth; } }
    public float MaxHealth { get { return m_maxHealth; } }
    [SyncVar(hook = nameof(Raise_HealthChanged)), SerializeField] private float m_health = 100f;
    [SyncVar] private float m_maxHealth = 100f;
    private float m_healthRegenRate = 2f;

    [Command]
    public void Cmd_GainHealth(float amount)
    {
        SC_GainHealth(amount);
    }

    [ServerCallback]
    public void SC_GainHealth(float amount)
    {
        m_health += amount;
        if (m_health > m_maxHealth)
        {
            m_health = m_maxHealth;
        }
    }

    [Command]
    public void Cmd_DrainHealth(float amount)
    {
        SC_DrainHealth(amount);
    }

    [ServerCallback]
    public void SC_DrainHealth(float amount)
    {
        m_health -= amount;
        if (m_health <= 0f)
        {
            m_health = 0f;
            Die();
        }
    }

    private void Raise_HealthChanged(float oldHealth, float newHealth)
    {
        if(oldHealth > newHealth)
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

    [Command]
    public void Cmd_GainMana(float amount)
    {
        SC_GainMana(amount);
    }

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
        bool valid = (m_mana - amount) >= 0;
        if (!valid)
        {
            Raise_CastingCanceled(ActionEventArgsFlag.NotEnoughMana, "Casting Canceled");
            return;
        }

        m_mana -= amount;
    }

    private void Raise_ManaChanged(float oldMana, float newMana)
    {
        if(oldMana > newMana)
        {
            OnManaDrained?.Invoke(this, ActionEventArgs.Empty);
        }
        else if(newMana > oldMana)
        {
            OnManaGained?.Invoke(this, ActionEventArgs.Empty);
        }
    }

    [TargetRpc]
    private void Raise_CastingCanceled(ActionEventArgsFlag reason, string message)
    {
        m_playerCombat.Raise_CastingCanceled(this, reason, message);
    }
    #endregion

    [ServerCallback]
    private IEnumerator SC_RegenTicker()
    {
        yield return new WaitForSeconds(1f);

        if(m_mana < m_maxMana)
        {
            SC_GainMana(m_manaRegenRate);
        }

        if(m_health < m_maxHealth)
        {
            SC_GainHealth(m_healthRegenRate);
        }

        m_regenRoutine = StartCoroutine(SC_RegenTicker());
    }

    [ClientRpc]
    public void Die()
    {
        Debug.Log("Someone died!");
    }
}
