using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Spell
{
    [SerializeField] private StatusEffectObject m_invulnerableStatusObject;

    [Command]
    protected override void OnFinishedCasting()
    {
        ownerCollider.GetComponent<PlayerEntity>().SC_AddStatusEffect(m_invulnerableStatusObject.GetStatusEffectStruct());
    }
}
