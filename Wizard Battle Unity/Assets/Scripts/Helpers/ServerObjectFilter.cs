using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerObjectFilter : MonoBehaviour
{
    [SerializeField] private List<Object> m_objectsToKill = new List<Object>();

    private void Awake()
    {
        if(SystemInfo.graphicsDeviceType != UnityEngine.Rendering.GraphicsDeviceType.Null)
        {
            Debug.Log("Canceling Object Killer - This is a Client.");
            return;
        }

        Debug.Log("Killing Objects - This is a Server.");
        foreach (Object obj in m_objectsToKill)
        {
            Destroy(obj);
        }
    }
}
