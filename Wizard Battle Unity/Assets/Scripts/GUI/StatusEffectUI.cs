using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectUI : MonoBehaviour
{
    private StatusEffect m_statusEffect;
    private float m_currentTimer;
    [SerializeField] private Image m_effectIconImage;
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
        }
    }

    public void SetStatusEffect(StatusEffect newStatusEffect)
    {
        m_statusEffect = newStatusEffect;
        m_currentTimer = m_statusEffect.effectLifetime;
    }
}
