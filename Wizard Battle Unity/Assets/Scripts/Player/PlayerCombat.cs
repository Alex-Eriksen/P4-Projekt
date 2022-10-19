using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;
using UnityEditor.Experimental.GraphView;
using System;

public class PlayerCombat : NetworkBehaviour
{
    private PlayerInput m_playerInput;
    private PlayerEntity m_playerEntity;
    private Transform m_graphicsTransform;
    private Spellbook m_spellbook;
    private Coroutine m_spellCastingRoutine;

    private Vector2 m_mousePosition = Vector2.zero;
    private bool m_isCasting = false;

    public delegate void ActionEvent(object sender, ActionEventArgs args);
    public event ActionEvent OnCastingCanceled;
    public event Action<float> OnCastTimeChanged;

    private void Awake()
    {
        // Bind variables to components.
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

        // Subscribe to input events.
        m_playerInput.actions["LeftMouse"].started += LeftMouse_Started;
        m_playerInput.actions["RightMouse"].started += RightMouse_Started;
        m_playerInput.actions["MousePosition"].performed += MousePosition_Performed;
        m_playerInput.actions["Spellbook"].started += Spellbook_Started;
        m_playerInput.actions["Spellbook"].canceled += Spellbook_Canceled;
        OnCastingCanceled += PlayerEntity_CastingCanceled;
    }

    /// <summary>
    /// Method listening on the RightMouse started event. Responsible for casting the spell selected in the secondary selection.
    /// </summary>
    /// <param name="obj"></param>
    private void RightMouse_Started(InputAction.CallbackContext obj)
    {
        if (m_spellbook.SecondarySelectedSpell == null || m_isCasting)
        {
            return;
        }

        CastSpell(m_spellbook.SecondarySelectedSpell);
    }

    /// <summary>
    /// Method listening on the LeftMouse started event. Responsible for casting the spell selected in the primary selection.
    /// </summary>
    /// <param name="obj"></param>
    private void LeftMouse_Started(InputAction.CallbackContext obj)
    {
        if (m_spellbook.PrimarySelectedSpell == null || m_isCasting)
        {
            return;
        }

        CastSpell(m_spellbook.PrimarySelectedSpell);
    }

    /// <summary>
    /// Responsible for calling the server command to spawn the selected spell, and make the required prechecks.
    /// </summary>
    /// <param name="spell"></param>
    private void CastSpell(SpellObject spell)
    {
        m_isCasting = true;
        m_playerEntity.CmdDrainMana(spell.ManaCost);
        m_spellCastingRoutine = StartCoroutine(SpellCastingRoutine(spell));
    }

    /// <summary>
    /// Coroutine for handling the cast time of a spell.
    /// </summary>
    /// <param name="spell"></param>
    /// <returns></returns>
    private IEnumerator SpellCastingRoutine(SpellObject spell)
    {
        Debug.Log("Started casting spell: " + spell.SpellName);
        OnCastTimeChanged?.Invoke(spell.CastTime);
        yield return new WaitForSeconds(spell.CastTime);
        Debug.Log("Telling server to spawn spell: " + spell.SpellName);
        CmdSpawnSpell(spell.PrefabPath);
        m_isCasting = false;
    }

    /// <summary>
    /// A method called from a client and run on the server.
    /// Responsible for spawning the specified prefab <paramref name="spellPrefabPath"/> on the server and all clients.
    /// </summary>
    /// <param name="spellPrefabPath"></param>
    [Command]
    private void CmdSpawnSpell(string spellPrefabPath)
    {
        GameObject spawnedSpell = Instantiate(Resources.Load<GameObject>(spellPrefabPath));
        Spell spell = spawnedSpell.GetComponent<Spell>();
        spell.ownerCollider = connectionToClient.identity.GetComponent<Collider2D>();
        spawnedSpell.transform.SetPositionAndRotation(m_graphicsTransform.position, m_graphicsTransform.rotation);
        Debug.Log($"Spawning {spell.spellData.SpellName} on all clients...");
        NetworkServer.Spawn(spawnedSpell, connectionToClient);
    }
    
    /// <summary>
    /// Method listening on the PlayerEntity OnCastingCanceled event.
    /// Responsible for canceling the players casting coroutine and informing the player of the reason.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private void PlayerEntity_CastingCanceled(object sender, ActionEventArgs args)
    {
        StopCoroutine(m_spellCastingRoutine);
        m_isCasting = false;
        Debug.Log("Canceled Casting - Reason: " + args.Flag.ToString());
        // TODO: Add UI for displaying the ActionEventArgsFlag. (like not enough mana)
    }

    /// <summary>
    /// Method listening on the Spellbook started event.
    /// Responsible for opening the spellbook.
    /// </summary>
    /// <param name="obj"></param>
    private void Spellbook_Started(InputAction.CallbackContext obj)
    {
        m_spellbook.OpenSpellbook();
    }

    /// <summary>
    /// Method listening on the Spellbook canceled event.
    /// Responsible for closing the spellbook.
    /// </summary>
    /// <param name="obj"></param>
    private void Spellbook_Canceled(InputAction.CallbackContext obj)
    {
        m_spellbook.CloseSpellbook();
    }

    /// <summary>
    /// Method listening on the MousePosition performed event.
    /// Responsible for reading the mouse position and updating graphics.
    /// </summary>
    /// <param name="obj"></param>
    private void MousePosition_Performed(InputAction.CallbackContext obj)
    {
        m_mousePosition = obj.ReadValue<Vector2>();
        Vector3 lookPos = Camera.main.ScreenToWorldPoint(m_mousePosition);
        lookPos -= m_graphicsTransform.position;
        float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg - 90f;
        m_graphicsTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    /// <summary>
    /// A public method for raising the OnCastingCancled event from other objects.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="reason"></param>
    public void Raise_CastingCanceled(object sender, ActionEventArgsFlag reason)
    {
        OnCastingCanceled?.Invoke(sender, new ActionEventArgs(reason));
    }
}