using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : Spell
{
    [SerializeField] private StatusEffectObject m_invisibilityStatus;

    protected override void OnSetup()
    {
        transform.SetParent(initialTargetTransform, false);
    }

    protected override void OnFinishedCasting()
    {
        if (!isServer)
        {
            vfx.SendEvent("OnInvisibilityActive");
            return;
        }
        ownerCollider.GetComponent<PlayerEntity>().SC_AddStatusEffect(m_invisibilityStatus.GetStatusEffectStruct());
    }
}
