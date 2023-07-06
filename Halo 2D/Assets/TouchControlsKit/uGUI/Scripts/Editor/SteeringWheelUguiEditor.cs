/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 SteeringWheelUguiEditor.cs          *
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
    [CustomEditor( typeof( SteeringWheelUgui ) )]
    public class SteeringWheelUguiEditor : Editor
    {
        private SteeringWheelUgui myTarget = null;


        // OnEnable
        void OnEnable()
        {
            myTarget = ( SteeringWheelUgui )target;

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
            GUILayout.Label( "Max Steering Angle", GUILayout.Width( size ) );
            myTarget.maxSteeringAngle = EditorGUILayout.Slider( myTarget.maxSteeringAngle, 36f, 360f );
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label( "Released Speed", GUILayout.Width( size ) );
            myTarget.releasedSpeed = EditorGUILayout.Slider( myTarget.releasedSpeed, 25f, 150f );
            GUILayout.EndHorizontal();

            GUILayout.Space( 5 );

            GUILayout.BeginHorizontal();
            GUILayout.Label( "Wheel Name", GUILayout.Width( size ) );
            myTarget.MyName = EditorGUILayout.TextField( myTarget.MyName );
            GUILayout.EndHorizontal();

            GUILayout.Space( 5 );

            GUILayout.BeginHorizontal();
            GUILayout.Label( "Steering Wheel", GUILayout.Width( size ) );
            myTarget.myData.touchzoneImage.sprite = EditorGUILayout.ObjectField( myTarget.myData.touchzoneImage.sprite, typeof( Sprite ), false ) as Sprite;
            GUILayout.EndHorizontal();

            GUILayout.Space( 5 );
            GUILayout.EndVertical();

            GUILayout.Space( 5 );
            AxesHelper.ShowAxes( size, true );
            GUILayout.Space( 5 );
            EventsHelper.ShowEvents( size );
        }
    }
}