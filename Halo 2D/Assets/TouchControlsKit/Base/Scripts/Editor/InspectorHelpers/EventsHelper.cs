/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 EventsHelper.cs               		 *
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
    public static class EventsHelper
    {
        private static string[] stateNames = { "OFF", "ON" };
        private static string[] methodNames = { "Native", "Custom" };

        private static Texture receiverEnable = null;
        private static Texture receiverDisable = null;
        private static Texture receiverRemove = null;

        private static ControllerBase myTarget = null;


        // HelperSetup
        public static void HelperSetup( ControllerBase target )
        {
            myTarget = target;

            receiverEnable = AssetDatabase.LoadAssetAtPath<Texture>( "Assets/TouchControlsKit/Base/Resources/icons/receiverEnable.png" );
            receiverDisable = AssetDatabase.LoadAssetAtPath<Texture>( "Assets/TouchControlsKit/Base/Resources/icons/receiverDisable.png" );
            receiverRemove = AssetDatabase.LoadAssetAtPath<Texture>( "Assets/TouchControlsKit/Base/Resources/icons/receiverRemove.png" );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myTarget"></param>
        /// <param name="size"></param>
        public static void ShowEvents( int size )
        {
            GUILayout.BeginVertical( "Box" );
            GUILayout.Label( "Events", StyleHelper.LabelStyle() );
            GUILayout.Space( 5 );

            GUILayout.BeginHorizontal();
            GUILayout.Label( "Broadcasting", GUILayout.Width( size ) );
            myTarget.broadcastMessages = System.Convert.ToBoolean( GUILayout.Toolbar( System.Convert.ToInt32( myTarget.broadcastMessages ), stateNames, GUILayout.Height( 15 ) ) );
            GUILayout.EndHorizontal();

            GUILayout.Space( 5 );

            if( myTarget.broadcastMessages )
            {
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if( GUILayout.Button( "+ Add Receiver", GUILayout.Width( 110 ), GUILayout.Height( 20 ) ) || myTarget.receivers.Count == 0 )
                {
                    myTarget.receivers.Add( new Receiver() );
                }
                GUILayout.EndHorizontal();

                GUILayout.Space( 2 );

                for( int cnt = 0; cnt < myTarget.receivers.Count; cnt++ )
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space( 15 );

                    if( myTarget.receivers[ cnt ].enabled )
                    {
                        if( GUILayout.Button( receiverEnable, GUILayout.Width( 23 ), GUILayout.Height( 16 ) ) )
                            myTarget.receivers[ cnt ].enabled = !myTarget.receivers[ cnt ].enabled;
                    }
                    else
                    {
                        if( GUILayout.Button( receiverDisable, GUILayout.Width( 23 ), GUILayout.Height( 16 ) ) )
                            myTarget.receivers[ cnt ].enabled = !myTarget.receivers[ cnt ].enabled;
                    }

                    if( cnt == 0 ) GUILayout.Label( "Receiver ", GUILayout.Width( 88 ) );
                    else if( cnt > 0 ) GUILayout.Label( "Receiver " + ( cnt + 1 ).ToString(), GUILayout.Width( 88 ) );
                    myTarget.receivers[ cnt ].receiver = EditorGUILayout.ObjectField( myTarget.receivers[ cnt ].receiver, typeof( GameObject ), true ) as GameObject;

                    if( cnt > 0 )
                        if( GUILayout.Button( receiverRemove, GUILayout.Width( 23 ), GUILayout.Height( 16 ) ) )
                        {
                            myTarget.receivers.RemoveAt( cnt );
                        }

                    GUILayout.EndHorizontal();
                }

                GUILayout.Space( 5 );

                GUILayout.BeginHorizontal();
                GUILayout.Space( 15 );
                GUILayout.Label( "Sending Mode", GUILayout.Width( size ) );
                myTarget.sendingMode = ( SendingModes )EditorGUILayout.EnumPopup( myTarget.sendingMode );
                GUILayout.EndHorizontal();

                GUILayout.Space( 5 );

                GUILayout.BeginHorizontal();
                GUILayout.Space( 15 );
                GUILayout.Label( "Methods", GUILayout.Width( size ) );
                myTarget.customMethods = System.Convert.ToBoolean( GUILayout.Toolbar( System.Convert.ToInt32( myTarget.customMethods ), methodNames, GUILayout.Height( 15 ) ) );
                GUILayout.EndHorizontal();

                GUILayout.Space( 5 );

                if( myTarget.customMethods )
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space( 25 );
                    GUILayout.Label( "Down Method Name", GUILayout.Width( 125 ) );
                    myTarget.downMethodName = EditorGUILayout.TextField( myTarget.downMethodName );
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Space( 25 );
                    GUILayout.Label( "Press Method Name", GUILayout.Width( 125 ) );
                    myTarget.pressMethodName = EditorGUILayout.TextField( myTarget.pressMethodName );
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Space( 25 );
                    GUILayout.Label( "Up Method Name", GUILayout.Width( 125 ) );
                    myTarget.upMethodName = EditorGUILayout.TextField( myTarget.upMethodName );
                    GUILayout.EndHorizontal();
                }
                else
                {
                    EditorGUILayout.HelpBox( myTarget.GetNativeNames(), MessageType.Info );
                }
            }

            GUILayout.Space( 5 );
            GUILayout.EndVertical();
        }
    }
}