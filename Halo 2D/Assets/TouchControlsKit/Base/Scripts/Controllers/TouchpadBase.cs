/***********************************************************
 * 														   *
 * Asset:		 Touch Controls Kit        			       *
 * Script:		 TouchpadBase.cs 	           		       *
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
    public class TouchpadBase : ControllerBase
    {
        //
        private const string nativeStartMethodName = "OnTouchpadStart";
        private const string nativeMoveMethodName = "OnTouchpadMove";
        private const string nativeEndMethodName = "OnTouchpadEnd";


#if UNITY_EDITOR
        // for Editor
        public override string GetNativeNames()
        {
            return "Native Start:  " + nativeStartMethodName +
                   "\nNative Move: " + nativeMoveMethodName +
                   "\nNative End:    " + nativeEndMethodName;
        }
#endif


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
                if( enableAxisX ) currentPosition.x = touchPos.x;
                if( enableAxisY ) currentPosition.y = touchPos.y;

                currentDirection = currentPosition - defaultPosition;
                
                float touchForce = Vector2.Distance( defaultPosition, currentPosition ) * 2f;
                defaultPosition = currentPosition;

                float asisX = currentDirection.normalized.x * touchForce / 100f * sensitivity;
                float asisY = currentDirection.normalized.y * touchForce / 100f * sensitivity;

                if( inverseAxisX ) asisX = -asisX;
                if( inverseAxisY ) asisY = -asisY;

                SetAxis( asisX, asisY );

                // Broadcasting
                PressHandler( nativeMoveMethodName, controllerData );
            }
            else
            {
                touchDown = true;
                defaultPosition = touchPos;
                currentPosition = defaultPosition;

                // Broadcasting
                DownHandler( nativeStartMethodName, controllerData );
            }
        }

        // ControlReset
        internal override void ControlReset()
        {
            base.ControlReset();

            // Broadcasting
            UpHandler( nativeEndMethodName, controllerData );
        }  
        //
    }
}