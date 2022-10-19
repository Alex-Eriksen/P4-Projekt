using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Spell : NetworkBehaviour
{
    public SpellObject spellData = null;
    public Collider2D ownerCollider;
    private Coroutine m_destructionCoroutine;

    [ServerCallback]
    private void Start()
    {
        m_destructionCoroutine = StartCoroutine(SCDestroySpellObject(gameObject, spellData.LifeTime));
    }

    [ServerCallback]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isServer)
        {
            return;
        }

        if (collision.Equals(ownerCollider))
        {
            return;
        }

        if (collision.TryGetComponent<PlayerEntity>(out var opponentEntity))
        {
            opponentEntity.SCDrainHealth(spellData.DamageAmount);
        }

        StopCoroutine(m_destructionCoroutine);
        NetworkServer.Destroy(gameObject);
    }

    [ServerCallback]
    private IEnumerator SCDestroySpellObject(GameObject obj, float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        NetworkServer.Destroy(obj);
    }
}
