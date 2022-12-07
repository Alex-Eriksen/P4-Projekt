using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => m_instance;
    private static GameManager m_instance;

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
        var objects = GameObject.FindGameObjectsWithTag("Scene FOW Light");
        for (int i = 0; i < objects.Length; i++)
        {
            Destroy(objects[i]);
        }
    }

    #region From Github -> https://gist.github.com/wherrera/0eda457277810c3b5368c01a7d2852d0
    public class JsonWebResponse
    {
        public long responseCode;
    }

    public void Get<T>(string url, KeyValuePair<string, string>[] query, UnityAction<T> result) where T : JsonWebResponse, new()
    {
        StartCoroutine(GetAsync<T>(url, query, result));
    }

    public IEnumerator GetAsync<T>(string endPoint, KeyValuePair<string, string>[] query, UnityAction<T> result) where T : JsonWebResponse, new()
    {
        string queryString = string.Empty;

        foreach (KeyValuePair<string, string> keyValuePair in query)
        {
            if (queryString.Length > 0)
                queryString += "&";

            queryString += keyValuePair.Key + "=" + UnityWebRequest.EscapeURL(keyValuePair.Value);
        }

        UnityWebRequest www = UnityWebRequest.Get(endPoint + "?" + queryString);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogError(www.downloadHandler.text + "-" + www.error);

            if (result != null)
            {
                result.Invoke(new T()
                {
                    responseCode = www.responseCode
                });
            }
        }
        else
        {
            T resultObject = JsonUtility.FromJson<T>(www.downloadHandler.text);

            resultObject.responseCode = www.responseCode;

            if (result != null)
                result.Invoke(resultObject);
        }
    }

    public IEnumerator Put<T>(string endPoint, string postData, UnityAction<T> result) where T : JsonWebResponse, new()
    {
        string queryString = string.Empty;

        byte[] bytes = Encoding.UTF8.GetBytes(postData);

        UnityWebRequest www = UnityWebRequest.Put(endPoint, bytes);

        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogError(www.downloadHandler.text + "-" + www.error);

            if (result != null)
                result.Invoke(new T()
                {
                    responseCode = www.responseCode
                });
        }
        else
        {
            T resultObject = JsonUtility.FromJson<T>(www.downloadHandler.text);

            resultObject.responseCode = www.responseCode;

            if (result != null)
                result.Invoke(resultObject);
        }
    }

    public IEnumerator Get(string endPoint, KeyValuePair<string, string>[] query, UnityAction<UnityWebRequest> result)
    {
        string queryString = string.Empty;

        foreach (KeyValuePair<string, string> keyValuePair in query)
        {
            if (queryString.Length > 0)
                queryString += "&";

            queryString += keyValuePair.Key + "=" + UnityWebRequest.EscapeURL(keyValuePair.Value);
        }

        UnityWebRequest www = UnityWebRequest.Get(endPoint + "?" + queryString);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogError(www.downloadHandler.text + "-" + www.error);

        }

        if (result != null)
            result.Invoke(www);
    }

    public IEnumerator PostJson(string endPoint, string postData, UnityAction<UnityWebRequest> result)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(postData);

        UploadHandlerRaw uploadHandler = new UploadHandlerRaw(bytes);

        UnityWebRequest www = UnityWebRequest.Post(endPoint, postData);

        www.uploadHandler = uploadHandler;

        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogError(www.downloadHandler.text + "-" + www.error);
        }

        if (result != null)
            result.Invoke(www);
    }
    #endregion
}
