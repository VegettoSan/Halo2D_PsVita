/***********************************************************
 * 														   *
 * Asset:		 Touch Controls Kit     			       *
 * Script:		 ButtonBase.cs  	           		       *
 * 														   *
 * Copyright(c): Victor Klepikov						   *
 * Support: 	 http://bit.ly/vk-Support				   *
 * 														   *
 * mySite:       http://vkdemos.ucoz.org				   *
 * myAssets:     http://bit.ly/VictorKlepikovUnityAssets   *
 * myTwitter:	 http://twitter.com/VictorKlepikov		   *
 * myFacebook:	 http://www.facebook.com/vikle4 		   *
 * 														   *
 ***********************************************************/


using UnityEngine;

namespace TouchControlsKit
{
    public abstract class ButtonBase : ControllerBase
    {
        private const string nativeDownMethodName = "OnButtonDown";
        private const string nativePressMethodName = "OnButtonPress";
        private const string nativeUpMethodName = "OnButtonUp";


#if UNITY_EDITOR
        // for Editor
        public override string GetNativeNames()
        {
            return "Native Down: " + nativeDownMethodName +
                   "\nNative Press: " + nativePressMethodName +
                   "\nNative Up:      " + nativeUpMethodName;
        }
#endif

        private int pressedFrame = -1;
        private int releasedFrame = -1;


        // ButtonPRESSED
        internal bool ButtonPRESSED
        {
            get
            {
                return touchDown;
            }
        }

        // ButtonDOWN
        internal bool ButtonDOWN
        {
            get
            {
                return ( pressedFrame == Time.frameCount - 1 );
            }
        }

        // ButtonUP
        internal bool ButtonUP
        {
            get
            {
                return ( releasedFrame == Time.frameCount - 1 );
            }
        }


        // ControlAwake
        internal override void ControlAwake()
        {
            base.ControlAwake();
        }

        // UpdatePosition
        internal override void UpdatePosition( Vector2 touchPos )
        {
            if( touchDown )
            {
                // Broadcasting
                PressHandler( nativePressMethodName, myName );
            }
            else
            {
                touchDown = true;
                pressedFrame = Time.frameCount;
                ButtonDown();

                // Broadcasting
                DownHandler( nativeDownMethodName, myName );                
            }
        }

        // ButtonDown
        protected abstract void ButtonDown();

        // ControlReset
        internal override void ControlReset()
        {
            base.ControlReset();

            releasedFrame = Time.frameCount;
            ButtonUp();

            // Broadcasting
            UpHandler( nativeUpMethodName, myName );
        }

        // ButtonUp
        protected abstract void ButtonUp();
    }
}