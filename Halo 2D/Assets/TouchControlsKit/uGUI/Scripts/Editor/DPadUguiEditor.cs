/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 DPadUguiEditor.cs                   *
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
    [CustomEditor( typeof( DPadUgui ) )]
    public class DPadUguiEditor : Editor
    {
        private DPadUgui myTarget = null;


        // OnEnable
        void OnEnable()
        {
            myTarget = ( DPadUgui )target;

            AxesHelper.HelperSetup( myTarget );
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
            const int size = 115;

            GUILayout.BeginVertical( "Box" );
            GUILayout.Label( "Parameters", StyleHelper.LabelStyle() );
            GUILayout.Space( 5 );

            GUILayout.BeginHorizontal();
            GUILayout.Label( "Sensitivity", GUILayout.Width( size ) );
            myTarget.sensitivity = EditorGUILayout.Slider( myTarget.sensitivity, 1f, 10f );
            GUILayout.EndHorizontal();

            GUILayout.Space( 5 );

            GUILayout.BeginHorizontal();
            GUILayout.Label( "Show TouchZone", GUILayout.Width( size ) );
            myTarget.ShowTouchZone = EditorGUILayout.Toggle( myTarget.ShowTouchZone );
            GUILayout.EndHorizontal();

            if( myTarget.ShowTouchZone )
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space( 15 );
                GUILayout.Label( "TouchZone Sprite", GUILayout.Width( size ) );
                myTarget.myData.touchzoneImage.sprite = EditorGUILayout.ObjectField( myTarget.myData.touchzoneImage.sprite, typeof( Sprite ), false ) as Sprite;
                GUILayout.EndHorizontal();
            }

            GUILayout.Space( 5 );

            GUILayout.BeginHorizontal();
            GUILayout.Label( "DPad Name", GUILayout.Width( size ) );
            myTarget.MyName = EditorGUILayout.TextField( myTarget.MyName );
            GUILayout.EndHorizontal();

            GUILayout.Space( 5 );

            GUILayout.BeginHorizontal();
            GUILayout.Label( "    Arrow Normal" );
            GUILayout.Label( "     Arrow Pressed" );
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            myTarget.normalSprite = EditorGUILayout.ObjectField( myTarget.normalSprite, typeof( Sprite ), false ) as Sprite;
            myTarget.pressedSprite = EditorGUILayout.ObjectField( myTarget.pressedSprite, typeof( Sprite ), false ) as Sprite;
            GUILayout.EndHorizontal();

            GUILayout.Space( 5 );
            GUILayout.EndVertical();

            GUILayout.Space( 5 );
            AxesHelper.ShowAxes( size );
            GUILayout.Space( 5 );
            EventsHelper.ShowEvents( size ); 
        }
    }
}