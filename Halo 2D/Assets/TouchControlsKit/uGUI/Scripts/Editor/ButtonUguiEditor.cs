/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 ButtonUguiEditor.cs               	 *
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
    [CustomEditor( typeof( ButtonUgui ) )]
    public class ButtonUguiEditor : Editor
    {
        private ButtonUgui myTarget = null;


        // OnEnable
        void OnEnable()
        {
            myTarget = ( ButtonUgui )target;
            EventsHelper.HelperSetup( myTarget );
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
            GUILayout.Label( "Button Name", GUILayout.Width( 115 ) );
            myTarget.MyName = EditorGUILayout.TextField( myTarget.MyName );
            GUILayout.EndHorizontal();

            GUILayout.Space( 5 );

            GUILayout.BeginHorizontal();
            GUILayout.Label( "         Normal" );
            GUILayout.Label( "        Pressed" );
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            myTarget.normalSprite = EditorGUILayout.ObjectField( myTarget.normalSprite, typeof( Sprite ), false ) as Sprite;
            myTarget.pressedSprite = EditorGUILayout.ObjectField( myTarget.pressedSprite, typeof( Sprite ), false ) as Sprite;
            GUILayout.EndHorizontal();

            GUILayout.Space( 5 );
            GUILayout.EndVertical();

            GUILayout.Space( 5 );

            EventsHelper.ShowEvents( 115 );
        }
    }
}