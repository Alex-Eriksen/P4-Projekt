using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class OrbPickUp : PickUp
{
    [SerializeField] private OrbType m_orbType;
    private VisualEffect m_vfx;

    protected override void OnAwake()
    {
        m_vfx = GetComponent<VisualEffect>();
    }

    protected override void OnPickUpChanged(bool oldValue, bool newValue)
    {
        if (newValue)
        {
            canvasGroup.alpha = 0f;
        }
    }

    protected override void OnSpawnTimeChanged(float oldValue, float newValue)
    {
        if (newValue <= 0)
        {
            m_vfx.SendEvent("OnSpawn");
        }
        m_vfx.SetFloat("Size", NormalizedSpawnTime);
    }

    protected override void OnClientInteract(Entity entity)
    {
        m_vfx.SetVector3("EndPos", entity.transform.position);
        m_vfx.SendEvent("OnPickUp");
    }

    protected override void OnServerInteract(Entity entity)
    {

        switch (m_orbType)
        {
            case OrbType.Health:
                entity.SC_GainHealth(amount);
                break;

            case OrbType.Mana:
                entity.SC_GainMana(amount);
                break;

            case OrbType.UltCharge:
                Debug.Log("UltCharge Not Implmented");
                break;

            default:
                Debug.LogError($"For whatever reason {name} did not have a pick up type.");
                break;
        }
    }
}
