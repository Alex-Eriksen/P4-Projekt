using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellbook : MonoBehaviour
{
    public SpellObject PrimarySelectedSpell { get { return m_primarySelectedSpell; } }
    public SpellObject SecondarySelectedSpell { get { return m_secondarySelectedSpell; } }
    private SpellObject m_primarySelectedSpell, m_secondarySelectedSpell;

    [SerializeField] private GameObject m_spellbookUI;
    public SpellbookObject currentSpellbook;

    private void Start()
    {
        CloseSpellbook();
    }

    public void OpenSpellbook()
    {
        if (m_spellbookUI.activeSelf)
        {
            return;
        }

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
