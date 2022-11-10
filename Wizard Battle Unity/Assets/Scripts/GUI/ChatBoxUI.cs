using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ChatBoxUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField m_messageText;
    [SerializeField] private TextMeshProUGUI m_displayText;
    [SerializeField] private CanvasGroup m_canvasGroup;
    [SerializeField] private Image m_showImage, m_hideImage;
    [SerializeField] private Toggle m_showToggle;

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
        }
    }

    public void SendMessage(InputAction.CallbackContext ctx)
    {
        if(!m_canvasGroup.interactable)
        {
            return;
        }

        if (ctx.action.name == "EnterChat")
        {
            OnStartEditing();
            return;
        }

        if (string.IsNullOrEmpty(m_messageText.text)) 
        {
            OnStopEditing();
            return;
        }

        ChatManager.Instance.Cmd_SendMessage(PlayerConnection.PlayerName, m_messageText.text);
        m_messageText.text = "";
        OnStopEditing();
    }

    public void OnStartEditing()
    {
        m_messageText.ActivateInputField();
        PlayerConnection.PlayerInput.actions["EnterChat"].started -= SendMessage;
        PlayerConnection.PlayerInput.SwitchCurrentActionMap("UI");
        PlayerConnection.PlayerInput.actions["ExitChat"].started += SendMessage;
    }

    public void OnStopEditing()
    {
        m_messageText.DeactivateInputField();
        PlayerConnection.PlayerInput.actions["ExitChat"].started -= SendMessage;
        PlayerConnection.PlayerInput.SwitchCurrentActionMap("Gameplay");
        PlayerConnection.PlayerInput.actions["EnterChat"].started += SendMessage;
    }

    public void ToggleChatDisplay(bool value)
    {
        if (!value)
        {
            m_canvasGroup.alpha = 1f;
            m_canvasGroup.interactable = true;
            m_canvasGroup.blocksRaycasts = true;
            m_displayText.text = "Hide Chat";
            m_showToggle.graphic = m_hideImage;
            m_hideImage.gameObject.SetActive(true);
            m_showImage.gameObject.SetActive(false);
        }
        else
        {
            m_canvasGroup.alpha = 0f;
            m_canvasGroup.interactable = false;
            m_canvasGroup.blocksRaycasts = false;
            m_displayText.text = "Show Chat";
            m_showToggle.graphic = m_showImage;
            m_hideImage.gameObject.SetActive(false);
            m_showImage.gameObject.SetActive(true);
        }
    }
}