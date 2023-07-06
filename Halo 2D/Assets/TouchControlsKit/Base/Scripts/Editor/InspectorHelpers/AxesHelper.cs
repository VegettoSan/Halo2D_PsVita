/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 AxesHelper.cs               		 *
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
    public static class AxesHelper
    {
        private static string[] stateNames = { "OFF", "ON" };
        private static ControllerBase myTarget = null;


        // HelperSetup
        public static void HelperSetup( ControllerBase target )
        {
            myTarget = target;
        }

        /// <summary>
        /// ShowAxes
        /// </summary>
        /// <param name="size"></param>
        /// <param name="hideVert"></param>
        public static void ShowAxes( int size, bool hideVert = false )
        {
            GUILayout.BeginVertical( "Box" );
            GUILayout.Label( "Axes", StyleHelper.LabelStyle() );
            GUILayout.Space( 5 );

            GUILayout.BeginHorizontal();
            GUILayout.Label( "Enable Axis X", GUILayout.Width( size ) );
            myTarget.enableAxisX = System.Convert.ToBoolean( GUILayout.Toolbar( System.Convert.ToInt32( myTarget.enableAxisX ), stateNames, GUILayout.Height( 15 ) ) );
            GUILayout.EndHorizontal();

            if( myTarget.enableAxisX )
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space( 25 );
                GUILayout.Label( "Inverse", GUILayout.Width( size ) );
                myTarget.inverseAxisX = EditorGUILayout.Toggle( myTarget.inverseAxisX );
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space( 25 );
                GUILayout.Label( "Axis Name", GUILayout.Width( size ) );
                myTarget.AxisNameX = EditorGUILayout.TextField( myTarget.AxisNameX );
                GUILayout.EndHorizontal();

                GUILayout.Space( 5 );
            }


            if( hideVert )
            {
                GUILayout.Space( 5 );
                GUILayout.EndVertical();
                return;
            }


            GUILayout.BeginHorizontal();
            GUILayout.Label( "Enable Axis Y", GUILayout.Width( size ) );
            myTarget.enableAxisY = System.Convert.ToBoolean( GUILayout.Toolbar( System.Convert.ToInt32( myTarget.enableAxisY ), stateNames, GUILayout.Height( 15 ) ) );
            GUILayout.EndHorizontal();

            if( myTarget.enableAxisY )
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space( 25 );
                GUILayout.Label( "Inverse", GUILayout.Width( size ) );
                myTarget.inverseAxisY = EditorGUILayout.Toggle( myTarget.inverseAxisY );
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space( 25 );
                GUILayout.Label( "Axis Name", GUILayout.Width( size ) );
                myTarget.AxisNameY = EditorGUILayout.TextField( myTarget.AxisNameY );
                GUILayout.EndHorizontal();
            }

            GUILayout.Space( 5 );
            GUILayout.EndVertical();
        }        
    }
}