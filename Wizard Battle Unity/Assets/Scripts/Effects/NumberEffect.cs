using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberEffect : MonoBehaviour
{
    private TextMeshProUGUI m_numberText;
    public NumberEffectData data;

    private void Awake()
    {
        m_numberText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        m_numberText.color = data.numberColor;
        m_numberText.text = data.numberText;
        transform.position = data.position;
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}

public struct NumberEffectData
{
    public string numberText;
    public Color numberColor;
    public Vector2 position;
}