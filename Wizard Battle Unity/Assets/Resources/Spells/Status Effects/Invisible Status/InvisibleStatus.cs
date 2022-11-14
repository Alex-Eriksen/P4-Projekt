using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleStatus : Status
{
    private SpriteRenderer m_targetSpriteRenderer;
    private CanvasGroup m_targetUI;
    private PlayerCombat m_targetPlayerCombat;
    private Color m_originalColor;
    [SerializeField] private Color m_targetColor, m_localTargetColor;

    protected override void OnSetup()
    {
        m_targetSpriteRenderer = target.transform.Find("Graphics").GetComponent<SpriteRenderer>();
        m_targetUI = target.GetComponentInChildren<CanvasGroup>();
        m_targetPlayerCombat = target.GetComponent<PlayerCombat>();
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
    }
    private void EndInvisible()
    {
        m_targetSpriteRenderer.color = m_originalColor;
        m_targetUI.alpha = 1f;
    }

    private void LocalStartInvisible()
    {
        m_targetSpriteRenderer.color = m_localTargetColor;
        m_targetUI.alpha = 0.25f;
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
