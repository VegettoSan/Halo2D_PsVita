/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 GuiCamera.cs               		 *
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

namespace TouchControlsKit.Utils
{
    [RequireComponent( typeof( Camera ) )]
    public sealed class GuiCamera : MonoBehaviour
    {
        private static Camera myCamera = null;
        private static Transform myTransform = null;
        
        // guiCamera
        public static Camera guiCamera
        {
            get
            {
                if( !myCamera )
                    myCamera = FindObjectOfType<GuiCamera>().GetComponent<Camera>();
                
               return myCamera;
            }
        }

        // guiCameraTransform
        public static Transform guiCameraTransform
        {
            get
            {
                if( !myTransform )
                    myTransform = FindObjectOfType<GuiCamera>().transform;

                return myTransform;
            }
        }                    

        // ScreenToWorldPoint
        public static Vector2 ScreenToWorldPoint( Vector2 pos )
        {
            return guiCamera.ScreenToWorldPoint( pos );
        }


#if UNITY_EDITOR
        // CreateCamera
        public static void CreateCamera( Transform parent, int cullingMask, float orthographicSize )
        {
            GameObject camObj = new GameObject( "_guiCamera", typeof( GuiCamera ) );
            camObj.transform.parent = parent;
            camObj.transform.localPosition = Vector3.zero;

            Camera cam = camObj.GetComponent<Camera>();
            cam.clearFlags = CameraClearFlags.Depth;
            cam.cullingMask = cullingMask;
            cam.orthographic = true;
            cam.orthographicSize = orthographicSize;
            cam.nearClipPlane = -0.25f;
            cam.farClipPlane = 0.25f;
            cam.depth = 1;
            cam.useOcclusionCulling = false;
            cam.allowHDR = false;
        }
#endif
    }
}