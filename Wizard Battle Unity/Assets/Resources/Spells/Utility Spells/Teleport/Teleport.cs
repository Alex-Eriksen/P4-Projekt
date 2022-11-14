using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Spell
{
    private Transform m_transform, m_targetTransform;
    private Vector3 m_endPosition;
    private PlayerMovement m_playerMovement;

    protected override void OnAwake()
    {
        m_transform = transform;
    }

    protected override void OnSetup()
    {
        m_playerMovement = ownerCollider.GetComponent<PlayerMovement>();
        m_transform.SetParent(initialTargetTransform, false);
        m_targetTransform = initialTargetTransform.Find("Graphics").Find("TargetPoint");
        m_endPosition = m_targetTransform.position;

        if (!isClient)
        {
            return;
        }

        vfx.SetVector3("EndPos", m_endPosition);
    }

    protected override void OnFinishedCasting()
    {
        if (isClient)
        {
            vfx.SetVector3("StartPos", m_transform.position);
            vfx.SendEvent("OnHit");
            return;
        }

        m_playerMovement.SC_OverrideCurrentSavedPosition(m_endPosition, "Used Teleport.", spellData.LifeTime);
    }
}
