using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 手势提示部分窗口
/// </summary>
public class FingerWindowUI : WindowUIBase
{
    #region 左侧手势提示部分
    [SerializeField]
    Button moveLightBtn;            //高亮移动按钮
    [SerializeField]
    Button moveDarkBtn;             //灰暗移动按钮
    [SerializeField]
    GameObject moveImage;           //移动提示图片物体
    [SerializeField]
    Button selectLightBtn;          //高亮选中按钮
    [SerializeField]
    Button selectDarkBtn;           //灰暗选中按钮
    [SerializeField]
    GameObject selectImage;         //选中提示图片物体
    [SerializeField]
    Button scaleLightBtn;           //高亮缩放按钮
    [SerializeField]
    Button scaleDarkBtn;            //灰暗缩放按钮
    [SerializeField]
    GameObject scaleImage;          //缩放提示图片物体

    Tween moveTween = null;         //移动的DoTween动画进程
    Tween selectTween = null;       //选中的DoTween动画进程
    Tween scaleTween = null;        //缩放的DoTween动画进程
    #endregion

    private void OnEnable()
    {
        //初始按钮为收缩状态
        CloseMove();
        CloseSelect();
        CloseScale();

        //添加按钮的监听
        moveLightBtn.onClick.AddListener(CloseMove);
        moveDarkBtn.onClick.AddListener(OpenMove);
        selectLightBtn.onClick.AddListener(CloseSelect);
        selectDarkBtn.onClick.AddListener(OpenSelect);
        scaleLightBtn.onClick.AddListener(CloseScale);
        scaleDarkBtn.onClick.AddListener(OpenScale);
    }

    private void OnDisable()
    {
        //移除按钮的监听
        moveLightBtn.onClick.RemoveListener(CloseMove);
        moveDarkBtn.onClick.RemoveListener(OpenMove);
        selectLightBtn.onClick.RemoveListener(CloseSelect);
        selectDarkBtn.onClick.RemoveListener(OpenSelect);
        scaleLightBtn.onClick.RemoveListener(CloseScale);
        scaleDarkBtn.onClick.RemoveListener(OpenScale);
    }


    /// <summary>
    /// 打开移动提示图标
    /// </summary>
    private void OpenMove()
    {
        moveDarkBtn.gameObject.SetActive(false);
        moveLightBtn.gameObject.SetActive(true);
        moveImage.transform.parent.gameObject.SetActive(true);
        if (moveTween != null)
        {
            moveTween.Kill();           //如果DoTween动画进程存在，先杀死，再执行新的DoTween动画
        }
        moveTween = moveImage.GetComponent<RectTransform>().DOAnchorPosX(20.5f, 0.6f);
    }

    /// <summary>
    /// 关闭移动提示图标
    /// </summary>
    private void CloseMove()
    {
        if (moveTween != null)
        {
            moveTween.Kill();
        }

        //动画执行完毕的回调，由lambda完成
        moveTween =
        moveImage.GetComponent<RectTransform>().DOAnchorPosX(-200, 0.6f).OnComplete(() => {
            moveDarkBtn.gameObject.SetActive(true);
            moveLightBtn.gameObject.SetActive(false);
            moveImage.transform.parent.gameObject.SetActive(false);
        });
    }

    /// <summary>
    /// 打开选中提示图标
    /// </summary>
    private void OpenSelect()
    {
        selectDarkBtn.gameObject.SetActive(false);
        selectLightBtn.gameObject.SetActive(true);
        selectImage.transform.parent.gameObject.SetActive(true);
        if (selectTween != null)
        {
            selectTween.Kill();
        }
        selectTween = selectImage.GetComponent<RectTransform>().DOAnchorPosX(20.5f, 0.6f);
    }

    /// <summary>
    /// 关闭选中提示图标
    /// </summary>
    private void CloseSelect()
    {
        if (selectTween != null)
        {
            selectTween.Kill();
        }
        selectTween =
        selectImage.GetComponent<RectTransform>().DOAnchorPosX(-200, 0.6f).OnComplete(() => {
            selectDarkBtn.gameObject.SetActive(true);
            selectLightBtn.gameObject.SetActive(false);
            selectImage.transform.parent.gameObject.SetActive(false);
        });
    }

    /// <summary>
    /// 打开缩放提示图标
    /// </summary>
    private void OpenScale()
    {
        scaleDarkBtn.gameObject.SetActive(false);
        scaleLightBtn.gameObject.SetActive(true);
        scaleImage.transform.parent.gameObject.SetActive(true);
        if (scaleTween != null)
        {
            scaleTween.Kill();
        }
        scaleTween = scaleImage.GetComponent<RectTransform>().DOAnchorPosX(20.5f, 0.6f);
    }

    /// <summary>
    /// 关闭缩放提示图标
    /// </summary>
    private void CloseScale()
    {
        if (scaleTween != null)
        {
            scaleTween.Kill();
        }
        scaleTween =
        scaleImage.GetComponent<RectTransform>().DOAnchorPosX(-200, 0.6f).OnComplete(() => {
            scaleDarkBtn.gameObject.SetActive(true);
            scaleLightBtn.gameObject.SetActive(false);
            scaleImage.transform.parent.gameObject.SetActive(false);
        });
    }

}
