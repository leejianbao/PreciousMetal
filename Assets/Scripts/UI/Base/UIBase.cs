using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有UI的基类
/// </summary>
public class UIBase : MonoBehaviour {


    private void Awake()
    {
        OnAwake();
    }

    private void Start()
    {
        OnStart();
    }

    private void Update()
    {
        OnUpdate();
    }

    private void OnDestroy()
    {
        BeforeDestroy();
    }

    protected virtual void OnAwake() { }
    protected virtual void OnStart() { }
    protected virtual void OnUpdate() { }
    protected virtual void BeforeDestroy() { }
}
