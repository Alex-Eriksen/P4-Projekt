using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DestroyableObject : Entity
{
    [SerializeField] private Sprite[] m_spritePieces;
    [SerializeField] private Vector2 m_spreadForceMinMax;
    private SpriteRenderer m_spriteRenderer;
    private Collider2D m_collider;

    protected override void OnAwake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_collider = GetComponent<Collider2D>();
    }

    protected override void OnClientDeath()
    {
        SpawnPieces();
    }

    public void SpawnPieces()
    {
        m_collider.enabled = false;
        m_spriteRenderer.enabled = false;
        for (int i = 0; i < m_spritePieces.Length; i++)
        {
            GameObject pieceObject = new GameObject($"{name}-Piece-{i}");
            pieceObject.transform.position = m_transform.position;
            var pieceSpriteRenderer = pieceObject.AddComponent<SpriteRenderer>();
            pieceSpriteRenderer.sprite = m_spritePieces[i];
            pieceSpriteRenderer.sortingLayerName = "GroundVFX";
            var pieceRigidbody = pieceObject.AddComponent<Rigidbody2D>();
            pieceRigidbody.mass = 0.1f;
            pieceRigidbody.drag = 1f;
            pieceRigidbody.angularDrag = 1f;
            pieceRigidbody.gravityScale = 0f;
            pieceRigidbody.AddForceAtPosition(Random.insideUnitCircle * Random.Range(m_spreadForceMinMax.x, m_spreadForceMinMax.y), m_transform.position, ForceMode2D.Force);
            pieceRigidbody.angularVelocity = Random.Range(-1, 2) * 2;
            Destroy(pieceObject, 2.5f);
        }
        isReadyToDestroy = true;
    }
}
