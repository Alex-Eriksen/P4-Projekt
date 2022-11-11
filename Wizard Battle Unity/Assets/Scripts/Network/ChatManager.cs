using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ChatManager : NetworkBehaviour
{
    public static ChatManager Instance
    { 
        get
        {
            return m_instance;
        }
    }
    private static ChatManager m_instance;

    [SerializeField] private GameObject m_messagePrefab;
    [SerializeField] private Transform m_contentTransform;

    private void Awake()
    {
        if(m_instance != null && m_instance != this)
        {
            Destroy(this);
        }
        else
        {
            m_instance = this;
        }
    }

    [Command(requiresAuthority = false)]
    public void Cmd_SendMessage(string playerName, string messageText)
    {
        Rpc_RecieveMessage(playerName, messageText);
    }

    [ClientRpc]
    public void Rpc_RecieveMessage(string playerName, string messageText)
    {
        ChatMessageUI obj = Instantiate(m_messagePrefab, m_contentTransform).GetComponent<ChatMessageUI>();
        obj.Setup(playerName, messageText);
    }
}
