using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 继承MonoBehaviour的单例模板
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonMono<T> : MonoBehaviour where T:MonoBehaviour
{
    #region 单例
    private static T instance;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<T>();           //先查找，若没有，则动态创建物体并添加该脚本
                if(instance ==null)
                {
                    GameObject go = new GameObject(typeof(T).Name);
                    DontDestroyOnLoad(go);
                    instance = go.GetComponent<T>();
                    if (instance == null)
                    {
                        instance = go.AddComponent<T>();
                    }
                }
                                
            }
            return instance;
        }
    }
    #endregion

    void Awake()
    {
        OnAwake();
    }

    void Start ()
    {
        OnStart();
	}
	

	void Update ()
    {
        OnUpdate();
	}

    void OnDestroy()
    {
        BeforeOnDestroy();
    }

    protected virtual void OnAwake() { }
    protected virtual void OnStart() { }
    protected virtual void OnUpdate() { }
    protected virtual void BeforeOnDestroy() { }
}
