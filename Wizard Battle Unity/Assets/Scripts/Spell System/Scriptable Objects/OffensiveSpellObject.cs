using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffensiveSpellObject : SpellObject
{
    public float DamageAmount = 0f;
    public DamageType DamageType = DamageType.Instant;
    public OffensiveSpellBehaviour SpellBehaviour = OffensiveSpellBehaviour.Skillshot;

    public OffensiveSpellObject()
    {
        SpellType = SpellType.Offensive;
    }
}
