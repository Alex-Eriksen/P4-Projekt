using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastBar : MonoBehaviour
{
    [SerializeField] private PlayerCombat m_playerCombat;
    [SerializeField] private Image m_barImage;
    [SerializeField] private Color m_barColor;
    [SerializeField] private CanvasGroup m_canvasGroup;
    private bool m_shouldUpdate = false;
    private float m_maxCastTime = 0f;
    private float m_currentCastTime = 0f;
    private float CastTimeNormalized { get { return m_currentCastTime / m_maxCastTime; } }

    private void Awake()
    {
        m_barImage.color = m_barColor;
    }

    private void Start()
    {
        m_playerCombat.OnCastTimeChanged += PlayerCombat_OnCastTimeChanged;
        m_playerCombat.OnCastingCanceled += PlayerCombat_OnCastCanceled;
        SetCastTime(CastTimeNormalized);
        m_canvasGroup.alpha = 0f;
    }

    private void Update()
    {
        if (!m_shouldUpdate)
        {
            return;
        }

        if (m_currentCastTime >= m_maxCastTime)
        {
            m_canvasGroup.alpha = 0f;
        }
        else
        {
            m_currentCastTime += Time.deltaTime;
            SetCastTime(CastTimeNormalized);
        }
    }

    private void PlayerCombat_OnCastCanceled(object sender, ActionEventArgs args)
    {
        StopAllCoroutines();
        m_canvasGroup.alpha = 0f;
        m_shouldUpdate = false;
    }

    private void PlayerCombat_OnCastTimeChanged(float castTime)
    {
        m_maxCastTime = castTime;
        m_currentCastTime = 0f;
        m_canvasGroup.alpha = 1f;
        SetCastTime(CastTimeNormalized);
        m_shouldUpdate = true;
    }

    private void SetCastTime(float castTimeNormalized)
    {
        m_barImage.fillAmount = castTimeNormalized;
    }
}
