using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectUI : MonoBehaviour
{
    private StatusEffect m_statusEffect;
    private float m_currentTimer, m_maxTime;
    [SerializeField] private Image m_effectIconImage, m_timeDisplayImage;
    [SerializeField] private TextMeshProUGUI m_timerText;

    private void Update()
    {
        if(m_currentTimer <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            m_currentTimer -= Time.deltaTime;
            m_timerText.text = Mathf.RoundToInt(m_currentTimer).ToString();
            m_timeDisplayImage.fillAmount = m_currentTimer / m_maxTime;
        }
    }

    public void SetStatusEffect(StatusEffect newStatusEffect)
    {
        m_statusEffect = newStatusEffect;
        m_maxTime = m_statusEffect.effectLifetime;
        m_currentTimer = m_statusEffect.effectLifetime;
        m_effectIconImage.sprite = Resources.Load<StatusEffectObject>($"Spells/Status Effects/{m_statusEffect.effectType} Status/{m_statusEffect.effectType} Status").effectIcon;
        m_effectIconImage.color = Color.white;
    }

    public void ResetStatusEffect()
    {
        SetStatusEffect(m_statusEffect);
    }
}
