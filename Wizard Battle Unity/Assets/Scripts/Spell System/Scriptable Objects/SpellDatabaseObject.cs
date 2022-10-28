using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spell System/New Spell Database")]
public class SpellDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public SpellObject[] spellObjects;

    public void OnAfterDeserialize()
    {
        for(int i = 0; i < spellObjects.Length; i++)
        {
            spellObjects[i].SpellID = i;
        }
    }

    public void OnBeforeSerialize()
    {
        
    }
}
