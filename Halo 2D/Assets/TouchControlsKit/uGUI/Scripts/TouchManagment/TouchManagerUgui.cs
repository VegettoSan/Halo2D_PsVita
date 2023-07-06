/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 TouchManagerUgui.cs                 *
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

namespace TouchControlsKit.Ugui
{
    public class TouchManagerUgui : TouchManagerBase
    {
        // Use this for initialization
        void Awake()
        {
            TouchManagerSetup();
        }

        // Update is called once per frame
        void Update()
        {
            FinalUpdate( Input.touchCount );
        }
    }
}