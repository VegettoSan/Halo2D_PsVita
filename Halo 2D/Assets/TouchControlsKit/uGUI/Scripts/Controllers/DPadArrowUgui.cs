/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 DPadArrowUgui.cs               	 *
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
    public class DPadArrowUgui : DPadArrowBase
    {
        public Data.ControllerDataUgui myData = new Data.ControllerDataUgui();


        // DPadArrowAwake
        internal override void DPadArrowAwake()
        {
            myData.GetRectAndImage( gameObject );
            myData.touchzoneImage.color = ElementTransparency.colorHalfSprite;
        }

        // DPadArrowADisable
        internal override void DPadArrowADisable()
        {
            myData.touchzoneImage.color = ElementTransparency.colorZeroAll;
        }

        // CheckBoolPosition
        protected override bool CheckBoolPosition( Vector2 touchPos, float sizeX, float sizeY )
        {
            float halfSizeX = myData.touchzoneRect.sizeDelta.x / 2f;
            float halfSizeY = myData.touchzoneRect.sizeDelta.x / 2f;

            switch( ArrowType )
            {
                case ArrowTypes.UP:
                case ArrowTypes.DOWN:
                    if( touchPos.x < myData.touchzoneRect.position.x + sizeX / 2f
                    && touchPos.y < myData.touchzoneRect.position.y + halfSizeY / 2f //maxY
                    && touchPos.x > myData.touchzoneRect.position.x - sizeX / 2f
                    && touchPos.y > myData.touchzoneRect.position.y - halfSizeY / 2f ) // minY
                    {
                        return true;
                    }
                    break;

                case ArrowTypes.RIGHT:
                case ArrowTypes.LEFT:
                    if( touchPos.x < myData.touchzoneRect.position.x + halfSizeX / 2f //maxX
                    && touchPos.y < myData.touchzoneRect.position.y + sizeY / 2f
                    && touchPos.x > myData.touchzoneRect.position.x - halfSizeX / 2f //minX
                    && touchPos.y > myData.touchzoneRect.position.y - sizeY / 2f )
                    {
                        return true;
                    }
                    break;           
            }
            return false;
        }
        //
    }
}

/*
Debug.DrawLine( new Vector2( myData.touchzoneRect.position.x + sizeX / 2f, myData.touchzoneRect.position.y + halfSizeY / 2f ),
                new Vector2( myData.touchzoneRect.position.x - sizeX / 2f, myData.touchzoneRect.position.y - halfSizeY / 2f ), Color.red );
 
Debug.DrawLine( new Vector2( myData.touchzoneRect.position.x + halfSizeX / 2f, myData.touchzoneRect.position.y + sizeY / 2f ),
                new Vector2( myData.touchzoneRect.position.x - halfSizeX / 2f, myData.touchzoneRect.position.y - sizeY / 2f ), Color.green );
*/