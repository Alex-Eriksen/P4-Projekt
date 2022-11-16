using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spell System/Status Effects/New Status Effect")]
public class StatusEffectObject : ScriptableObject
{
    public StatusEffectType effectType;
    public float effectLifetime;
    public bool effectStackable;
    public int maxEffectStacks;
    public Sprite effectIcon;

    /// <summary>
    /// Gets the struct of the status effect.
    /// </summary>
    /// <returns><see cref="StatusEffect"/></returns>
    public StatusEffect GetStatusEffectStruct()
    {
        return new StatusEffect() { effectLifetime = effectLifetime, effectType = effectType };
    }
}
