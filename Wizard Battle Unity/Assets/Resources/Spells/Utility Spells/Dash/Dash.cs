using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Spell
{
    [SerializeField] private StatusEffectObject m_invulnerableStatusObject;
    [SerializeField] private float m_dashSpeed = 50f, m_dashDuration = 0.1f;
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
        vfx.SetFloat("Distance", m_dashSpeed);

        if (isServer)
        {
            return;
        }

        m_ownerPlayerMovement.InterruptMovementCallback += OwnerPlayerMovement_InterruptMovementCallback;
    }

    private void OwnerPlayerMovement_InterruptMovementCallback()
    {
        vfx.SendEvent("OnHit");
    }

    protected override void OnFinishedCasting()
    {
        if (!isServer)
        {
            return;
        }

        ownerCollider.GetComponent<PlayerEntity>().SC_AddStatusEffect(m_invulnerableStatusObject.GetStatusEffectStruct());
        m_transform.SetPositionAndRotation(initialTargetTransform.position, initialTargetTransform.rotation);
        m_ownerPlayerMovement.SC_OverrideCurrentSavedVelocity(m_transform.up * m_dashSpeed, m_dashDuration);
    }

    private void OnDestroy()
    {
        if (isServer)
        {
            return;
        }

        m_ownerPlayerMovement.InterruptMovementCallback -= OwnerPlayerMovement_InterruptMovementCallback;
    }
}
