using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static ActionNotificationHandler;

public class NotificationObject : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_notificationTextUI;
    public Notification notificationData;

    private void Start()
    {
        m_notificationTextUI.text = $"{notificationData.Message} - {Helpers.AddSpacesToSentence(notificationData.Flag.ToString())}";
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
