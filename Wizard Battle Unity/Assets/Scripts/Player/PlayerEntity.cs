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

    [ServerCallback]
    private void Start()
    {
        m_regenRoutine = StartCoroutine(SCRegenTicker());
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

    [Command]
    public void CmdGainHealth(float amount)
    {
        SCGainHealth(amount);
    }

    [ServerCallback]
    public void SCGainHealth(float amount)
    {
        m_health += amount;
        if (m_health > m_maxHealth)
        {
            m_health = m_maxHealth;
        }
    }

    [Command]
    public void CmdDrainHealth(float amount)
    {
        SCDrainHealth(amount);
    }

    [ServerCallback]
    public void SCDrainHealth(float amount)
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
    public void CmdGainMana(float amount)
    {
        SCGainMana(amount);
    }

    [ServerCallback]
    public void SCGainMana(float amount)
    {
        m_mana += amount;
        if (m_mana > m_maxMana)
        {
            m_mana = m_maxMana;
        }
    }

    [Command]
    public void CmdDrainMana(float amount)
    {
        bool valid = (m_mana - amount) >= 0;
        if (!valid)
        {
            Raise_CastingCanceled(ActionEventArgsFlag.NotEnoughMana);
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
    private void Raise_CastingCanceled(ActionEventArgsFlag reason)
    {
        m_playerCombat.Raise_CastingCanceled(this, reason);
    }
    #endregion

    [ServerCallback]
    private IEnumerator SCRegenTicker()
    {
        yield return new WaitForSeconds(1f);

        if(m_mana < m_maxMana)
        {
            SCGainMana(m_manaRegenRate);
        }

        if(m_health < m_maxHealth)
        {
            SCGainHealth(m_healthRegenRate);
        }

        m_regenRoutine = StartCoroutine(SCRegenTicker());
    }

    [ClientRpc]
    public void Die()
    {
        Debug.Log("Someone died!");
    }
}
