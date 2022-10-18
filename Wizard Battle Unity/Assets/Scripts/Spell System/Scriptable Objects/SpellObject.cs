using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellObject : ScriptableObject
{
    public int SpellID = 0;
    public string SpellName = "";
    [TextArea(5, 10)]
    public string Description = "";
    public Sprite SpellIcon = null;
    public float ManaCost = 0f;
    public float LifeTime = 0f;
    public float DamageAmount = 0f;
    public float CastTime = 0f;
    public string PrefabPath = "Spells/";
    public SpellTier SpellTier = SpellTier.Tier_I;
}
