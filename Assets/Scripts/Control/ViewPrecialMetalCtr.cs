//===================
//描述：
//作者：#AuthorName#
//创建时间：2019-06-13 15:48:17
//版本：V1.0
//==================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DigitalRubyShared
{
    public class ViewPrecialMetalCtr : MonoBehaviour
    {
       

        private void DebugText(string text, params object[] format)
        {
            //bottomLabel.text = string.Format(text, format);
            Debug.Log(string.Format(text, format));
        }

        GameObject draggingPreciousMetal;

        private PanGestureRecognizer panGesture;
        private ScaleGestureRecognizer scaleGesture;
        private SwipeGestureRecognizer swipeGesture;
        private LongPressGestureRecognizer longPressGesture;

        #region
        //双指放大缩小
        private void CreateScaleGesture()
        {
            scaleGesture = new ScaleGestureRecognizer();
            scaleGesture.StateUpdated += ScaleGestureCallback;
            FingersScript.Instance.AddGesture(scaleGesture);
        }
        private void ScaleGestureCallback(GestureRecognizer gesture)
        {
            if (gesture.State == GestureRecognizerState.Executing)
            {
                DebugText("Scaled: {0}, Focus: {1}, {2}", scaleGesture.ScaleMultiplier, scaleGesture.FocusX, scaleGesture.FocusY);
                GameManager.Instance.ScalePreciousMetal(scaleGesture.ScaleMultiplier);
            }
        }
        #endregion

        #region
        //双指拖动
        private void CreatePanGesture()
        {
            panGesture = new PanGestureRecognizer();
            panGesture.MinimumNumberOfTouchesToTrack = 2;
            panGesture.StateUpdated += PanGestureCallback;
            FingersScript.Instance.AddGesture(panGesture);
        }
        private void PanGestureCallback(GestureRecognizer gesture)
        {
            if (gesture.State == GestureRecognizerState.Executing)
            {
                DebugText("Panned, Location: {0}, {1}, Delta: {2}, {3}", gesture.FocusX, gesture.FocusY, gesture.DeltaX, gesture.DeltaY);
              //  GameManager.Instance.PM.transform.Translate(0.01f*gesture.DeltaX, 0, 0, Space.Self);
              if (gesture.DeltaX > gesture.DeltaY)
              GameManager.Instance.LoadPreciousMetal("goldbrick", "GoldBrick");
              else
              GameManager.Instance.LoadPreciousMetal("bracelet", "Bracelet");
            }
        }
        #endregion
 

        #region
        //旋转效果不是很好，需要滑动完成去转动
        private void CreateSwipeGesture()
        {
            swipeGesture = new SwipeGestureRecognizer();
            swipeGesture.StateUpdated += SwipeGestureCallBack;
            FingersScript.Instance.AddGesture(swipeGesture);
        }
        private void SwipeGestureCallBack(GestureRecognizer gesture)
        {

            if (gesture.State == GestureRecognizerState.Ended)
            {
                DebugText("Swiped from {0},{1} to {2},{3}; velocity: {4}, {5}", gesture.StartFocusX, gesture.StartFocusY, gesture.FocusX, gesture.FocusY, swipeGesture.VelocityX, swipeGesture.VelocityY);
               // GameManager
            }
        }
        #endregion

        #region
        //自定义旋转效果
        /// <summary>
        /// 单手滑动
        /// </summary>
        public  enum OneFingerMoveDirection
        {
            Left,
            Right,
            Down,
            Up,
            Any
        }
        public float RotateSpeed = 20;
        Vector3 startFingerPos = Vector3.zero;
        Vector3 nowFingerPos = Vector3.zero;
        public  OneFingerMoveDirection GetOneFingerMoveDirection()
        {
            OneFingerMoveDirection direction = OneFingerMoveDirection.Any;

            if (Input.touchCount == 1)
            {
                if (Input.GetTouch(0).phase == UnityEngine.TouchPhase.Began)
                {
                    //Debug.Log("======开始触摸=====");    
                    startFingerPos = Input.GetTouch(0).position;
                }

                nowFingerPos = Input.GetTouch(0).position;

                if ((Input.GetTouch(0).phase == UnityEngine.TouchPhase.Stationary) || (Input.GetTouch(0).phase == UnityEngine.TouchPhase.Ended))
                {
                    startFingerPos = nowFingerPos;
                    //Debug.Log("======释放触摸=====");    
                    return OneFingerMoveDirection.Any;
                }
                if (startFingerPos == nowFingerPos)
                {
                    return OneFingerMoveDirection.Any;
                }
               float xMoveDistance = Mathf.Abs(nowFingerPos.x - startFingerPos.x);
               float yMoveDistance = Mathf.Abs(nowFingerPos.y - startFingerPos.y);

                if (xMoveDistance > yMoveDistance)
                {
                    if (nowFingerPos.x - startFingerPos.x > 0)
                    {
                        direction = OneFingerMoveDirection.Left; //沿着X轴负方向移动    
                    }
                    else
                    {
                        direction = OneFingerMoveDirection.Right; //沿着X轴正方向移动    
                    }
                }
                else
                {
                    if (nowFingerPos.y - startFingerPos.y > 0)
                    {
                        direction = OneFingerMoveDirection.Up; //沿着Y轴正方向移动    
                    }
                    else
                    {
                        direction = OneFingerMoveDirection.Down; //沿着Y轴负方向移动    
                    }

                }
            }

                return direction;
         }


        #endregion


        private static bool? CaptureGestureHandler(GameObject obj)
        {
            // I've named objects PassThrough* if the gesture should pass through and NoPass* if the gesture should be gobbled up, everything else gets default behavior
            if (obj.name.StartsWith("PassThrough", System.StringComparison.CurrentCulture))
            {
                // allow the pass through for any element named "PassThrough*"
                return false;
            }
            else if (obj.name.StartsWith("NoPass", System.StringComparison.CurrentCulture))
            {
                // prevent the gesture from passing through, this is done on some of the buttons and the bottom text so that only
                // the triple tap gesture can tap on it
                return true;
            }

            // fall-back to default behavior for anything else
            // return null;
            return false;
        }

        private void Start()
        {
          
            CreateSwipeGesture();
            CreatePanGesture();
            CreateScaleGesture();

            panGesture.AllowSimultaneousExecution(scaleGesture);
            FingersScript.Instance.CaptureGestureHandler = CaptureGestureHandler;
        }

        private void Update()
        {
            switch (GetOneFingerMoveDirection())
            {
                case OneFingerMoveDirection.Left:
                    GameManager.Instance.RotatePreciousMetal(Vector3.up * -1 * Time.deltaTime * RotateSpeed, Space.World);
                    break;
                case OneFingerMoveDirection.Right:
                    GameManager.Instance.RotatePreciousMetal(Vector3.up  * Time.deltaTime * RotateSpeed, Space.World);
                    break;
                default:break;
            }
        }

       


    }
}