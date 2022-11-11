using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Spell
{
    [SerializeField] private StatusEffectObject m_invulnerableStatusObject;

    protected override void OnFinishedCasting()
    {
        if (!isServer)
        {
            return;
        }

        ownerCollider.GetComponent<PlayerEntity>().SC_AddStatusEffect(m_invulnerableStatusObject.GetStatusEffectStruct());
    }
}
