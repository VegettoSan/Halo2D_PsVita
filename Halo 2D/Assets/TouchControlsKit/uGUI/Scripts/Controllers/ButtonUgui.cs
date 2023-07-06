/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 ButtonUgui.cs               		 *
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
    public class ButtonUgui : ButtonBase
    {
        public Data.ControllerDataUgui myData = new Data.ControllerDataUgui();

        [SerializeField]
        private Sprite normalsprite = null;
        public Sprite pressedSprite = null;

        public Sprite normalSprite
        {
            get { return normalsprite; }
            set
            {
                if( normalsprite == value ) return;
                normalsprite = value;
                ControlAwake();
                myData.touchzoneImage.sprite = normalsprite;
            }
        }


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

        // ButtonDown
        protected override void ButtonDown()
        {
            myData.touchzoneImage.sprite = pressedSprite;
        }

        // ButtonUp
        protected override void ButtonUp()
        {
            myData.touchzoneImage.sprite = normalSprite;
        }
    }
}