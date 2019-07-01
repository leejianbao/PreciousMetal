//===================
//描述：
//作者：#AuthorName#
//创建时间：2019-06-19 11:05:16
//版本：V1.0
//==================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomePage : UIBase
{
    // Start is called before the first frame update
    private GameObject viewBtn;
    private GameObject buyBtn;
    protected override void OnStart()
    {
        viewBtn = transform.Find("PMInfo/Button/ViewBtn").gameObject;
        EventTriggerListener.Get(viewBtn).onClick += ViewBtnClick;

        buyBtn = transform.Find("PMInfo/Button/BuyBtn").gameObject;
        EventTriggerListener.Get(buyBtn).onClick += BuyBtnClick;

        LoadPreciousMetalUI();
    }

    // Update is called once per frame
    protected override void OnUpdate()
    {
        
    }

    void ViewBtnClick(GameObject btn)
    {
        UIManager.Instance.CloseWindowUI(WindowUIType.PMHomePage);
        UIManager.Instance.CloseWindowUI(WindowUIType.BuyPage);
        UIManager.Instance.ShowWindowUI(WindowUIType.ViewPage);

        GameManager.Instance.SetPreciousMetalActive(true);
        GameManager.Instance.SetPreciousMetalAutoRotate(false);
    }

    void BuyBtnClick(GameObject btn)
    {
    
        UIManager.Instance.CloseWindowUI(WindowUIType.ViewPage);
        GameManager.Instance.SetPreciousMetalActive(false);
        UIManager.Instance.ShowWindowUI(WindowUIType.BuyPage);
    }

    public void LoadPreciousMetalUI()
    {
        SetTextData();
        SetTextColor();
    }

    private void SetTextData()
    {
        if(GameManager.Instance.PM != null)
        { 
            transform.Find("PMInfo/Text/MetalNameText").gameObject.GetComponent<Text>().text = GameManager.Instance.PM.MetalName;
            transform.Find("PMInfo/Text/DimentionsText").gameObject.GetComponent<Text>().text = GameManager.Instance.PM.Dimentions;
            transform.Find("PMInfo/Text/MadeofMaterialText").gameObject.GetComponent<Text>().text = GameManager.Instance.PM.MadeofMaterial;
            transform.Find("PMInfo/Text/IndustrialArtText").gameObject.GetComponent<Text>().text = GameManager.Instance.PM.IndustrialArt;
            transform.Find("PMInfo/Text/WeightText").gameObject.GetComponent<Text>().text = GameManager.Instance.PM.Weight.ToString()+"g";
            transform.Find("PMInfo/Text/PriceText").gameObject.GetComponent<Text>().text = GameManager.Instance.PM.Price.ToString()+".00";
        }
    }
    private void SetTextColor()
    {

        GameObject InfoText = transform.Find("PMInfo/Text").gameObject;
       
        for (int i = 0; i < InfoText.transform.childCount; i++)
        {
            InfoText.transform.GetChild(i).GetComponent<Text>().color = new Color(255, 215, 0);//色号
        }
    }

}
