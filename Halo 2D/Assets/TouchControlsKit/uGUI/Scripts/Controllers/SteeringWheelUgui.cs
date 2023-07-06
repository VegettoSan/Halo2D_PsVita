/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 SteeringWheelUgui.cs                *
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
    public class SteeringWheelUgui : SteeringWheelBase
    {
        public Data.ControllerDataUgui myData = new Data.ControllerDataUgui();


        // ControlDisable
        internal override void ControlDisable()
        {
            myData.touchzoneImage.color = ElementTransparency.colorZeroAll;
        }

        // ControlAwake
        internal override void ControlAwake()
        {
            base.ControlAwake();
            myData.GetRectAndImage( gameObject );
            myData.touchzoneImage.color = ElementTransparency.colorHalfSprite;
        }

        // CheckTouchPosition
        internal override bool CheckTouchPosition( Vector2 touchPos )
        {
            return myData.CheckTouchPosition( touchPos );
        }

        // GetCurrentPosition
        protected override void GetCurrentPosition( Vector2 touchPos )
        {
            defaultPosition = currentPosition = myData.touchzoneRect.position;
            currentPosition.x = GuiCamera.ScreenToWorldPoint( touchPos ).x;
            currentPosition.y = GuiCamera.ScreenToWorldPoint( touchPos ).y;
        }

        // UptateWheelRotation
        protected override void UptateWheelRotation()
        {
            base.UptateWheelRotation();
            myData.touchzoneRect.localEulerAngles = localEulerAngles;             
        }
    }
}