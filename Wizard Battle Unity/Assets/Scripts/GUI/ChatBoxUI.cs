using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class ChatBoxUI : NetworkBehaviour
{
    [SerializeField] private Transform m_contentTransform;
    [SerializeField] private GameObject m_messagePrefab;
    [SerializeField] private TextMeshProUGUI m_messageText;

    public void SendMessage()
    {
        CmdSendMessage(connectionToServer.address, m_messageText.text);
        m_messageText.text = "";
    }

    [Command(requiresAuthority = false)]
    public void CmdSendMessage(string playerName, string messageText)
    {
        RpcRecieveMessage(playerName, messageText);
    }

    [ClientRpc]
    public void RpcRecieveMessage(string playerName, string messageText)
    {
        ChatMessageUI obj = Instantiate(m_messagePrefab, m_contentTransform).GetComponent<ChatMessageUI>();
        obj.Setup(playerName, messageText);
    }
}