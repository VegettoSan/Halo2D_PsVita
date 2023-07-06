/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 JoystickUguiEditor.cs               *
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
    [CustomEditor( typeof( JoystickUgui ) )]
    public class JoystickUguiEditor : Editor
    {
        private JoystickUgui myTarget = null;
        private static string[] modNames = { "Dynamic", "Static" };


        // OnEnable
        void OnEnable()
        {
            myTarget = ( JoystickUgui )target;

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
            GUILayout.Label( "Mode", GUILayout.Width( size ) );
            myTarget.IsStatic = System.Convert.ToBoolean( GUILayout.Toolbar( System.Convert.ToInt32( myTarget.IsStatic ), modNames, GUILayout.Height( 20 ) ) );
            GUILayout.EndHorizontal();

            GUILayout.Space( 5 );

            GUILayout.BeginHorizontal();
            GUILayout.Label( "Sensitivity", GUILayout.Width( size ) );
            myTarget.sensitivity = EditorGUILayout.Slider( myTarget.sensitivity, 0.1f, 10f );
            GUILayout.EndHorizontal();

            GUILayout.Space( 5 );

            GUILayout.BeginHorizontal();
            GUILayout.Label( "Border Size", GUILayout.Width( size ) );
            myTarget.borderSize = EditorGUILayout.Slider( myTarget.borderSize, 1f, 9f );
            GUILayout.EndHorizontal();

            GUILayout.Space( 5 );

            if( myTarget.IsStatic )
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label( "Smooth Return", GUILayout.Width( size ) );
                myTarget.smoothReturn = EditorGUILayout.Toggle( myTarget.smoothReturn );
                GUILayout.EndHorizontal();

                if( myTarget.smoothReturn )
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space( 15 );
                    GUILayout.Label( "Smooth Factor", GUILayout.Width( size ) );
                    myTarget.smoothFactor = EditorGUILayout.Slider( myTarget.smoothFactor, 1f, 20f );
                    GUILayout.EndHorizontal();
                }
            }

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
            GUILayout.Label( "Joystick Name", GUILayout.Width( size ) );
            myTarget.MyName = EditorGUILayout.TextField( myTarget.MyName );
            GUILayout.EndHorizontal();

            GUILayout.Space( 5 );

            GUILayout.BeginHorizontal();
            GUILayout.Label( "         Joystick" );
            GUILayout.Label( "         Background" );
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            myTarget.joystickImage.sprite = EditorGUILayout.ObjectField( myTarget.joystickImage.sprite, typeof( Sprite ), false ) as Sprite;
            myTarget.joystickBackgroundImage.sprite = EditorGUILayout.ObjectField( myTarget.joystickBackgroundImage.sprite, typeof( Sprite ), false ) as Sprite;
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