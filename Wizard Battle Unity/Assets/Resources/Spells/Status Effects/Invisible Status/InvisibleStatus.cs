using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class InvisibleStatus : Status
{
    private SpriteRenderer m_targetSpriteRenderer;
    private CanvasGroup m_targetUI;
    private PlayerCombat m_targetPlayerCombat;
    private Color m_originalColor;
    private ShadowCaster2D m_shadowCaster;
    [SerializeField] private Color m_targetColor, m_localTargetColor;

    protected override void OnSetup()
    {
        m_targetSpriteRenderer = target.transform.Find("Graphics").GetComponent<SpriteRenderer>();
        m_targetUI = target.GetComponentInChildren<CanvasGroup>();
        m_targetPlayerCombat = target.GetComponent<PlayerCombat>();
        m_shadowCaster = target.GetComponentInChildren<ShadowCaster2D>();
        m_originalColor = m_targetSpriteRenderer.color;
    }

    protected override void OnClientSetup()
    {
        m_targetPlayerCombat.Casting_Started += Casting_Started;
        if (target.hasAuthority)
        {
            LocalStartInvisible();
            return;
        }
        StartInvisible();
    }

    private void Casting_Started()
    {
        EndInvisible();
    }

    private void StartInvisible()
    {
        m_targetSpriteRenderer.color = m_targetColor;
        m_targetUI.alpha = 0f;
        m_shadowCaster.enabled = false;
    }
    private void EndInvisible()
    {
        m_targetSpriteRenderer.color = m_originalColor;
        m_targetUI.alpha = 1f;
        m_shadowCaster.enabled = true;
    }

    private void LocalStartInvisible()
    {
        m_targetSpriteRenderer.color = m_localTargetColor;
        m_targetUI.alpha = 0.25f;
        m_shadowCaster.enabled = false;
    }
    private void OnDestroy()
    {
        if (!isClient)
        {
            return;
        }
        EndInvisible();
        m_targetPlayerCombat.Casting_Started -= Casting_Started;
    }
}
