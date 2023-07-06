/***********************************************************
 * 														   *
 * Asset:		 Touch Controls Kit     			       *
 * Script:		 DPadArrowBase.cs      	           		   *
 * 														   *
 * Copyright(c): Victor Klepikov						   *
 * Support: 	 http://bit.ly/vk-Support				   *
 * 														   *
 * mySite:       http://vkdemos.ucoz.org				   *
 * myAssets:     http://bit.ly/VictorKlepikovUnityAssets   *
 * myTwitter:	 http://twitter.com/VictorKlepikov		   *
 * myFacebook:	 http://www.facebook.com/vikle4 		   *
 * 														   *
 ***********************************************************/


using UnityEngine;

namespace TouchControlsKit
{
    public abstract class DPadArrowBase : MonoBehaviour
    {
        public enum ArrowTypes { none, UP, DOWN, LEFT, RIGHT }
        public ArrowTypes ArrowType = ArrowTypes.none;

        internal float INDEX { get; private set; }


        // DPadArrowAwake
        internal abstract void DPadArrowAwake();

        // DPadArrowADisable
        internal abstract void DPadArrowADisable();

        // CheckBoolPosition
        internal bool CheckTouchPosition( Vector2 touchPos, float sizeX, float sizeY )
        {
            if( CheckBoolPosition( touchPos, sizeX, sizeY ) )
            {
                switch( ArrowType )
                {
                    case ArrowTypes.UP:
                    case ArrowTypes.RIGHT: INDEX = 1f;
                        break;

                    case ArrowTypes.DOWN:
                    case ArrowTypes.LEFT: INDEX = -1f;
                        break;

                    case ArrowTypes.none:                        
                        Debug.LogError( "ERROR: Arrow type " + gameObject.name + " is not assigned!" );
                        INDEX = 0f;
                        return false;
                }
                return true;
            }
            else
            {
                INDEX = 0f;
                return false;
            }
        }

        // CheckBoolPosition
        protected abstract bool CheckBoolPosition( Vector2 touchPos, float sizeX, float sizeY );
    }
}