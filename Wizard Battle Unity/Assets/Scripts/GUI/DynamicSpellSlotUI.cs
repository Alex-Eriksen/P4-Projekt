using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DynamicSpellSlotUI : MonoBehaviour
{
    private SpellDatabaseObject m_spellDatabase;
    private Image m_spellImage, m_slotImage;
    [SerializeField] private int m_index = -1;
    private SpellObject m_currentSpell;

    private void Awake()
    {
        m_spellImage = transform.GetChild(0).GetComponent<Image>();
        m_slotImage = GetComponent<Image>();
        m_spellDatabase = Resources.Load<SpellDatabaseObject>("Spells/SpellDatabase");
    }

    public void ChangeIcon(int newIndex)
    {
        m_index = newIndex;
        
        if(m_index == -1)
        {
            m_spellImage.color = new Color(255, 255, 255, 0f);
            return;
        }

        m_currentSpell = m_spellDatabase.spellObjects[m_index];
        m_spellImage.sprite = m_currentSpell.SpellIcon;
        m_spellImage.color = Color.white;
    }
}
