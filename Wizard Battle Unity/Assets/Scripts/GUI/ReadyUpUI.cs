using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ReadyUpUI : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI m_readyTextUI;
    [SerializeField] private TextMeshProUGUI m_statusTextUI;
    [SerializeField] private Image m_readyBackgroundUI;
    [SerializeField] private GameObject m_readyButton;

    [SyncVar(hook = nameof(OnPlayerStatusChanged))]
    private bool m_ready = false;

    private void Start()
    {
        if (hasAuthority)
        {
            return;
        }

        m_readyButton.SetActive(false);
    }

    private void OnPlayerStatusChanged(bool oldValue, bool newValue)
    {
        if(newValue == false)
        {
            m_statusTextUI.text = "NOT READY";
            m_statusTextUI.color = Color.red;
            m_readyTextUI.text = "Ready";
            m_readyBackgroundUI.color = Color.green;
        }
        else
        {
            m_statusTextUI.text = "READY";
            m_statusTextUI.color = Color.green;
            m_readyTextUI.text = "Unready";
            m_readyBackgroundUI.color = Color.red;
        }
    }

    [Command]
    public void ToggleReady()
    {
        m_ready = !m_ready;
    }
}
