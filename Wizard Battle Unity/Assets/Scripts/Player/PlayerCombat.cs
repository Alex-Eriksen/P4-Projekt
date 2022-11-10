using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;
using System;
using System.Linq;

public class PlayerCombat : NetworkBehaviour
{
    private PlayerConnection m_playerConnection;
    private PlayerInput m_playerInput;
    private PlayerEntity m_playerEntity;
    private Transform m_graphicsTransform, m_targetPoint;
    private Spellbook m_spellbook;
    private Coroutine m_spellCastingRoutine;
    private SpellObject m_spellToCast;
    private Animator m_animator;
    private ActionNotificationHandler m_notificationHandler;

    public Vector2 MousePosition { get { return m_mousePosition; } }
    private Vector2 m_mousePosition = Vector2.zero;
    public bool IsCasting = false;

    public delegate void ActionEvent(object sender, ActionEventArgs args);
    public event ActionEvent OnCastingCanceled;
    public event Action<float> OnCastTimeChanged;

    private void Update()
    {
        if (!isClient)
        {
            return;
        }

        m_animator.SetBool("Attacking", IsCasting);
    }

    public override void OnStartAuthority()
    {
        // Bind variables to components.
        m_playerConnection = FindObjectsOfType<PlayerConnection>().Where(x => x.isLocalPlayer == true).Single();
        m_playerInput = m_playerConnection.PlayerInput;
        m_playerEntity = GetComponent<PlayerEntity>();
        m_graphicsTransform = transform.GetChild(1);
        m_targetPoint = m_graphicsTransform.Find("TargetPoint");
        m_spellbook = FindObjectOfType<Spellbook>();
        m_animator = GetComponentInChildren<Animator>();
        m_notificationHandler = ActionNotificationHandler.Instance;

        SetInput();
    }

    private void SetInput()
    {
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
        if (m_spellbook.SecondarySelectedSpell == null || IsCasting || m_spellbook.IsActive)
        {
            return;
        }

        if (m_playerEntity.ContainsStatusEffect(StatusEffectType.Stun))
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
        if (m_spellbook.PrimarySelectedSpell == null || IsCasting || m_spellbook.IsActive)
        {
            return;
        }

        if (m_playerEntity.ContainsStatusEffect(StatusEffectType.Stun))
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
        m_spellToCast = spell;
        m_playerEntity.OnManaDrained += PlayerEntity_OnManaDrained;
        m_playerEntity.Cmd_DrainMana(spell.ManaCost);
    }

    /// <summary>
    /// Method listening on the PlayerEntity OnManaDrained event.
    /// Responsible for registering a successfull mana drain when a spell is about to be cast.
    /// If the mana drain was successfull the listener will be unsubscribed in the method.
    /// If the mana drain was unsuccessfull the listener will be unsubscribed in the OnCastingCanceled event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private void PlayerEntity_OnManaDrained(object sender, EventArgs args)
    {
        m_spellCastingRoutine = StartCoroutine(SpellCastingRoutine(m_spellToCast));
        m_playerEntity.OnManaDrained -= PlayerEntity_OnManaDrained;
    }

    /// <summary>
    /// Coroutine for handling the cast time of a spell.
    /// </summary>
    /// <param name="spell"></param>
    /// <returns></returns>
    private IEnumerator SpellCastingRoutine(SpellObject spell)
    {
        OnCastTimeChanged?.Invoke(spell.CastTime);
        Cmd_SpawnSpell(spell.PrefabPath);

        yield return new WaitForSeconds(spell.CastTime);
    }

    /// <summary>
    /// A method called from a client and run on the server.
    /// Responsible for spawning the specified prefab <paramref name="spellPrefabPath"/> on the server and all clients.
    /// </summary>
    /// <param name="spellPrefabPath"></param>
    [Command]
    private void Cmd_SpawnSpell(string spellPrefabPath)
    {
        GameObject spawnedSpell = Instantiate(Resources.Load<GameObject>(spellPrefabPath));
        NetworkServer.Spawn(spawnedSpell, connectionToClient);

        Spell spell = spawnedSpell.GetComponent<Spell>();
        var conn = connectionToClient.identity.GetComponent<PlayerConnection>().wizardIdentity;
        spell.SC_SetupSpell(conn);
        spell.Rpc_SetupSpell(conn);
    }
    
    /// <summary>
    /// Method listening on the PlayerEntity OnCastingCanceled event.
    /// Responsible for canceling the players casting coroutine and informing the player of the reason.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private void PlayerEntity_CastingCanceled(object sender, ActionEventArgs args)
    {
        if(m_spellCastingRoutine != null)
        {
            StopCoroutine(m_spellCastingRoutine);
        }
        m_playerEntity.OnManaDrained -= PlayerEntity_OnManaDrained;
        m_animator.SetBool("Attacking", IsCasting);
        m_notificationHandler.AddActionEventNotification(sender, args);
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
        if (m_playerEntity.ContainsStatusEffect(StatusEffectType.Stun))
        {
            return;
        }
        m_mousePosition = obj.ReadValue<Vector2>();
        Vector3 lookPos = Camera.main.ScreenToWorldPoint(m_mousePosition);
        m_targetPoint.position = new Vector3(lookPos.x, lookPos.y, 0f);

        lookPos -= m_graphicsTransform.position;
        float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg - 90f;
        m_graphicsTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    /// <summary>
    /// A public method for raising the OnCastingCancled event from other objects.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="reason"></param>
    public void Raise_CastingCanceled(object sender, ActionEventArgsFlag reason, string message)
    {
        OnCastingCanceled?.Invoke(sender, new ActionEventArgs(reason, message));
    }
}