using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Mirror;

public class PickUp : NetworkBehaviour, IInteractable
{
    public bool IsInteractable { get { return currentSpawnTime <= 0 && isPickedUp == false; } }

    [SerializeField] protected float amount;
    [SerializeField] protected float spawnTime;
    [SerializeField] protected CanvasGroup canvasGroup;

    [SyncVar(hook = nameof(SpawnTimeChanged))] protected float currentSpawnTime;
    [SyncVar(hook = nameof(PickUpChanged))] protected bool isPickedUp = false;
    protected float NormalizedSpawnTime { get { return currentSpawnTime / spawnTime; } }

    private void Awake()
    {
        OnAwake();
    }
    
    private void Update()
    {
        if (!isServer)
        {
            return;
        }
        if (currentSpawnTime > 0)
        {
            if (!isClient)
            {
                currentSpawnTime -= Time.deltaTime;
                return;
            }
        }
        OnUpdate();
    }

    public override void OnStartServer()
    {
        currentSpawnTime = spawnTime;
        OnServerStart();
    }

    public override void OnStartClient()
    {
        SpawnTimeChanged(0, currentSpawnTime);
        PickUpChanged(false, isPickedUp);
        OnClientStart();
    }

    private void PickUpChanged(bool oldValue, bool newValue)
    {
        OnPickUpChanged(oldValue, newValue);
    }

    private void SpawnTimeChanged(float oldValue, float newValue)
    {
        OnSpawnTimeChanged(oldValue, newValue);
    }

    [ClientRpc]
    private void Rpc_ClientInteract(Entity entity)
    {
        OnClientInteract(entity);
    }

    public void Interact(Entity entity)
    {
        isPickedUp = true;

        OnServerInteract(entity);
        Rpc_ClientInteract(entity);

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
        canvasGroup.alpha = 1f;
    }

    public void ExitRange()
    {
        canvasGroup.alpha = 0f;
    }

    protected virtual void OnAwake() { }
    protected virtual void OnUpdate() { }
    [ClientCallback]
    protected virtual void OnClientStart() { }
    [ServerCallback]
    protected virtual void OnServerStart() { }
    protected virtual void OnServerInteract(Entity entity) { }
    protected virtual void OnClientInteract(Entity entity) { }
    protected virtual void OnSpawnTimeChanged(float oldValue, float newValue) { }
    protected virtual void OnPickUpChanged(bool oldValue, bool newValue) { }
}
