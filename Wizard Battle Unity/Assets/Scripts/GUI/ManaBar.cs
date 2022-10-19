using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class ManaBar : MonoBehaviour
{
    private const float MANA_SPENT_SHRINK_TIMER_MAX = .6f;

    [SerializeField] private Image m_barImage, m_manaSpentBarImage;
    [SerializeField] private Color m_manaSpentColor;
    [SerializeField] private PlayerEntity m_entity;
    private float m_manaSpentShrinkTimer;

    private void Awake()
    {
        m_manaSpentBarImage.color = m_manaSpentColor;
    }

    private void Start()
    {
        SetMana(m_entity.ManaNormalized);
        m_manaSpentBarImage.fillAmount = m_barImage.fillAmount;
        m_entity.OnManaDrained += PlayerEntity_OnManaDrained;
        m_entity.OnManaGained += PlayerEntity_OnManaGained;
    }

    private void Update()
    {
        m_manaSpentShrinkTimer -= Time.deltaTime;
        if (m_manaSpentShrinkTimer < 0)
        {
            if (m_barImage.fillAmount < m_manaSpentBarImage.fillAmount)
            {
                float shrinkSpeed = 1f;
                m_manaSpentBarImage.fillAmount -= shrinkSpeed * Time.deltaTime;
            }
        }
    }

    private void PlayerEntity_OnManaDrained(object sender, System.EventArgs e)
    {
        m_manaSpentShrinkTimer = MANA_SPENT_SHRINK_TIMER_MAX;
        SetMana(m_entity.ManaNormalized);
    }

    private void PlayerEntity_OnManaGained(object sender, System.EventArgs e)
    {
        SetMana(m_entity.ManaNormalized);
        m_manaSpentBarImage.fillAmount = m_barImage.fillAmount;
    }

    private void SetMana(float healthNormalized)
    {
        m_barImage.fillAmount = healthNormalized;
    }
}
