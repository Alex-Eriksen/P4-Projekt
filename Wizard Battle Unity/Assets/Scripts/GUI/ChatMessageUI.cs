using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatMessageUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_textObj;

    public void Setup(string playerName, string messageText)
    {
        m_textObj.text = $"{playerName}: {messageText}";
    }
}
