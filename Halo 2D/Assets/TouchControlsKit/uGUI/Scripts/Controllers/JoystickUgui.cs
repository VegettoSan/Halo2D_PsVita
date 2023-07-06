/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 JoystickUgui.cs               		 *
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
using UnityEngine.UI;
using TouchControlsKit.Utils;

namespace TouchControlsKit.Ugui
{
    [RequireComponent( typeof( Image ) )]
    public class JoystickUgui : JoystickBase
    {
        public Data.ControllerDataUgui myData = new Data.ControllerDataUgui();

        public Image joystickImage = null;
        public Image joystickBackgroundImage = null;

        public RectTransform joystickRT = null;
        public RectTransform joystickBackgroundRT = null;


        // ControlDisable
        internal override void ControlDisable()
        {
            ShowTouchZone = false;
            joystickImage.color = joystickBackgroundImage.color = ElementTransparency.colorZeroAll;
        }

        // ControlAwake
        internal override void ControlAwake()
        {
            base.ControlAwake();
            myData.GetRectAndImage( gameObject );
            SetTransparency();            
        }

        // ShowingTouchZone
        protected override void ShowingTouchZone()
        {
            if( showTouchZone )
                myData.touchzoneImage.color = ElementTransparency.colorHalfSprite;
            else
                myData.touchzoneImage.color = ElementTransparency.colorZeroAll;
        }

        // ElementTransparency
        private void SetTransparency()
        {
            if( isStatic || showTouchZone )
                joystickImage.color = joystickBackgroundImage.color = ElementTransparency.colorHalfSprite;            
            else
                joystickImage.color = joystickBackgroundImage.color = ElementTransparency.colorZeroAll;            
        }

        // CheckPosition
        internal override bool CheckTouchPosition( Vector2 touchPos )
        {
            return myData.CheckTouchPosition( touchPos );
        }

        // GetCurrentPosition
        protected override void GetCurrentPosition( Vector2 touchPos )
        {
            defaultPosition = currentPosition = joystickBackgroundRT.position;
            if( enableAxisX ) currentPosition.x = GuiCamera.ScreenToWorldPoint( touchPos ).x;
            if( enableAxisY ) currentPosition.y = GuiCamera.ScreenToWorldPoint( touchPos ).y;
        }

        // CalculateBorderSize
        protected override float CalculateBorderSize()
        { 
           return ( joystickBackgroundRT.sizeDelta.magnitude / 2f ) * borderSize / 8f;
        }

        // UpdateJoystickPosition
        protected override void UpdateJoystickPosition()
        {
            joystickRT.position = currentPosition;
        }

        //Update Transparency And Position for Dynamic Joystick
        protected override void UpdateTransparencyAndPosition( Vector2 touchPos )
        {
            joystickImage.color = joystickBackgroundImage.color = ElementTransparency.colorHalfSprite;
            joystickRT.position = joystickBackgroundRT.position = GuiCamera.ScreenToWorldPoint( touchPos );
        }

        // ControlReset
        internal override void ControlReset()
        {
            base.ControlReset();
            SetTransparency();
        }
    }
}