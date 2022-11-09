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
        vfx.SetVector3("EndPos", m_endPosition);
    }

    protected override void OnUpdate()
    {
    }

    protected override void OnFinishedCasting()
    {
        vfx.SetVector3("StartPos", m_transform.position);
        vfx.SendEvent("OnHit");
        Cmd_MovePlayer(m_endPosition);
    }

    [Command]
    private void Cmd_MovePlayer(Vector3 newPosition)
    {
        m_playerMovement.SC_OverrideCurrentSavedPosition(newPosition, "Used Teleport.");
        ownerCollider.transform.position = newPosition;
    }
}
