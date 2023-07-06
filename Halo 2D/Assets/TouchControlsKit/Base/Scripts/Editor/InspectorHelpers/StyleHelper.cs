/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 StyleHelper.cs               		 *
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
using UnityEditor;

namespace TouchControlsKit.Inspector
{
    public static class StyleHelper
    {
        // LabelStyle
        public static GUIStyle LabelStyle()
        {
            GUIStyle labelStyle = new GUIStyle( EditorStyles.label );
            labelStyle.fontStyle = FontStyle.Bold;
            labelStyle.alignment = TextAnchor.UpperCenter;
            labelStyle.fontSize = 13;
            labelStyle.normal.textColor = Color.red;
            return labelStyle;
        }
    }
}