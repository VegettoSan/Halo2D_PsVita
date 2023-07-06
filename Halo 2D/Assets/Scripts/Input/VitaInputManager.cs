using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class VitaInputManager : MonoBehaviour {
 
    #region SingletonSetup
    
        private static VitaInputManager instance;
 
        public static VitaInputManager Instance
        {
            get
            {
                if (!instance)
                {
                    instance = GameObject.FindObjectOfType(typeof(VitaInputManager)) as VitaInputManager;
                    if (!instance)
                    {
                        //VitaDebug.Log("No active VitaInputManager script on any GameObject");
                    }
                }
 
                return instance;
            }
        }
    
    #endregion
 
    #region Delegates
 
        public delegate void FaceButtonEvent();
 
        public delegate void DpadButtonEvent();
 
        public delegate void TriggerButtonEvent();
 
        public delegate void StartSelectEvent();
 
        public delegate void LeftStickEvent(float horizontal, float vertical);
 
        public delegate void RightStickEvent(float horizontal, float vertical);
    
    #endregion
    
    //events
 
    #region FaceButtonEvents
    
        //cross events
        public event FaceButtonEvent OnCross;
        public event FaceButtonEvent OnCrossDown;
        public event FaceButtonEvent OnCrossUp;
        //square events
        public event FaceButtonEvent OnSquare;
        public event FaceButtonEvent OnSquareDown;
        public event FaceButtonEvent OnSquareUp;
        //triangle events
        public event FaceButtonEvent OnTriangle;
        public event FaceButtonEvent OnTriangleDown;
        public event FaceButtonEvent OnTriangleUp;
        //circle events
        public event FaceButtonEvent OnCircle;
        public event FaceButtonEvent OnCircleDown;
        public event FaceButtonEvent OnCircleUp;
    
    #endregion
 
    #region DPadEvents
 
        public event DpadButtonEvent OnDpadUp;
        public event DpadButtonEvent OnDpadDown;
        public event DpadButtonEvent OnDpadLeft;
        public event DpadButtonEvent OnDpadRight;
 
    #endregion
 
    #region TriggerEvents
 
        //L Trigger events
        public event TriggerButtonEvent OnLTrig;
        public event TriggerButtonEvent OnLTrigDown;
        public event TriggerButtonEvent OnLTrigUp;
        //R Trigger events
        public event TriggerButtonEvent OnRTrig;
        public event TriggerButtonEvent OnRTrigDown;
        public event TriggerButtonEvent OnRTrigUp;
    
    #endregion
 
    #region StartSelectEvents
 
        public event StartSelectEvent OnStart;
        public event StartSelectEvent OnSelect;
 
    #endregion
 
    #region JoystickEvents
 
        public event LeftStickEvent OnLeftStick;
        public event RightStickEvent OnRightStick;
 
    #endregion
    
    
    // Update is called once per frame
    void Update () {
        
        #region Cross
        
            if (Input.GetButton("Cross"))
            {
                if (OnCross != null)
                {
                    OnCross();
                }
            }
            if (Input.GetButtonDown("Cross"))
            {
                if (OnCrossDown != null)
                {
                    OnCrossDown();
                }
            }
            if (Input.GetButtonUp("Cross"))
            {
                if (OnCrossUp != null)
                {
                    OnCrossUp();
                }
            }
            
        #endregion
        
        #region Square
            
            if (Input.GetButton("Square"))
            {
                if (OnSquare != null)
                {
                    OnSquare();
                }
            }
            if (Input.GetButtonDown("Square"))
            {
                if (OnSquareDown != null)
                {
                    OnSquareDown();
                }
            }
            if (Input.GetButtonUp("Square"))
            {
                if (OnCrossUp != null)
                {
                    OnCrossUp();
                }
            }
            
        #endregion
        
        #region Triangle
            
            if (Input.GetButton("Triangle"))
            {
                if (OnTriangle != null)
                {
                    OnTriangle();
                }
            }
            if (Input.GetButtonDown("Triangle"))
            {
                if (OnTriangleDown != null)
                {
                    OnTriangleDown();
                }
            }
            if (Input.GetButtonUp("Triangle"))
            {
                if (OnTriangleUp != null)
                {
                    OnTriangleUp();
                }
            }
            
        #endregion
        
        #region Circle
            
            if (Input.GetButton("Circle"))
            {
                if (OnCircle != null)
                {
                    OnCircle();
                }
            }
            if (Input.GetButtonDown("Circle"))
            {
                if (OnCircleDown != null)
                {
                    OnCircleDown();
                }
            }
            if (Input.GetButtonUp("Circle"))
            {
                if (OnCircleUp != null)
                {
                    OnCircleUp();
                }
            }
            
        #endregion
 
        #region Triggers
 
            //L Trigger
            if (Input.GetButton("LTRIG"))
            {
                if (OnLTrig != null)
                {
                    OnLTrig();
                }
            }
            if (Input.GetButtonDown("LTRIG"))
            {
                if (OnLTrigDown != null)
                {
                    OnLTrigDown();
                }
            }
            if (Input.GetButtonUp("LTRIG"))
            {
                if (OnLTrigUp != null)
                {
                    OnLTrigUp();
                }
            }
            //R Trigger
            if (Input.GetButton("RTRIG"))
            {
                if (OnRTrig != null)
                {
                    OnRTrig();
                }
            }
            if (Input.GetButtonDown("RTRIG"))
            {
                if (OnRTrigDown != null)
                {
                    OnRTrigDown();
                }
            }
            if (Input.GetButtonUp("RTRIG"))
            {
                if (OnRTrigUp != null)
                {
                    OnRTrigUp();
                }
            }
 
        #endregion
 
        #region StartSelect
 
            if (Input.GetButton("Start"))
            {
                if (OnStart != null)
                {
                    OnStart();
                }
            }
            if (Input.GetButton("Select"))
            {
                if (OnSelect != null)
                {
                    OnSelect();
                }   
            }
 
        #endregion
 
        #region Joysticks
 
            if (OnLeftStick != null)
            {
                OnLeftStick(Input.GetAxis("Left Stick Horizontal"),
                                Input.GetAxis("Left Stick Vertical"));
            }
            if (OnRightStick != null)
            {
                OnRightStick(Input.GetAxis("Right Stick Horizontal"),
                                Input.GetAxis("Right Stick Vertical"));
            }
 
        #endregion
        
    }
 
}