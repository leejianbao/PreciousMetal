//===================
//描述：
//作者：#AuthorName#
//创建时间：2019-06-19 14:49:37
//版本：V1.0
//==================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyPage : UIBase
{
    private GameObject closeBtn;
    protected override void OnStart()
    {
        closeBtn = transform.Find("CloseBtn").gameObject;
        EventTriggerListener.Get(closeBtn).onClick += CloseBtnClick;
        GenerateQRCode();
    }

    // Update is called once per frame
    protected override void OnUpdate()
    {

    }

    public void GenerateQRCode()
    {
        RawImage rawImg = transform.Find("QRCodeIMG").gameObject.GetComponent<RawImage>();
        GameManager.Instance.GenerateQRCode(rawImg);
    }

    void CloseBtnClick(GameObject btn)
    {
        // transform.parent.Find("HomePage").gameObject.SetActive(true);
        UIManager.Instance.CloseAllWindowUI();
        UIManager.Instance.ShowWindowUI(WindowUIType.PMHomePage);
        GameManager.Instance.SetPreciousMetalActive(true);
        GameManager.Instance.SetPreciousMetalAutoRotate(true);
       // transform.parent.Find("BuyPage").gameObject.SetActive(false);
       // transform.parent.Find("ViewPage").gameObject.SetActive(false);
    }


}
