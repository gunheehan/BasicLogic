using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class ServerInfo {
    public string serverId;
    public string serverName;
}

[Serializable]
public class ServerListInfo {
    public ServerInfo[] rows;
}

public class NeopleApi : MonoBehaviour
{

    private const string neopleUrl = "https://api.neople.co.kr";
    private const string neopleApiKey = "6XzDcT1nJlutgtkoeZO8mtWfRV6xMM5r";

    private void Start()
    {
        RequestNeopleApi("/df/servers");
    }

    public void RequestNeopleApi(string requesturl)
    {
        string url = neopleUrl + requesturl;
        Hashtable newBody = new Hashtable();
        newBody.Add("apikey", neopleApiKey);

        StartCoroutine(APIPost(url, "Get",newBody));
    }
    
    
    IEnumerator APIPost(string url, string requestType, Hashtable body = null, Action<string> callBack = null)
    {
        Debug.Log("Call post");
        
        List<string> queryParams = new List<string>();
        foreach (DictionaryEntry entry in body)
        {
            string key = UnityWebRequest.EscapeURL((string)entry.Key);
            string value = UnityWebRequest.EscapeURL((string)entry.Value);
            queryParams.Add(key + "=" + value);
        }
        string queryString = string.Join("&", queryParams.ToArray());

        url = url + "?" + queryString;

        using (UnityWebRequest www = new UnityWebRequest(url, requestType))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                Debug.Log(www.url);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                callBack?.Invoke(www.downloadHandler.text);
                Debug.Log("post request complete!");
            }
        }
    }
}
