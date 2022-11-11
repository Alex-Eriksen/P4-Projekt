using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Utility Spell", menuName = "Spell System/New Utility Spell")]
public class UtilitySpellObject : SpellObject
{
    public UtilitySpellBehaviour spellBehaviour = UtilitySpellBehaviour.Teleport;

    public UtilitySpellObject()
    {
        SpellType = SpellType.Utility;
    }
}
