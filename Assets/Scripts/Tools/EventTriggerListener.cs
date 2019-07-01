//===================
//描述：
//作者：lijianbao
//创建时间：2019-06-19 10:42:59
//版本：V1.0
//==================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class EventTriggerListener : UnityEngine.EventSystems.EventTrigger {

	public delegate void VoidDelegate (GameObject go);
	public event VoidDelegate onClick;
	public event VoidDelegate onDown;
	public event VoidDelegate onEnter;
	public event VoidDelegate onExit;
	public event VoidDelegate onUp;
	public event VoidDelegate onSelect;
	public event VoidDelegate onUpdateSelect;

	static public EventTriggerListener Get(GameObject go)
	{
		EventTriggerListener listener = go.GetComponent<EventTriggerListener> ();
		if(listener == null) listener = go.AddComponent<EventTriggerListener> ();
        Debug.Log("单例eventTriggerListener: "+listener.ToString());
		return listener;
	}

	public override void OnPointerClick(PointerEventData eventData)
	{
        Debug.Log("ON POINTER CLICK");
		if (onClick != null)
			onClick(gameObject);
	}

	public override void OnPointerDown(PointerEventData eventData)
	{
        Debug.Log("ON POINTER DOWN");
        if (onDown != null)
			onDown(gameObject);
	}

	public override void OnPointerEnter(PointerEventData eventData)
	{
        Debug.Log("ON POINTER ENTER");
        if (onEnter != null)
			onEnter(gameObject);
	}

	public override void OnPointerExit(PointerEventData eventData)
	{

        Debug.Log("ON POINTER EXIT");
        if (onExit != null)
			onExit(gameObject);
	}
		
	public override void OnPointerUp(PointerEventData eventData)
	{
        Debug.Log("ON POINTER UP");
        if (onUp != null)
			onUp(gameObject);
	}

	public override void OnSelect(BaseEventData eventData)
	{
        Debug.Log("ON SELECT");
        if (onSelect != null)
			onSelect(gameObject);
	}

	public override void OnUpdateSelected(BaseEventData eventData)
	{
        Debug.Log("ON POINTER UpdateSelected");
        if (onUpdateSelect != null)
			onUpdateSelect(gameObject);
	}

}
