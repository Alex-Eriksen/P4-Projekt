using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveSpellObject : SpellObject
{
    // The amount of damage this spell is able to handle, deflect, absorb or block.
    public float DefensiveAmount = 0f;
    public DefensiveSpellBehaviour SpellBehaviour = DefensiveSpellBehaviour.Block;

    public DefensiveSpellObject()
    {
        SpellType = SpellType.Defensive;
    }
}
