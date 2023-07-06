/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 DPadUgui.cs                  		 *
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
    public class DPadUgui : DPadBase
    {
        public Data.ControllerDataUgui myData = new Data.ControllerDataUgui();

        [SerializeField]
        private Sprite normalsprite = null;
        public Sprite pressedSprite = null;

        private DPadArrowUgui[] myArrowsUgui = null;

        public Sprite normalSprite
        {
            get { return normalsprite; }
            set
            {
                if( normalsprite == value ) return;
                normalsprite = value;
                ControlAwake();
            }
        }


        // ControlDisable
        internal override void ControlDisable()
        {
            ShowTouchZone = false;
            for( int cnt = 0; cnt < myArrowsUgui.Length; cnt++ )
            {
                myArrowsUgui[ cnt ].DPadArrowADisable();
            }
        }

        // ControlAwake
        internal override void ControlAwake()
        {
            base.ControlAwake();
            myData.GetRectAndImage( gameObject );           

            myArrowsUgui = GetComponentsInChildren<DPadArrowUgui>();
            for( int cnt = 0; cnt < myArrowsUgui.Length; cnt++ )
            {
                myArrowsUgui[ cnt ].DPadArrowAwake();
                ArrowUp( cnt );
            }
        }

        // ShowingTouchZone
        protected override void ShowingTouchZone()
        {
            if( showTouchZone )
                myData.touchzoneImage.color = ElementTransparency.colorHalfSprite;
            else
                myData.touchzoneImage.color = ElementTransparency.colorZeroAll;
        }

        // CheckPosition
        internal override bool CheckTouchPosition( Vector2 touchPos )
        {
            return myData.CheckTouchPosition( touchPos );
        }

        // GetCurrentPosition
        protected override void GetCurrentPosition( Vector2 touchPos )
        {
            if( enableAxisX ) currentPosition.x = GuiCamera.ScreenToWorldPoint( touchPos ).x;
            if( enableAxisY ) currentPosition.y = GuiCamera.ScreenToWorldPoint( touchPos ).y;

            sizeX = myData.touchzoneRect.sizeDelta.x / 2f;
            sizeY = myData.touchzoneRect.sizeDelta.y / 2f;
            defaultPosition = myData.touchzoneRect.position;           
        }

        // CalculateBorderSize
        protected override void CalculateBorderSize( out float calcX, out float calcY )
        {
            calcX = myData.touchzoneRect.sizeDelta.x / 6f;
            calcY = myData.touchzoneRect.sizeDelta.y / 6f;
        }

        // ArrowDown
        protected override void ArrowDown( int index )
        {
            if( myArrowsUgui[ index ].myData.touchzoneImage.sprite != pressedSprite )
                myArrowsUgui[ index ].myData.touchzoneImage.sprite = pressedSprite;
        }

        // ArrowUp
        protected override void ArrowUp( int index )
        {
            if( myArrowsUgui[ index ].myData.touchzoneImage.sprite != normalSprite )
                myArrowsUgui[ index ].myData.touchzoneImage.sprite = normalSprite;
        }    
    }
}