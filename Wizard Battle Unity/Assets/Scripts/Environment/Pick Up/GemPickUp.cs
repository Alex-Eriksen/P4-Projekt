using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPickUp : PickUp
{
    [SerializeField] private GemType m_gemType;

    protected override void OnServerInteract(Entity entity)
    {
        Debug.Log($"{entity.entityName} - Collected {amount} {m_gemType} Gem.");
    }
}
