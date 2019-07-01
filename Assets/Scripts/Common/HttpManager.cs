using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using LitJson;

/// <summary>
/// Http协议管理器
/// </summary>
public class HttpManager : SingletonMono<HttpManager>
{
    private string m_WebUrlBase;            //WebUrl的前面公共部分

    public string WebUrlBase
    {
        set
        {
            m_WebUrlBase = value;
        }
    }

    /// <summary>
    /// web数据请求方法
    /// </summary>
    /// <param name="urlPostfix">url后缀部分</param>
    /// <param name="parameter">WWWForm需要的键值对</param>
    /// <param name="protoCode">事件编码（得到数据后回调由事件触发）</param>
    public void GetWebResult(string urlPostfix,Hashtable parameter,ushort protoCode = 0)
    {
        StartCoroutine(GetPostInfo(m_WebUrlBase + urlPostfix, parameter, protoCode));
    }

    IEnumerator GetPostInfo(string url,Hashtable parameter,ushort protoCode)
    {
        WWWForm form = new WWWForm();
        if(parameter != null)
        {
            foreach (DictionaryEntry de in parameter)
            {
                form.AddField(de.Key.ToString(), de.Value.ToString());
            }
        }

        UnityWebRequest webRequest = UnityWebRequest.Post(url, form);
        yield return webRequest.SendWebRequest();

        if(webRequest.isDone)
        {
            if(webRequest.error ==null)
            {
                if(protoCode !=0)
                {
                    string str = webRequest.downloadHandler.text;
                    //byte[] buff = Encoding.UTF8.GetBytes(s);
                    JsonData data = LitJson.JsonMapper.ToObject(str);
                    EventManager.Instance.DispatchEvent(protoCode, data);
                }
                
            }
            else
            {
                Debug.LogError(webRequest.error);
            }
        }
    }
	
}
