using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.InputSystem;

public class ChatBoxUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField m_messageText;
    public PlayerConnection PlayerConnection { get; private set; }

    private void Start()
    {
        StartCoroutine(GetPlayerConnectionObjectBuffer());
    }

    private IEnumerator GetPlayerConnectionObjectBuffer()
    {
        try
        {
            PlayerConnection = FindObjectsOfType<PlayerConnection>().Where(x => x.isLocalPlayer == true).Single();
        }
        catch
        {
            PlayerConnection = null;
        }
        yield return new WaitForEndOfFrame();
        if(PlayerConnection == null)
        {
            StartCoroutine(GetPlayerConnectionObjectBuffer());
        }
        else
        {
            PlayerConnection.PlayerInput.actions["EnterChat"].started += SendMessage;
            PlayerConnection.PlayerInput.actions["ExitChat"].started += SendMessage;
        }
    }

    public void SendMessage(InputAction.CallbackContext ctx)
    {
        if (!m_messageText.isFocused)
        {
            OnStartEditing();
            return;
        }

        if (string.IsNullOrEmpty(m_messageText.text)) 
        {
            return;
        }
        ChatManager.Instance.CmdSendMessage(PlayerConnection.PlayerName, m_messageText.text);
        m_messageText.text = "";
        OnStopEditing();
    }

    public void OnStartEditing()
    {
        m_messageText.ActivateInputField();
        PlayerConnection.PlayerInput.SwitchCurrentActionMap("UI");
    }

    public void OnStopEditing()
    {
        m_messageText.DeactivateInputField();
        PlayerConnection.PlayerInput.SwitchCurrentActionMap("Gameplay");
    }
}