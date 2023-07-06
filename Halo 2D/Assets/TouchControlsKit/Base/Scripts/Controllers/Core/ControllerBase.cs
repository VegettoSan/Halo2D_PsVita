/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 ControllerBase.cs            		 *
 * 													 *
 * Copyright(c): Victor Klepikov					 *
 * Support: 	 http://bit.ly/vk-Support			 *
 * 													 *
 * mySite:       http://vkdemos.ucoz.org			 *
 * myAssets:     http://u3d.as/5Fb                   *
 * myTwitter:	 http://twitter.com/VictorKlepikov	 *
 * myFacebook:	 http://www.facebook.com/vikle4 	 *
 * 													 *
 ****************************************************/


using UnityEngine;

namespace TouchControlsKit
{
    public enum ControllerAnchor : byte
    {
        UpperLeft,
        UpperCenter,
        UpperRight,
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        LowerLeft,
        LowerCenter,
        LowerRight,
    }     

    public enum SendingModes : byte
    {
        SendMessage,
        SendMessageUpwards,
        BroadcastMessage
    }

    [System.Serializable]
    public class Receiver
    {
        public bool enabled = true;
        public GameObject receiver = null;
    }

    public struct ControllerData
    {
        internal string myName;
        internal float axisX;
        internal float axisY;
    }

    // ControllerBase
    public abstract class ControllerBase : MonoBehaviour
    {
        protected ControllerData controllerData;

        public float sensitivity = 4f;

        [SerializeField]
        protected bool showTouchZone = true;

        [SerializeField]
        protected string myName = "Controller";

        public bool enableAxisX = true;
        public bool inverseAxisX = false;

        [SerializeField]
        protected string axisNameX = "Horizontal";

        public bool enableAxisY = true;
        public bool inverseAxisY = false;

        [SerializeField]
        protected string axisNameY = "Vertical";

        public bool broadcastMessages = false;
        public bool customMethods = false;

        internal int touchId = -1;
        internal bool touchDown = false;

        public SendingModes sendingMode = SendingModes.SendMessage;
        public System.Collections.Generic.List<Receiver> receivers = new System.Collections.Generic.List<Receiver>();

        public string downMethodName, pressMethodName, upMethodName;

        protected Vector2 defaultPosition, currentPosition, currentDirection;


        // GetAxis
        internal float AxisValueX { get; private set; }   // Horizontal
        internal float AxisValueY { get; private set; }   // Vertical


#if UNITY_EDITOR
        // for Editor
        public abstract string GetNativeNames();
#endif

        // ShowTouchZone
        public bool ShowTouchZone
        {
            get { return showTouchZone; }
            set
            {
                if( showTouchZone == value ) return;
                showTouchZone = value;
                ShowingTouchZone();
            }
        }

        // ShowingTouchZone
        protected virtual void ShowingTouchZone()
        { }

        // MyName
        public string MyName
        {
            get { return myName; }
            set
            {
                if( myName == value ) return;

                if( value == string.Empty )
                {
                    Debug.LogError( "ERROR: Controller name for " + gameObject.name + " cannot be empty" );
                    return;
                }

                myName = value;
                controllerData.myName = myName;
                gameObject.name = myName;
            }
        }

        // AxisNameX
        public string AxisNameX
        {
            get { return axisNameX; }
            set
            {
                if( axisNameX == value ) return;
                if( value == string.Empty )
                {
                    Debug.LogError( "ERROR: name for Axis_X cannot be empty!" );
                    return;
                }
                if( value == axisNameY )
                {
                    Debug.LogError( "ERROR: names of axisNameY and axisNameX must be different!" );
                    return;
                }
                axisNameX = value;
            }
        }

        // AxisNameY
        public string AxisNameY
        {
            get { return axisNameY; }
            set
            {
                if( axisNameY == value ) return;
                if( value == string.Empty )
                {
                    Debug.LogError( "ERROR: name for Axis_Y cannot be empty!" );
                    return;
                }
                if( value == axisNameX )
                {
                    Debug.LogError( "ERROR: names of axisNameX and axisNameY must be different!" );
                    return;
                }
                axisNameY = value;
            }
        }


        // ControlAwake
        internal virtual void ControlAwake()
        {
            controllerData.myName = myName;
        }

        // ControlDisable
        internal virtual void ControlDisable()
        { }

        // CalculationSizeAndPosition
        internal virtual void CalculationSizeAndPosition()
        { }

        // CheckPosition
        internal virtual bool CheckTouchPosition( Vector2 touchPos )
        { return false; }

        // UpdatePosition
        internal abstract void UpdatePosition( Vector2 touchPos );

        // ControlReset
        internal virtual void ControlReset()
        {
            touchId = -1;
            touchDown = false;
            SetAxis( 0f, 0f );
        }

        // SetAxis
        protected void SetAxis( float x, float y )
        {
            AxisValueX = x;
            AxisValueY = y;
            controllerData.axisX = x;
            controllerData.axisY = y;
        }
        

        // DownHandler
        protected void DownHandler( string nativeMethodName, object value )
        {
            if( broadcasting )
            {
                if( customMethods )
                {
                    if( downMethodName == string.Empty )
                    {
                        Debug.LogError( "ERROR: Start Method Name is Empty! Error in: " + myName );
                        return;
                    }
                    MessagesHandler( downMethodName, value );
                }
                else
                {
                    MessagesHandler( nativeMethodName, value );
                }
            }
        }

        // PressHandler
        protected void PressHandler( string nativeMethodName, object value )
        {
            if( broadcasting )
            {
                if( customMethods )
                {
                    if( downMethodName == string.Empty )
                    {
                        Debug.LogError( "ERROR: Move Method Name is Empty! Error in: " + myName );
                        return;
                    }
                    MessagesHandler( pressMethodName, value );
                }
                else
                {
                    MessagesHandler( nativeMethodName, value );
                }
            }
        }

        // UpHandler
        protected void UpHandler( string nativeMethodName, object value )
        {
            if( broadcasting )
            {
                if( customMethods )
                {
                    if( downMethodName == string.Empty )
                    {
                        Debug.LogError( "ERROR: End Method Name is Empty! Error in: " + myName );
                        return;
                    }
                    MessagesHandler( upMethodName, value );
                }
                else
                {
                    MessagesHandler( nativeMethodName, value );
                }
            }
        }


        // broadcasting
        private bool broadcasting
        {
            get
            {
                if( !broadcastMessages ) return false;

                for( int cnt = 0; cnt < receivers.Count; cnt++ )
                {
                    if( receivers[ cnt ].enabled && receivers[ cnt ].receiver == null )
                    {
                        Debug.LogError( "ERROR: Receiver GameObject is NULL! Error in: " + myName );
                        return false;
                    }
                }
                return true;
            }
        }


        // MessagesHandler
        private void MessagesHandler( string methodName, object value )
        {
            switch( sendingMode )
            {
                case SendingModes.SendMessage:
                    for( int cnt = 0; cnt < receivers.Count; cnt++ )
                        if( receivers[ cnt ].enabled )
                            receivers[ cnt ].receiver.SendMessage( methodName, value, SendMessageOptions.DontRequireReceiver );
                    break;

                case SendingModes.SendMessageUpwards:
                    for( int cnt = 0; cnt < receivers.Count; cnt++ )
                        if( receivers[ cnt ].enabled )
                            receivers[ cnt ].receiver.SendMessageUpwards( methodName, value, SendMessageOptions.DontRequireReceiver );
                    break;

                case SendingModes.BroadcastMessage:
                    for( int cnt = 0; cnt < receivers.Count; cnt++ )
                        if( receivers[ cnt ].enabled )
                            receivers[ cnt ].receiver.BroadcastMessage( methodName, value, SendMessageOptions.DontRequireReceiver );
                    break;
            }
        }
        //
    }
}