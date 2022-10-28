using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [HideInInspector] public SpellObject currentSpell;
    private Spellbook m_spellbook;
    private Image m_spellImage, m_slotImage;
    private SpellDatabaseObject m_spellDatabase;
    private int m_index;

    private void Awake()
    {
        m_spellImage = transform.GetChild(0).GetComponent<Image>();
        m_slotImage = GetComponent<Image>();
        m_spellbook = GetComponentInParent<Spellbook>();
        m_spellDatabase = Resources.Load<SpellDatabaseObject>("Spells/SpellDatabase");
        m_index = int.Parse(gameObject.name[^1..]);
    }

    private void Start()
    {
        int spellIndex = m_spellbook.currentSpellbook.spellSlots[m_index - 1].currentSpellID;
        if(spellIndex == -1)
        {
            return;
        }
        currentSpell = m_spellDatabase.spellObjects[spellIndex];
        if(currentSpell == null)
        {
            m_slotImage.color = new Color(255, 255, 255, 0.75f);
            return;
        }

        m_spellImage.sprite = currentSpell.SpellIcon;
        m_spellImage.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            m_spellbook.SetPrimarySpell(this);
        }

        if(eventData.button == PointerEventData.InputButton.Right)
        {
            m_spellbook.SetSecondarySpell(this);
        }

        m_spellbook.CloseSpellbook();
    }
}
