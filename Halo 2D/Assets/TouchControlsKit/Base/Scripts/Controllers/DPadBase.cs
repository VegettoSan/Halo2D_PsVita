/***********************************************************
 * 														   *
 * Asset:		 Touch Controls Kit     			       *
 * Script:		 DPadBase.cs      	           		       *
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
    public abstract class DPadBase : ControllerBase
    {
        protected DPadArrowBase[] myArrows = null;
        protected Vector2 borderPosition = Vector2.zero;

        protected float sizeX, sizeY;

        //
        private const string nativeStartMethodName = "OnDPadStart";
        private const string nativeMoveMethodName = "OnDPadMove";
        private const string nativeEndMethodName = "OnDPadEnd";


#if UNITY_EDITOR
        // for Editor
        public override string GetNativeNames()
        {
            return "Native Start:  " + nativeStartMethodName +
                   "\nNative Move: " + nativeMoveMethodName +
                   "\nNative End:    " + nativeEndMethodName;
        }
#endif

        // ControlAwake
        internal override void ControlAwake()
        {
            base.ControlAwake();
            myArrows = GetComponentsInChildren<DPadArrowBase>();
            for( int cnt = 0; cnt < myArrows.Length; cnt++ )
                myArrows[ cnt ].DPadArrowAwake();
        }

        
        // UpdatePosition
        internal override void UpdatePosition( Vector2 touchPos )
        {
            if( !enableAxisX && !enableAxisY ) return;

            if( touchDown )
            {
                GetCurrentPosition( touchPos );

                currentDirection = currentPosition - defaultPosition;

                float borderSizeX = 0f;
                float borderSizeY = 0f;

                CalculateBorderSize( out borderSizeX, out borderSizeY );

                borderPosition = defaultPosition;
                borderPosition.x += currentDirection.normalized.x * borderSizeX;
                borderPosition.y += currentDirection.normalized.y * borderSizeY;

                float currentDistance = Vector2.Distance( defaultPosition, currentPosition );

                if( currentDistance > borderSizeX || currentDistance > borderSizeY ) currentPosition = borderPosition; //Debug.DrawLine( defaultPosition, currentPosition );

                float asisX = 0f;
                float asisY = 0f;

                for( int cnt = 0; cnt < myArrows.Length; cnt++ )
                {
                    if( myArrows[ cnt ].CheckTouchPosition( currentPosition, sizeX, sizeY ) )
                    {
                        ArrowDown( cnt );

                        if( myArrows[ cnt ].ArrowType == DPadArrowBase.ArrowTypes.UP
                            || myArrows[ cnt ].ArrowType == DPadArrowBase.ArrowTypes.DOWN )
                            asisY = myArrows[ cnt ].INDEX * sensitivity;

                        if( myArrows[ cnt ].ArrowType == DPadArrowBase.ArrowTypes.RIGHT
                            || myArrows[ cnt ].ArrowType == DPadArrowBase.ArrowTypes.LEFT )
                            asisX = myArrows[ cnt ].INDEX * sensitivity;
                    }
                    else
                    {
                        ArrowUp( cnt );

                        if( myArrows[ cnt ].ArrowType == DPadArrowBase.ArrowTypes.UP && myArrows[ cnt ].INDEX == 0f )
                        {
                            for( int dnt = 0; dnt < myArrows.Length; dnt++ )
                                if( myArrows[ dnt ].ArrowType == DPadArrowBase.ArrowTypes.DOWN && myArrows[ dnt ].INDEX == 0f )
                                    asisY = myArrows[ dnt ].INDEX * sensitivity;
                        }

                        if( myArrows[ cnt ].ArrowType == DPadArrowBase.ArrowTypes.RIGHT && myArrows[ cnt ].INDEX == 0f )
                        {
                            for( int dnt = 0; dnt < myArrows.Length; dnt++ )
                                if( myArrows[ dnt ].ArrowType == DPadArrowBase.ArrowTypes.LEFT && myArrows[ dnt ].INDEX == 0f )
                                    asisX = myArrows[ dnt ].INDEX * sensitivity;
                        }
                    }
                }

                if( inverseAxisX ) asisX = -asisX;
                if( inverseAxisY ) asisY = -asisY;

                SetAxis( asisX, asisY );

                // Broadcasting
                PressHandler( nativeMoveMethodName, controllerData );
            }
            else
            {
                touchDown = true;

                // Broadcasting
                DownHandler( nativeStartMethodName, controllerData );                
            }
        }

        // GetCurrentPosition
        protected abstract void GetCurrentPosition( Vector2 touchPos );

        // CalculateBorderSize
        protected abstract void CalculateBorderSize( out float calcX, out float calcY );

        // ArrowDown
        protected abstract void ArrowDown( int index );

        // ArrowUp
        protected abstract void ArrowUp( int index );
                
        // ControlReset
        internal override void ControlReset()
        {
            base.ControlReset();

            for( int cnt = 0; cnt < myArrows.Length; cnt++ ) 
                ArrowUp( cnt );

            // Broadcasting
            UpHandler( nativeEndMethodName, controllerData );
        }
    }
}