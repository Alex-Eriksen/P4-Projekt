using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using TMPro;
using System.Linq;

public class PlayerEntity : Entity
{
    public PlayerCombat PlayerCombat { get { return m_playerCombat; } }
    private PlayerConnection m_playerConnection;
    [SyncVar(hook = nameof(SetPlayerName))] private string m_playerName;
    [SerializeField] private PlayerCombat m_playerCombat;
    [SerializeField] private bool m_isTargetDummy = false;
    [SerializeField] private GameObject m_statusEffectUIPrefab;
    private Transform m_statusEffectsUI;

    protected override void OnAwake()
    {
        m_statusEffectsUI = GameObject.FindGameObjectWithTag("Status Effects").transform;
    }

    public override void OnStartServer()
    {
        m_regenRoutine = StartCoroutine(SC_RegenTicker());
        if (m_isTargetDummy)
        {
            m_playerName = "Target Dummy";
        }
    }

    public override void OnStartAuthority()
    {
        m_playerConnection = FindObjectsOfType<PlayerConnection>().Where(x => x.isLocalPlayer == true).Single();
        Cmd_SetPlayerName(m_playerConnection.PlayerName);

        // When subscribing to the SyncList callback you dont get the initial values of the SyncList.
        m_statusEffects.Callback += OnStatusEffectsChanged;

        // Because we dont get the initial values we update them here manually instead.
        for (int index = 0; index < m_statusEffects.Count; index++)
        {
            OnStatusEffectsChanged(SyncList<StatusEffect>.Operation.OP_ADD, index, new StatusEffect(), m_statusEffects[index]);
        }
    }

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
                GameObject obj = Instantiate(m_statusEffectUIPrefab, m_statusEffectsUI);
                StatusEffectUI statusScript = obj.GetComponent<StatusEffectUI>();
                statusScript.SetStatusEffect(newStatusEffect);
                m_activeUIStatusEffects.Add(newStatusEffect.effectType, obj);
                if (newStatusEffect.effectType == StatusEffectType.Stun)
                {
                    m_playerCombat.Raise_CastingCanceled(this, ActionEventArgsFlag.Stunned, "Status Effect");
                }
                break;

            case SyncList<StatusEffect>.Operation.OP_INSERT:
                break;

            case SyncList<StatusEffect>.Operation.OP_REMOVEAT:
                Destroy(m_activeUIStatusEffects[oldStatusEffect.effectType]);
                m_activeUIStatusEffects.Remove(oldStatusEffect.effectType);
                break;

            case SyncList<StatusEffect>.Operation.OP_SET:
                break;

            case SyncList<StatusEffect>.Operation.OP_CLEAR:
                break;
        }
    }

    [Command]
    private void Cmd_SetPlayerName(string playerName)
    {
        m_playerName = playerName;
    }

    private void SetPlayerName(string oldName, string newName)
    {
        m_transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = newName;
    }

    protected override void OnServerDeath()
    {
        Debug.Log($"{m_playerName} died!");
    }

    protected override void OnClientDeath()
    {
        Debug.Log($"{m_playerName} died!");
    }
}
