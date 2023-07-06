/***********************************************************
 * 														   *
 * Asset:		 Touch Controls Kit     			       *
 * Script:		 JoystickBase.cs 	           		       *
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
    /// <summary>
    /// isStatic = true;  - Switches a joystick in a static mode, in which it is only at the specified position.
    /// isStatic = false; - Switches a joystick in the dynamic mode, in this mode, it operates within the touch zone.
    /// </summary>

    public abstract class JoystickBase : ControllerBase
    {
        [SerializeField]
        protected bool isStatic = true;
                
        public float borderSize = 5.85f;
        protected Vector2 borderPosition = Vector2.zero;

        public bool smoothReturn = false;
        public float smoothFactor = 7f;

        private float xVel, yVel;

        //
        private const string nativeStartMethodName = "OnJoystickStart";
        private const string nativeMoveMethodName = "OnJoystickMove";
        private const string nativeEndMethodName = "OnJoystickEnd";

#if UNITY_EDITOR
        // for Editor
        public override string GetNativeNames()
        {
            return "Native Start:  " + nativeStartMethodName +
                   "\nNative Move: " + nativeMoveMethodName +
                   "\nNative End:    " + nativeEndMethodName;
        }
#endif

        // JoystickMode
        public bool IsStatic
        {
            get { return isStatic; }
            set
            {
                if( isStatic == value ) return;
                isStatic = value;
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
            if( !enableAxisX && !enableAxisY ) 
                return;

            if( touchDown )
            {
                GetCurrentPosition( touchPos );

                currentDirection = currentPosition - defaultPosition;

                float currentDistance = Vector2.Distance( defaultPosition, currentPosition );
                float touchForce = 0f;

                float calculatedBorderSize = CalculateBorderSize();

                borderPosition = defaultPosition;
                borderPosition += currentDirection.normalized * calculatedBorderSize;

                if( currentDistance > calculatedBorderSize )
                {
                    currentPosition = borderPosition;
                    touchForce = 100f;
                }
                else touchForce = ( currentDistance / calculatedBorderSize ) * 100f;

                UpdateJoystickPosition();

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
                if( !isStatic ) UpdateTransparencyAndPosition( touchPos );

                // Broadcasting
                DownHandler( nativeStartMethodName, controllerData );
            }
        }                

        // GetCurrentPosition
        protected abstract void GetCurrentPosition( Vector2 touchPos );

        // CalculateBorderSize
        protected abstract float CalculateBorderSize();

        // UpdateJoystickPosition
        protected abstract void UpdateJoystickPosition();

        //Update Transparency And Position for Dynamic Joystick
        protected abstract void UpdateTransparencyAndPosition( Vector2 touchPos );

        // SmoothReturnRun
        private System.Collections.IEnumerator SmoothReturnRun()
        {
            bool smoothReturnIsRun = true;

            while( smoothReturnIsRun && !touchDown && isStatic )
            {
                int dpMag = ( int )defaultPosition.sqrMagnitude;
                int cpMag = ( int )currentPosition.sqrMagnitude;

                currentPosition.x = Mathf.SmoothDamp( currentPosition.x, defaultPosition.x, ref xVel, smoothFactor * Time.smoothDeltaTime );
                currentPosition.y = Mathf.SmoothDamp( currentPosition.y, defaultPosition.y, ref yVel, smoothFactor * Time.smoothDeltaTime );

                if( cpMag == dpMag )
                {
                    currentPosition = defaultPosition;
                    smoothReturnIsRun = false;
                    xVel = yVel = 0f;
                }
                UpdateJoystickPosition();

                yield return null;
            }
        }

        // ControlReset
        internal override void ControlReset()
        {
            base.ControlReset();

            if( smoothReturn && isStatic )
            {
                StopCoroutine( "SmoothReturnRun" );
                StartCoroutine( "SmoothReturnRun" );
            }
            else
            {
                currentPosition = defaultPosition;
                UpdateJoystickPosition();
            }

            // Broadcasting
            UpHandler( nativeEndMethodName, controllerData );
        }
    }
}