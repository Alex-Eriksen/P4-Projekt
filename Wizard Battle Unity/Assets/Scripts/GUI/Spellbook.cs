using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Spellbook : MonoBehaviour
{
    public SpellObject PrimarySelectedSpell { get { return m_primarySelectedSpell; } }
    public SpellObject SecondarySelectedSpell { get { return m_secondarySelectedSpell; } }
    public SpellObject UtilitySelectedSpell { get { return m_utilitySelectedSpell; } }
    private SpellObject m_primarySelectedSpell, m_secondarySelectedSpell, m_utilitySelectedSpell;

    private PlayerInput m_playerInput;
    public bool IsActive { get => m_spellbookUI.activeSelf; }

    [SerializeField] private GameObject m_spellbookUI;
    public SpellbookObject currentSpellbook;
    private SpellDatabaseObject m_database;
    private Vector2 m_mousePosition;

    public UnityEvent<int> OnPrimaryChanged;
    public UnityEvent<int> OnSecondaryChanged;
    public UnityEvent<int> OnUtilityChanged;

    private void Awake()
    {
        m_playerInput = GetComponent<PlayerInput>();
        m_database = Resources.Load<SpellDatabaseObject>("Spells/SpellDatabase");
    }

    private void Start()
    {
        m_playerInput.actions["MousePosition"].performed += ctx => m_mousePosition = ctx.ReadValue<Vector2>();
        SetUtilitySpell();
        CloseSpellbook();
    }

    public void OpenSpellbook()
    {
        if (m_spellbookUI.activeSelf)
        {
            return;
        }

        m_spellbookUI.GetComponent<RectTransform>().anchoredPosition = m_mousePosition;

        m_spellbookUI.SetActive(true);
    }

    public void CloseSpellbook()
    {
        if (!m_spellbookUI.activeSelf)
        {
            return;
        }

        m_spellbookUI.SetActive(false);
    }

    public void SetPrimarySpell(SpellSlotUI sender)
    {
        m_primarySelectedSpell = sender.currentSpell;
        if(m_primarySelectedSpell == null)
        {
            OnPrimaryChanged?.Invoke(-1);
            return;
        }
        OnPrimaryChanged?.Invoke(m_primarySelectedSpell.SpellID);
    }   
    
    public void SetSecondarySpell(SpellSlotUI sender)
    {
        m_secondarySelectedSpell = sender.currentSpell;
        if(m_secondarySelectedSpell == null)
        {
            OnSecondaryChanged?.Invoke(-1);
            return;
        }
        OnSecondaryChanged?.Invoke(m_secondarySelectedSpell.SpellID);
    }

    private void SetUtilitySpell()
    {
        m_utilitySelectedSpell = m_database.spellObjects[currentSpellbook.utilitySpell.currentSpellID];
        if (m_utilitySelectedSpell == null)
        {
            OnUtilityChanged?.Invoke(-1);
            return;
        }
        OnUtilityChanged?.Invoke(m_utilitySelectedSpell.SpellID);
    }
}
