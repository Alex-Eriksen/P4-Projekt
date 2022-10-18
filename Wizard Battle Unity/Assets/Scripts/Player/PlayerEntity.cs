using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class PlayerEntity : NetworkBehaviour
{
    #region Health
    public event EventHandler OnHealthDrained; 
    public event EventHandler OnHealthGained; 

    public float Health { get { return m_health; } }
    public float HealthNormalized { get { return m_health / m_maxHealth; } }
    public float MaxHealth { get { return m_maxHealth; } }
    [SyncVar(hook = nameof(Raise_HealthChanged)), SerializeField] private float m_health = 100f;
    [SyncVar] private float m_maxHealth = 100f;

    [Command]
    public void CmdGainHealth(float amount)
    {
        m_health += amount;
        if(m_health > m_maxHealth)
        {
            m_health = m_maxHealth;
        }
    }

    [Command]
    public void CmdDrainHealth(float amount)
    {
        m_health -= amount;
        if(m_health <= 0f)
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
    public float ManaNormalized { get { return m_mana / m_maxMana; } }
    public float MaxMana { get { return m_maxMana; } }
    [SyncVar(hook = nameof(Raise_ManaChanged)), SerializeField] private float m_mana = 100f;
    [SyncVar] private float m_maxMana = 100f;

    [Command]
    public void CmdGainMana(float amount)
    {
        m_mana += amount;
        if(m_mana > m_maxMana)
        {
            m_mana = m_maxMana;
        }
    }

    [Command]
    public void CmdDrainMana(float amount)
    {
        m_mana -= amount;
        if(m_mana <= 0f)
        {
            m_mana = 0f;
        }
    }

    private void Raise_ManaChanged(float oldMana, float newMana)
    {
        if(oldMana > newMana)
        {
            OnManaDrained?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            OnManaGained?.Invoke(this, EventArgs.Empty);
        }
    }
    #endregion

    [ClientRpc]
    public void Die()
    {
        Debug.Log("Someone died!");
    }
}
