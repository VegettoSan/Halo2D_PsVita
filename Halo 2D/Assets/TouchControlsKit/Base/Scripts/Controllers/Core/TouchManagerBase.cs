/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 TouchManagerBase.cs                 *
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
    public class TouchManagerBase : MonoBehaviour
    {
        protected static ControllerBase[] controllers = null;
        protected static int controllersCount = 0;

        /// <summary>
        /// Allows to control TouchManager from code.
        /// </summary>
        public static TouchManagerBase Instance { get; private set; }


        /// <summary>
        /// Activates/Deactivates the all controllers in scene.
        /// </summary>
        /// <param name="value"></param>
        public void SetActive( bool value )
        {
            this.enabled = value;
        }

        // OnEnable
        void OnEnable()
        {
            if( Application.isPlaying ) TouchManagerSetup();
        }

        // OnDisable
        void OnDisable()
        {
            if( !Application.isPlaying ) return;

            if( controllersCount != 0 )
                for( int cnt = 0; cnt < controllersCount; cnt++ )
                    controllers[ cnt ].ControlDisable();
        }

        // TouchManagerSetup
        public void TouchManagerSetup()
        {
            Instance = this;

            controllers = gameObject.GetComponentsInChildren<ControllerBase>();
            controllersCount = controllers.Length;

            for( int cnt = 0; cnt < controllersCount; cnt++ )
                controllers[ cnt ].ControlAwake();

            InputManager.InputRegister( gameObject );
        }

        // ChecMutitouch
        protected void FinalUpdate( int touchCount )
        {
            if( touchCount > 0 )
            {
                for( int cnt = 0; cnt < touchCount; cnt++ )
                {
                    for( int dnt = 0; dnt < controllers.Length; dnt++ )
                    {
                        TouchInput( Input.GetTouch( cnt ), controllers[ dnt ] );
                    }
                }
            }
            else
            {
                for( int cnt = 0; cnt < controllers.Length; cnt++ )
                {
                    MouseInput( controllers[ cnt ] );
                }
            }
        }

        // JoyTouchManagment
        private void TouchInput( Touch touch, ControllerBase controller )
        {
            switch( touch.phase )
            {
                case TouchPhase.Began:
                    if( controller.CheckTouchPosition( touch.position ) && !controller.touchDown )
                    {
                        controller.touchId = touch.fingerId;
                        controller.UpdatePosition( touch.position );
                    }
                    break;

                case TouchPhase.Stationary:
                case TouchPhase.Moved:
                    if( controller.touchId == touch.fingerId && controller.touchDown )
                    {
                        controller.UpdatePosition( touch.position );
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if( controller.touchId == touch.fingerId && controller.touchDown )
                    {
                        controller.ControlReset();
                    }
                    break;
            }
        }

        // JoyMouseManagment
        private void MouseInput( ControllerBase controller )
        {
            if( controller.CheckTouchPosition( Input.mousePosition ) && Input.GetMouseButtonDown( 0 ) )
                controller.UpdatePosition( Input.mousePosition );

            if( controller.touchDown && Input.GetMouseButton( 0 ) )
                controller.UpdatePosition( Input.mousePosition );

            if( Input.GetMouseButtonUp( 0 ) && controller.touchDown )
                controller.ControlReset();
        }
        //
    }
}