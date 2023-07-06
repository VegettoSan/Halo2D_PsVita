/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 ControllerDataUgui.cs        		 *
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

namespace TouchControlsKit.Ugui.Data
{
    /// <summary>
    /// Contains second data, needed for the controller.
    /// </summary>
    [System.Serializable]
    public sealed class ControllerDataUgui
    {
        public RectTransform touchzoneRect = null;
        public Image touchzoneImage = null;
        

        // GetRectAndImage
        internal void GetRectAndImage( GameObject gameObject )
        {
            touchzoneRect = gameObject.GetComponent<RectTransform>();
            touchzoneImage = gameObject.GetComponent<Image>();
        }

        // CheckTouchPosition
        internal bool CheckTouchPosition( Vector2 touchPos )
        {
            touchPos = GuiCamera.ScreenToWorldPoint( touchPos );

            if( touchPos.x < touchzoneRect.position.x + touchzoneRect.sizeDelta.x / 4.5f
                && touchPos.y < touchzoneRect.position.y + touchzoneRect.sizeDelta.y / 4.5f
                && touchPos.x > touchzoneRect.position.x - touchzoneRect.sizeDelta.x / 4.5f
                && touchPos.y > touchzoneRect.position.y - touchzoneRect.sizeDelta.y / 4.5f )
            {
                return true;
            }
            else return false;
        }
    }
}