using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Mirror;

public class PickUp : NetworkBehaviour, IInteractable
{
    public bool IsInteractable { get { return m_currentSpawnTime <= 0 && m_isPickedUp == false; } }

    [SerializeField] private PickUpType m_pickUpType;
    [SerializeField] private float m_amount;
    [SerializeField] private float m_spawnTime;
    [SerializeField] private CanvasGroup m_canvasGroup;

    [SyncVar(hook = nameof(SpawnTimeChanged))] private float m_currentSpawnTime;
    [SyncVar(hook = nameof(PickUpChanged))] private bool m_isPickedUp = false;
    private float NormalizedSpawnTime { get { return m_currentSpawnTime / m_spawnTime; } }

    private VisualEffect m_vfx;

    private void Awake()
    {
        m_vfx = GetComponent<VisualEffect>();
    }

    public override void OnStartServer()
    {
        m_currentSpawnTime = m_spawnTime;
    }

    public override void OnStartClient()
    {
        SpawnTimeChanged(0, m_currentSpawnTime);
        PickUpChanged(false, m_isPickedUp);
    }

    private void Update()
    {
        if (!isServer)
        {
            return;
        }
        if(m_currentSpawnTime > 0)
        {
            if (!isClient)
            {
                m_currentSpawnTime -= Time.deltaTime;
                return;
            }
        }
    }

    private void PickUpChanged(bool oldValue, bool newValue)
    {
        if (newValue)
        {
            m_canvasGroup.alpha = 0f;
        }
    }

    private void SpawnTimeChanged(float oldValue, float newValue)
    {
        if (newValue <= 0)
        {
            m_vfx.SendEvent("OnSpawn");
        }
        m_vfx.SetFloat("Size", NormalizedSpawnTime);
    }

    [ClientRpc]
    private void Rpc_Interact(Entity entity)
    {
        m_vfx.SetVector3("EndPos", entity.transform.position);
        m_vfx.SendEvent("OnPickUp");
    }

    public void Interact(Entity entity)
    {
        if (!isServer)
        {
            return;
        }
        Rpc_Interact(entity);
        switch (m_pickUpType)
        {
            case PickUpType.Health:
                entity.SC_GainHealth(m_amount);
                break;

            case PickUpType.Mana:
                entity.SC_GainMana(m_amount);
                break;

            case PickUpType.UltCharge:
                Debug.Log("UltCharge Not Implmented");
                break;

            default:
                Debug.LogError($"For whatever reason {name} did not have a pick up type.");
                break;
        }

        m_isPickedUp = true;
        StartCoroutine(SC_DelayedDestroy());
    }

    [ServerCallback]
    private IEnumerator SC_DelayedDestroy()
    {
        yield return new WaitForSeconds(1.5f);
        NetworkServer.Destroy(gameObject);
    }

    public void EnterRange()
    {
        m_canvasGroup.alpha = 1f;
    }
}
