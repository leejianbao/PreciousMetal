//===================
//描述：
//作者：#AuthorName#
//创建时间：2019-06-19 13:35:23
//版本：V1.0
//==================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ViewPage : UIBase
{
    private GameObject backBtn;
    private GameObject resetBtn;
    protected override void OnStart()
    {
        backBtn = transform.Find("BackHomeBtn").gameObject;
        EventTriggerListener.Get(backBtn).onClick += BackBtnClick;

        resetBtn = transform.Find("ResetPMBtn").gameObject;
        EventTriggerListener.Get(resetBtn).onClick += ResetBtnClick;

       
    }

    // Update is called once per frame
    protected override void OnUpdate()
    {

    }

    void BackBtnClick(GameObject btn)
    {
        UIManager.Instance.CloseAllWindowUI();
        UIManager.Instance.ShowWindowUI(WindowUIType.PMHomePage);
        GameManager.Instance.SetPreciousMetalActive(true);
        GameManager.Instance.SetPreciousMetalAutoRotate(true);      
    }

    void ResetBtnClick(GameObject btn)
    {
        GameManager.Instance.ResetPreciousMetal();
    }
}
