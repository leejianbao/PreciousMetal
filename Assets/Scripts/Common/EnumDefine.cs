using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region ResourceType 资源类型
/// <summary>
/// 资源类型
/// </summary>
public enum ResourceType
{
    WindowUI,       //窗口UI
    SceneUI,        //场景UI
    Prefab,         //预设
    Effect,         //特效
    Other           //其他
}
#endregion

#region SceneType 场景类型
/// <summary>
/// 场景类型
/// </summary>
public enum SceneType
{
    Init,           //login场景
    ARGold,         //VR场景
    ViewPMScene,//查看场景
    Other           //其他
}
#endregion

/// <summary>
/// 场景UI类型
/// </summary>
public enum SceneUIType
{

    Init,                   //初始页面（资源加载页）
    HomePage,               //主场景主页面
    GoldStrip,              //金条页面
    AccumulatedGold,        //积存金页面
    BraceletShow,           //手镯展示页
    BraceletTry             //手镯试戴页

}

/// <summary>
/// 窗口UI类型
/// </summary>
public enum WindowUIType
{
    None,                   //未设置
    FingerTip,              //手势提示
    AccumulatedGold,        //积存金提示
    BuyPage,                  //二维码
    PMHomePage,
    ViewPage
}

