//===================
//描述：
//作者：#lijianbao#
//创建时间：2019-06-13 14:12:26
//版本：V1.0
//==================
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameManager:SingletonMono<GameManager>
{
    public PreciousMetal PM;
  
    protected override void OnAwake()
    { 
         LoadPreciousMetal("bracelet", "Bracelet");
    }

    protected override void OnStart()
    {
        
    }

    // Update is called once per frame
    protected override void OnUpdate()
    {
        
    }

    public void LoadPreciousMetal(string assetBundleName,string prefabName)
    {
        ClosePreciousMetal();
        GameObject o = ResourcesManager.Instance.LoadAssetBundle(assetBundleName, prefabName);
        o.name = "PreciousMetal";
        o.SetActive(true);
        PM = o.GetComponent<PreciousMetal>();

        GameObject homePage = UIManager.Instance.ShowWindowUI(WindowUIType.PMHomePage);
        homePage.GetComponent<HomePage>().LoadPreciousMetalUI();
    }
   
    public void Quit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
    }

    public void ResetPreciousMetal()
    {
        if (PM != null)
            PM.ResetPosition();
    }

    public void ScalePreciousMetal(float scaleMultiplier)
    {
        if(PM!=null)
        PM.Scale(scaleMultiplier);
    }

    public void RotatePreciousMetal(Vector3 eulers,Space relativeTo)
    {
        if(PM!=null)
        PM.Rotate(eulers, relativeTo);
    }

    public void SetPreciousMetalAutoRotate(bool startRotate)
    {
        if(PM!=null)
        {
            PM.AllowRotate = startRotate;
        }
    }

    public void GenerateQRCode(RawImage QRImage)
    {
        if(PM!=null && QRImage!= null && PM.QRCodeUrl!="")
        {
            QRCode.Instance.GenQRcode(QRImage,PM.QRCodeUrl);
        }
    }

    public void SetPreciousMetalActive(bool active)
    {
        if (PM != null)
            PM.gameObject.SetActive(active);
    }

    public void ClosePreciousMetal()
    {
        if(PM!=null)
        {
            PM.gameObject.SetActive(false);
            GameObject.Destroy(PM.gameObject);
            PM = null;
        }
    }
   

}
