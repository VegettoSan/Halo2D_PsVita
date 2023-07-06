/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 InputManager.cs               		 *
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
    public sealed class InputManager
    {
        private static ControllerBase[] controllers = null;
        private static int controllersCount = 0;

        private static ButtonBase[] buttons = null;
        private static int buttonsCount = 0;


        // InputManagerSetup
        internal static void InputRegister( GameObject gameObject )
        {
            controllers = gameObject.GetComponentsInChildren<ControllerBase>();
            controllersCount = controllers.Length;

            buttons = gameObject.GetComponentsInChildren<ButtonBase>();
            buttonsCount = buttons.Length;
        }
        

        /// <summary>
        /// Returns the value of the joystick or touchpad Axis identified by controllerName & axisName.
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="axisName"></param>
        /// <returns></returns>
        public static float GetAxis( string controllerName, string axisName )
        {
            for( int cnt = 0; cnt < controllersCount; cnt++ )
            {
                if( controllers[ cnt ].MyName == controllerName )
                {
                    if( axisName == controllers[ cnt ].AxisNameX ) return controllers[ cnt ].AxisValueX;
                    else if( axisName == controllers[ cnt ].AxisNameY ) return controllers[ cnt ].AxisValueY;
                    Debug.LogError( "Axis name: " + axisName + " not found!" );
                    return 0f;
                }
            }
            Debug.LogError( "Controller name: " + controllerName + " not found!" );
            return 0f;
        }


        /// <summary>
        /// Returns the value of the joystick or touchpad axis Enable identified by controllerName & axisName.
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="axisName"></param>
        /// <returns></returns>
        public static bool GetAxisEnable( string controllerName, string axisName )
        {
            for( int cnt = 0; cnt < controllersCount; cnt++ )
            {
                if( controllers[ cnt ].MyName == controllerName )
                {
                    if( axisName == controllers[ cnt ].AxisNameX ) return controllers[ cnt ].enableAxisX;
                    else if( axisName == controllers[ cnt ].AxisNameY ) return controllers[ cnt ].enableAxisY;
                    Debug.LogError( "Axis name: " + axisName + " not found!" );
                    return false;
                }
            }
            Debug.LogError( "Controller name: " + controllerName + " not found!" );
            return false;
        }

        /// <summary>
        /// Sets the value of the joystick or touchpad axis Enable identified by controllerName & axisName.
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="axisName"></param>
        /// <param name="value"></param>
        public static void SetAxisEnable( string controllerName, string axisName, bool value )
        {
            for( int cnt = 0; cnt < controllersCount; cnt++ )
            {
                if( controllers[ cnt ].MyName == controllerName )
                {
                    if( axisName == controllers[ cnt ].AxisNameX )
                    {
                        controllers[ cnt ].enableAxisX = value;
                        return;
                    }
                    else if( axisName == controllers[ cnt ].AxisNameY )
                    {
                        controllers[ cnt ].enableAxisY = value;
                        return;
                    }
                    Debug.LogError( "Axis name: " + axisName + " not found!" );
                    return;
                }
            }
            Debug.LogError( "Controller name: " + controllerName + " not found!" );
        }


        /// <summary>
        /// Returns the value of the joystick or touchpad axis Inverse identified by controllerName & axisName.
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="axisName"></param>
        /// <returns></returns>
        public static bool GetAxisInverse( string controllerName, string axisName )
        {
            for( int cnt = 0; cnt < controllersCount; cnt++ )
            {
                if( controllers[ cnt ].MyName == controllerName )
                {
                    if( axisName == controllers[ cnt ].AxisNameX ) return controllers[ cnt ].inverseAxisX;
                    else if( axisName == controllers[ cnt ].AxisNameY ) return controllers[ cnt ].inverseAxisY;
                    Debug.LogError( "Axis name: " + axisName + " not found!" );
                    return false;
                }
            }
            Debug.LogError( "Controller name: " + controllerName + " not found!" );
            return false;
        }

        /// <summary>
        /// Sets the value of the joystick or touchpad axis Inverse identified by controllerName & axisName.
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="axisName"></param>
        /// <param name="value"></param>
        public static void SetAxisInverse( string controllerName, string axisName, bool value )
        {
            for( int cnt = 0; cnt < controllersCount; cnt++ )
            {
                if( controllers[ cnt ].MyName == controllerName )
                {
                    if( axisName == controllers[ cnt ].AxisNameX )
                    {
                        controllers[ cnt ].inverseAxisX = value;
                        return;
                    }
                    else if( axisName == controllers[ cnt ].AxisNameY )
                    {
                        controllers[ cnt ].inverseAxisY = value;
                        return;
                    }
                    Debug.LogError( "Axis name: " + axisName + " not found!" );
                    return;
                }
            }
            Debug.LogError( "Controller name: " + controllerName + " not found!" );
        }
        

        /// <summary>
        /// Returns the value of the controller Sensitivity identified by controllerName.
        /// </summary>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        public static float GetSensitivity( string controllerName )
        {
            for( int cnt = 0; cnt < controllersCount; cnt++ )
            {
                if( controllers[ cnt ].MyName == controllerName )
                {
                    return controllers[ cnt ].sensitivity;
                }
            }
            Debug.LogError( "Controller name: " + controllerName + " not found!" );
            return 0f;
        }

        /// <summary>
        /// Sets the Sensitivity value identified by controllerName.
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="value"></param>
        public static void SetSensitivity( string controllerName, float value )
        {
            for( int cnt = 0; cnt < controllersCount; cnt++ )
            {
                if( controllers[ cnt ].MyName == controllerName )
                {
                    controllers[ cnt ].sensitivity = value;
                    return;
                }
            }
            Debug.LogError( "Controller name: " + controllerName + " not found!" );
        }


        /// <summary>
        /// Showing/Hiding touch zone for all controllers in scene.
        /// </summary>
        /// <param name="value"></param>
        public static void ShowingTouchZone( bool value )
        {
            for( int cnt = 0; cnt < controllersCount; cnt++ )
            {
                controllers[ cnt ].ShowTouchZone = value;
            }
        }


        /// <summary>
        /// Returns true during the frame the user pressed down the touch button identified by buttonName.
        /// </summary>
        /// <param name="buttonName"></param>
        /// <returns></returns>
        public static bool GetButtonDown( string buttonName )
        {
            for( int cnt = 0; cnt < buttonsCount; cnt++ )
            {
                if( buttons[ cnt ].MyName == buttonName )
                {
                    return buttons[ cnt ].ButtonDOWN;
                }
            }
            Debug.LogError( "Button name: " + buttonName + " not found!" );
            return false;
        }

        /// <summary>
        /// Returns whether the given touch button is held down identified by buttonName.
        /// </summary>
        /// <param name="buttonName"></param>
        /// <returns></returns>
        public static bool GetButton( string buttonName )
        {
            for( int cnt = 0; cnt < buttonsCount; cnt++ )
            {
                if( buttons[ cnt ].MyName == buttonName )
                {
                    return buttons[ cnt ].ButtonPRESSED;
                }
            }
            Debug.LogError( "Button name: " + buttonName + " not found!" );
            return false;
        }        

        /// <summary>
        /// Returns true during the frame the user releases the given touch button identified by buttonName.
        /// </summary>
        /// <param name="buttonName"></param>
        /// <returns></returns>
        public static bool GetButtonUp( string buttonName )
        {
            for( int cnt = 0; cnt < buttonsCount; cnt++ )
            {
                if( buttons[ cnt ].MyName == buttonName )
                {
                    return buttons[ cnt ].ButtonUP;
                }
            }
            Debug.LogError( "Button name: " + buttonName + " not found!" );
            return false;
        }
    }
}