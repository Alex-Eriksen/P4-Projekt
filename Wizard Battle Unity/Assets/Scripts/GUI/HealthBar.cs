using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class HealthBar : MonoBehaviour
{
    private const float DAMAGED_HEALTH_SHRINK_TIMER_MAX = .6f;

    [SerializeField] private Image m_barImage, m_damagedBarImage;
    [SerializeField] private Color m_damagedColor;
    [SerializeField] private PlayerEntity m_entity;
    private float m_damagedHealthShrinkTimer;

    private void Awake()
    {
        m_damagedBarImage.color = m_damagedColor;
    }

    private void Start()
    {
        SetHealth(m_entity.HealthNormalized);
        m_damagedBarImage.fillAmount = m_barImage.fillAmount;
        m_entity.OnHealthDrained += PlayerEntity_OnDamaged;
        m_entity.OnHealthGained += PlayerEntity_OnHealed;
    }

    private void Update()
    {
        m_damagedHealthShrinkTimer -= Time.deltaTime;
        if (m_damagedHealthShrinkTimer < 0)
        {
            if (m_barImage.fillAmount < m_damagedBarImage.fillAmount)
            {
                float shrinkSpeed = 1f;
                m_damagedBarImage.fillAmount -= shrinkSpeed * Time.deltaTime;
            }
        }
    }

    private void PlayerEntity_OnDamaged(object sender, System.EventArgs e)
    {
        m_damagedHealthShrinkTimer = DAMAGED_HEALTH_SHRINK_TIMER_MAX;
        SetHealth(m_entity.HealthNormalized);
    }

    private void PlayerEntity_OnHealed(object sender, System.EventArgs e)
    {
        SetHealth(m_entity.HealthNormalized);
        m_damagedBarImage.fillAmount = m_barImage.fillAmount;
    }

    private void SetHealth(float healthNormalized)
    {
        m_barImage.fillAmount = healthNormalized;
    }
}
