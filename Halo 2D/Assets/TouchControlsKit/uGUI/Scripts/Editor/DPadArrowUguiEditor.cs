/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 DPadArrowUguiEditor.cs              *
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
using TouchControlsKit.Inspector;

namespace TouchControlsKit.Ugui.Inspector
{
    [CustomEditor( typeof( DPadArrowUgui ) )]
    public class DPadArrowUguiEditor : Editor
    {
        private DPadArrowUgui myTarget = null;


        // OnEnable
        void OnEnable()
        {
            myTarget = ( DPadArrowUgui )target;
        }

        // OnInspectorGUI
        public override void OnInspectorGUI()
        {
            // BEGIN
            GUILayout.BeginVertical( "Box", GUILayout.Width( 300 ) );
            GUILayout.Space( 5 );
            //

            ShowParameters();
            if( GUI.changed ) EditorUtility.SetDirty( myTarget );

            // END
            GUILayout.Space( 5 );
            GUILayout.EndVertical();
            //
        }

        // ShowParameters
        private void ShowParameters()
        {
            GUILayout.BeginVertical( "Box" );
            GUILayout.Label( "Parameters", StyleHelper.LabelStyle() );
            GUILayout.Space( 5 );
            
            GUILayout.BeginHorizontal();
            GUILayout.Label( "Arrow Type", GUILayout.Width( 115 ) );
            myTarget.ArrowType = ( DPadArrowBase.ArrowTypes )EditorGUILayout.EnumPopup( myTarget.ArrowType );
            GUILayout.EndHorizontal();

            GUILayout.Space( 5 );
            GUILayout.EndVertical();
        }
    }
}