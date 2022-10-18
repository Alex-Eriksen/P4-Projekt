using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class PlayerCombat : NetworkBehaviour
{
    private PlayerInput m_playerInput;
    private PlayerEntity m_playerEntity;
    private SpellCaster m_spellCaster;
    private bool m_isCasting;

    private void Awake()
    {
        m_playerInput = GetComponent<PlayerInput>();
        m_playerEntity = GetComponent<PlayerEntity>();
        m_spellCaster = GetComponent<SpellCaster>();
    }

    private void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        m_playerInput.actions["LeftMouse"].started += LeftMouse_Started;
        m_playerInput.actions["RightMouse"].started += RightMouse_Started;
    }

    private void RightMouse_Started(InputAction.CallbackContext obj)
    {
        if(m_spellCaster.Spellbook.PrimarySelectedSpell == null || m_isCasting)
        {
            return;
        }

        m_isCasting = true;
        m_playerEntity.CmdDrainMana(m_spellCaster.Spellbook.PrimarySelectedSpell.ManaCost);
        CmdSpawnSpell(Resources.Load<GameObject>(m_spellCaster.Spellbook.PrimarySelectedSpell.PrefabPath));
    }

    private void LeftMouse_Started(InputAction.CallbackContext obj)
    {
        if (m_spellCaster.Spellbook.SecondarySelectedSpell == null || m_isCasting)
        {
            return;
        }

        m_isCasting = true;
        m_playerEntity.CmdDrainMana(m_spellCaster.Spellbook.SecondarySelectedSpell.ManaCost);
        CmdSpawnSpell(Resources.Load<GameObject>(m_spellCaster.Spellbook.SecondarySelectedSpell.PrefabPath));
    }

    [Command]
    private void CmdSpawnSpell(GameObject spellPrefab)
    {
        NetworkServer.Spawn(spellPrefab);
    }
}
