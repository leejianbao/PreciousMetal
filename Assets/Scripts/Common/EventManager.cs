using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 事件管理器
/// </summary>
public class EventManager:Singleton<EventManager>
{
    public delegate void OnActionHandler(object arg);
    public Dictionary<ushort, List<OnActionHandler>> eventDic = new Dictionary<ushort, List<OnActionHandler>>();
    
    /// <summary>
    /// 添加监听
    /// </summary>
    /// <param name="protoCode"></param>
    /// <param name="handler"></param>
    public void AddEventListener(ushort protoCode,OnActionHandler handler)
    {
        if(eventDic.ContainsKey(protoCode))
        {
            eventDic[protoCode].Add(handler);
        }
        else
        {
            List<OnActionHandler> listHandler = new List<OnActionHandler>();
            listHandler.Add(handler);
            eventDic[protoCode] = listHandler;
        }
    }

    /// <summary>
    /// 移除监听
    /// </summary>
    /// <param name="protoCode"></param>
    /// <param name="handler"></param>
    public void RemoveEventListener(ushort protoCode,OnActionHandler handler)
    {
        if(eventDic.ContainsKey(protoCode))
        {
            eventDic[protoCode].Remove(handler);
            if(eventDic[protoCode].Count==0)
            {
                eventDic.Remove(protoCode);
            }
        }
    }

    /// <summary>
    /// 触发事件
    /// </summary>
    /// <param name="protoCode"></param>
    /// <param name="arg"></param>
    public void DispatchEvent(ushort protoCode,object arg)
    {
        if(eventDic.ContainsKey(protoCode))
        {
            foreach (OnActionHandler item in eventDic[protoCode])
            {
                if(item!=null)
                {
                    item(arg);
                }
            }
        }
    }
}
