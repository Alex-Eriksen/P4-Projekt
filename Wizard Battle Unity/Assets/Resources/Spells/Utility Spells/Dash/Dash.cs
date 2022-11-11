using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Spell
{
    [SerializeField] private StatusEffectObject m_invulnerableStatusObject;
    [SerializeField] private float m_dashDistance = 3f, m_dashTime = 2f;
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

        if (isServer)
        {
            return;
        }

        m_ownerPlayerMovement.RecursiveMoveCallback += OwnerPlayerMovement_RecursiveMoveCallback;
    }

    private void OwnerPlayerMovement_RecursiveMoveCallback(object sender, System.EventArgs e)
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
        m_ownerPlayerMovement.Rpc_MovePlayer(10f * m_dashDistance, m_dashTime, initialTargetTransform.up);
    }

    private void OnDestroy()
    {
        if (isServer)
        {
            return;
        }

        m_ownerPlayerMovement.RecursiveMoveCallback -= OwnerPlayerMovement_RecursiveMoveCallback;
    }
}
