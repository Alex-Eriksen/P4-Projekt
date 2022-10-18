using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Spell : NetworkBehaviour
{
    public SpellObject spellData = null;
    public Collider2D ownerCollider;

    private void Start()
    {
        StartCoroutine(DestroyDelay());
    }

    private IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(spellData.LifeTime);
        DestroySelf(gameObject);
    }

    [Command]
    private void DestroySelf(GameObject obj)
    {
        NetworkServer.Destroy(obj);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.Equals(ownerCollider))
        {
            return;
        }

        if (collision.TryGetComponent<PlayerEntity>(out var opponentEntity))
        {
            opponentEntity.CmdDrainHealth(spellData.DamageAmount);
        }

        DestroySelf(gameObject);
    }
}
