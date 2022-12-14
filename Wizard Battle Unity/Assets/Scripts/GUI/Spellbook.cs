using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Spellbook : MonoBehaviour
{
    public SpellObject PrimarySelectedSpell { get { return m_primarySelectedSpell; } }
    public SpellObject SecondarySelectedSpell { get { return m_secondarySelectedSpell; } }
    private SpellObject m_primarySelectedSpell, m_secondarySelectedSpell;

    private PlayerInput m_playerInput;

    [SerializeField] private GameObject m_spellbookUI;
    public SpellbookObject currentSpellbook;
    private Vector2 m_mousePosition;

    private void Awake()
    {
        m_playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        m_playerInput.actions["MousePosition"].performed += ctx => m_mousePosition = ctx.ReadValue<Vector2>();
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
    }   
    
    public void SetSecondarySpell(SpellSlotUI sender)
    {
        m_secondarySelectedSpell = sender.currentSpell;
    }
}
