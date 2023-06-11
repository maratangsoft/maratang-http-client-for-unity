using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
public class MaratangHttpClient
{
    private MonoBehaviour monobehaviour;

    public MaratangHttpClient(MonoBehaviour monobehaviour)
    {
        this.monobehaviour = monobehaviour;
    }

    /*private static MaratangHttpClient _instance = null;

    public static MaratangHttpClient GetInstance(MonoBehaviour monoBehaviour)
    {
        if (_instance == null) _instance = new MaratangHttpClient(monoBehaviour);
        return _instance;
    }*/

    public void GetString(string url,
                          Action<string> onSuccess,
                          Action<string> onFailure,
                          Action<string> onError)
    {
        monobehaviour.StartCoroutine(
            RequestGetString(url, onSuccess, onFailure, onError)
        );
    }

    IEnumerator RequestGetString(string url,
                                 Action<string> onSuccess,
                                 Action<string> onFailure,
                                 Action<string> onError)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            onSuccess.Invoke(request.downloadHandler.text);
        }
        else if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            onFailure.Invoke(request.error);
        }
        else
        {
            onError.Invoke(request.error);
        }
    }

    public void GetJsonObject<T>(string url, 
                                 Action<T> onSuccess,
                                 Action<string> onFailure,
                                 Action<string> onError)
    {
        monobehaviour.StartCoroutine(
            RequestGetJson(url, onSuccess, onFailure, onError)
        );
    }

    IEnumerator RequestGetJson<T>(string url, 
                              Action<T> onSuccess,
                              Action<string> onFailure,
                              Action<string> onError)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            try
            {
                T jsonObject = JsonUtility.FromJson<T>(request.downloadHandler.text);
                onSuccess.Invoke(jsonObject);
            }
            catch(Exception e)
            {
                onError.Invoke(e.Message);
            }
        }
        else if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            onFailure.Invoke(request.error);
        }
        else
        {
            onError.Invoke(request.error);
        }
    }

    public void GetTexture(string url,
                           Action<Texture2D> onSuccess,
                           Action<string> onFailure,
                           Action<string> onError)
    {
        monobehaviour.StartCoroutine(
            RequestTexture(url, onSuccess, onFailure, onError)
        );
    }

    IEnumerator RequestTexture(string url,
                               Action<Texture2D> onSuccess,
                               Action<string> onFailure,
                               Action<string> onError)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture =
                ((DownloadHandlerTexture)request.downloadHandler).texture;
            onSuccess.Invoke(texture);
        }
        else if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            onFailure.Invoke(request.error);
        }
        else
        {
            onError.Invoke(request.error);
        }
    }
}
