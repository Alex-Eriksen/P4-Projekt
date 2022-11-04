using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => m_instance;
    private static GameManager m_instance;
    private string m_endpoint = @"https://gorest.co.in/public/v2/users";

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

    public void HTTPRequest()
    {
        StartCoroutine(Get());
    }

    public IEnumerator Get()
    {
        UnityWebRequest request = UnityWebRequest.Get(m_endpoint);
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
        }
    }
}
