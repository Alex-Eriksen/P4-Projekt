using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Spell
{
    [SerializeField] private StatusEffectObject m_invulnerableStatusObject;
    [SerializeField] private float m_dashDistance = 3f;
    private PlayerMovement m_ownerPlayerMovement;
    private Transform m_transform;

    protected override void OnAwake()
    {
        m_transform = transform;
    }

    protected override void OnSetup()
    {
        m_ownerPlayerMovement = ownerCollider.GetComponent<PlayerMovement>();
        m_transform.SetParent(initialTargetTransform, false);
        vfx.SetFloat("Distance", m_dashDistance);
    }

    protected override void OnFinishedCasting()
    {
        if (!isServer)
        {
            vfx.SendEvent("OnHit");
            return;
        }

        m_transform.SetPositionAndRotation(initialTargetTransform.position, initialTargetTransform.rotation);
        m_ownerPlayerMovement.SC_AddForceAtPosition(10f * m_dashDistance * m_transform.up, m_transform.position, ForceMode2D.Force);
        ownerCollider.GetComponent<PlayerEntity>().SC_AddStatusEffect(m_invulnerableStatusObject.GetStatusEffectStruct());
    }
}
