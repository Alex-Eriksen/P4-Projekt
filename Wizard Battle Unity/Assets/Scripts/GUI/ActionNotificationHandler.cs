using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections.Generic;
using UnityEngine;

public class ActionNotificationHandler : MonoBehaviour
{
    public static ActionNotificationHandler Instance { get { return m_instance; } }
    private static ActionNotificationHandler m_instance;

    [SerializeField] private GameObject m_notificationPrefab;

    // A collection that raises events when changes have been made.
    private readonly ObservableCollection<Notification> m_activeNotifications = new();

    public class Notification
    {
        public GameObject InstanceObject;
        public string Message = "Empty";
        public ActionEventArgsFlag Flag = ActionEventArgsFlag.None;
    }

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

    private void Start()
    {
        m_activeNotifications.CollectionChanged += ActiveNotifications_CollectionChanged;
    }

    /// <summary>
    /// Method listening for changes on the m_activeNotifications list.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ActiveNotifications_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                GameObject notificationObject = Instantiate(m_notificationPrefab, transform);
                notificationObject.GetComponent<NotificationObject>().notificationData = e.NewItems[0] as Notification;
                m_activeNotifications[m_activeNotifications.Count - 1].InstanceObject = notificationObject;
                StartCoroutine(RemoveNotification(e.NewItems[0] as Notification));
                break;

            case NotifyCollectionChangedAction.Remove:
                ((Notification)e.OldItems[0]).InstanceObject.GetComponent<Animator>().SetTrigger("Exit");
                break;

            case NotifyCollectionChangedAction.Replace:
                break;

            case NotifyCollectionChangedAction.Reset:
                break;

            case NotifyCollectionChangedAction.Move:
                break;
        }
    }

    /// <summary>
    /// Adds a new notifications.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public void AddActionEventNotification(object sender, ActionEventArgs args)
    {
        m_activeNotifications.Add(new() { Flag = args.Flag, Message = args.Message });
    }

    /// <summary>
    /// Coroutine that deletes a notification every seconds.
    /// </summary>
    /// <param name="notification"></param>
    /// <returns></returns>
    private IEnumerator RemoveNotification(Notification notification)
    {
        yield return new WaitForSeconds(1f);
        m_activeNotifications.Remove(notification);
    }
}
