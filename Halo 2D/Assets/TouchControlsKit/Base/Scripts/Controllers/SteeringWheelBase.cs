


using UnityEngine;

namespace TouchControlsKit
{
    public abstract class SteeringWheelBase : ControllerBase
    {
        public float maxSteeringAngle = 120f;
        public float releasedSpeed = 45f;

        protected Vector3 localEulerAngles = Vector3.zero;

        private float wheelAngle, wheelPrevAngle;

        //
        private const string nativeStartMethodName = "OnWheelStart";
        private const string nativeMoveMethodName = "OnWheelMove";
        private const string nativeEndMethodName = "OnWheelEnd";

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
        }


        // UpdatePosition
        internal override void UpdatePosition( Vector2 touchPos )
        {
            GetCurrentPosition( touchPos );

            if( touchDown )
            {
                float wheelNewAngle = Vector2.Angle( Vector2.up, currentPosition - defaultPosition );

                if( currentPosition.x > defaultPosition.x )
                    wheelAngle += wheelNewAngle - wheelPrevAngle;
                else
                    wheelAngle -= wheelNewAngle - wheelPrevAngle;

                wheelAngle = Mathf.Clamp( wheelAngle, -maxSteeringAngle, maxSteeringAngle );
                wheelPrevAngle = wheelNewAngle;

                UptateWheelRotation();

                float asisX = wheelAngle / maxSteeringAngle * sensitivity;

                if( inverseAxisX ) asisX = -asisX;

                SetAxis( asisX, 0f );


                // Broadcasting
                PressHandler( nativeMoveMethodName, controllerData );
            }
            else
            {
                touchDown = true;
                wheelPrevAngle = Vector2.Angle( Vector2.up, currentPosition - defaultPosition );

                // Broadcasting
                DownHandler( nativeStartMethodName, controllerData );
            }
        }

        // GetCurrentPosition
        protected abstract void GetCurrentPosition( Vector2 touchPos );

        // UptateWheelRotation
        protected virtual void UptateWheelRotation()
        {
            localEulerAngles = Vector3.back * wheelAngle;
        }


        internal override void ControlReset()
        {
            base.ControlReset();

            StopCoroutine( "WheelReturnRun" );
            StartCoroutine( "WheelReturnRun" );

            // Broadcasting
            UpHandler( nativeEndMethodName, controllerData );
        } 

        // WheelReturnRun
        private System.Collections.IEnumerator WheelReturnRun()
        {
            while( !Mathf.Approximately( 0f, wheelAngle ) )
            {
                float deltaAngle = releasedSpeed * Time.smoothDeltaTime * 10f;

                if( Mathf.Abs( deltaAngle ) > Mathf.Abs( wheelAngle ) )                
                    wheelAngle = 0f;                
                else if( wheelAngle > 0f )                
                    wheelAngle -= deltaAngle;                
                else                
                    wheelAngle += deltaAngle;

                UptateWheelRotation();

                yield return null;
            }
        }               
    }
}