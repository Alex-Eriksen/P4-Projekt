using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlashEffect : MonoBehaviour
{
    public float flashDuration = 0.1f;
    [SerializeField] private Material m_flashMaterial;
    private Material m_originalMaterial;
    private Color m_originalColor;
    private PlayerEntity m_playerEntity;
    private SpriteRenderer m_graphics;
    private Coroutine m_flashRoutine;

    private void Awake()
    {
        m_playerEntity = GetComponent<PlayerEntity>();
        m_graphics = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        m_playerEntity.OnHealthDrained += PlayerEntity_OnDrainedHealth;
        m_originalMaterial = m_graphics.material;
        m_originalColor = m_graphics.color;
    }

    private void PlayerEntity_OnDrainedHealth(object sender, EventArgs e)
    {
        if(m_flashRoutine != null)
        {
            StopCoroutine(m_flashRoutine);
        }
        m_flashRoutine = StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        m_graphics.material = m_flashMaterial;
        m_graphics.color = Color.white;
        yield return new WaitForSeconds(flashDuration);
        m_graphics.material = m_originalMaterial;
        m_graphics.color = m_originalColor;
    }
}
