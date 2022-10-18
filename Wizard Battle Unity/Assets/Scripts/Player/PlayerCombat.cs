using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;
using UnityEditor.Experimental.GraphView;

public class PlayerCombat : NetworkBehaviour
{
    private PlayerInput m_playerInput;
    private PlayerEntity m_playerEntity;
    private bool m_isCasting;
    private Vector2 m_mousePosition = Vector2.zero;
    private Transform m_graphicsTransform;

    private Spellbook m_spellbook;

    private void Awake()
    {
        m_playerInput = GetComponent<PlayerInput>();
        m_playerEntity = GetComponent<PlayerEntity>();
        m_graphicsTransform = transform.GetChild(1);
        m_spellbook = FindObjectOfType<Spellbook>();
    }

    private void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        m_playerInput.actions["LeftMouse"].started += LeftMouse_Started;
        m_playerInput.actions["RightMouse"].started += RightMouse_Started;
        m_playerInput.actions["MousePosition"].performed += MousePosition_Performed;
        m_playerInput.actions["Spellbook"].started += Spellbook_Started;
        m_playerInput.actions["Spellbook"].canceled += Spellbook_Canceled;
    }

    private void RightMouse_Started(InputAction.CallbackContext obj)
    {
        if (m_spellbook.SecondarySelectedSpell == null || m_isCasting)
        {
            return;
        }

        SpawnSpell(m_spellbook.SecondarySelectedSpell);
    }

    private void LeftMouse_Started(InputAction.CallbackContext obj)
    {
        if (m_spellbook.PrimarySelectedSpell == null || m_isCasting)
        {
            return;
        }

        SpawnSpell(m_spellbook.PrimarySelectedSpell);
    }

    private void SpawnSpell(SpellObject spell)
    {
        //m_isCasting = true;
        m_playerEntity.CmdDrainMana(spell.ManaCost);
        CmdSpawnSpell(spell.PrefabPath);
    }

    [Command]
    private void CmdSpawnSpell(string spellPrefabPath)
    {
        GameObject spawnedSpell = Instantiate(Resources.Load<GameObject>(spellPrefabPath));
        spawnedSpell.GetComponent<Spell>().ownerCollider = GetComponent<Collider2D>();
        spawnedSpell.transform.SetPositionAndRotation(m_graphicsTransform.position, m_graphicsTransform.rotation);
        NetworkServer.Spawn(spawnedSpell, connectionToClient);
    }

    private void Spellbook_Started(InputAction.CallbackContext obj)
    {
        m_spellbook.OpenSpellbook();
    }

    private void Spellbook_Canceled(InputAction.CallbackContext obj)
    {
        m_spellbook.CloseSpellbook();
    }

    private void MousePosition_Performed(InputAction.CallbackContext obj)
    {
        m_mousePosition = obj.ReadValue<Vector2>();
        Vector3 lookPos = Camera.main.ScreenToWorldPoint(m_mousePosition);
        lookPos -= m_graphicsTransform.position;
        float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg - 90f;
        m_graphicsTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}