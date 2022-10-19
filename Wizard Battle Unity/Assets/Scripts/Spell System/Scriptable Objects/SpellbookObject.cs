using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spell System/New Spellbook")]
public class SpellbookObject : ScriptableObject
{
    public string spellbookName = "Unnamed Spellbook";
    public SpellSlot[] spellSlots = new SpellSlot[8];
}
