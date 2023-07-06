/*****************************************************
 * 													 *
 * Asset:		 Touch Controls Kit					 *
 * Script:		 PrefabCreatorUgui.cs                *
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
using UnityEngine.UI;
using UnityEditor;
using TouchControlsKit.Utils;

namespace TouchControlsKit.Ugui.Inspector
{
    public sealed class PrefabCreatorUgui : Editor
    {
        // 
        private const string mainGOName = "_TouchManagerUgui";
        private const string menuAbbrev = "GameObject/UI/Touch Controls Kit/";
        private const string nameAbbrev = "TouchControlsKit";

        //
        private static GameObject tckGUIobj = null;

        private static GameObject Button = null;

        private static GameObject JoystickMain, JoystickBackgr, JoystickImage;

        private static GameObject Touchpad = null;

        private static GameObject DpadMain, DpadArrowUP, DpadArrowDOWN, DpadArrowLEFT, DpadArrowRIGHT;

        private static GameObject SteeringWheel = null;


        // CreateTouchManager [MenuItem]
        [MenuItem( menuAbbrev + "Touch Manager" )]
        private static void CreateTouchManager()
        {
            if( FindObjectOfType<TouchManagerUgui>() && !tckGUIobj ) tckGUIobj = FindObjectOfType<TouchManagerUgui>().gameObject;

            if( tckGUIobj ) 
                return;

            tckGUIobj = new GameObject( mainGOName, typeof( Canvas ), typeof( GraphicRaycaster ), typeof( CanvasScaler ), typeof( TouchManagerUgui ) );
            tckGUIobj.layer = LayerMask.NameToLayer( "UI" );

            GuiCamera.CreateCamera( tckGUIobj.transform, 32, 100f );
            
            tckGUIobj.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;            
            tckGUIobj.GetComponent<Canvas>().worldCamera = GuiCamera.guiCamera;
            tckGUIobj.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        }

        [MenuItem( menuAbbrev + "Touch Manager", true )]
        private static bool ValidateCreateTouchManager()
        {
            return !FindObjectOfType<TouchManagerUgui>();
        }
        

        // CreateButton [MenuItem]
        [MenuItem( menuAbbrev + "Button" )]
        private static void CreateButton()
        {
            if( !tckGUIobj ) 
                CreateTouchManager();

            SetupController<ButtonUgui>( ref Button, tckGUIobj.transform, "Button" + FindObjectsOfType<ButtonUgui>().Length.ToString(), true );

            ButtonUgui btnTemp = Button.GetComponent<ButtonUgui>();
            btnTemp.normalSprite = AssetDatabase.LoadAssetAtPath<Sprite>( "Assets/" + nameAbbrev + "/Base/Resources/Sprites/ButtonNormal.png" );
            btnTemp.pressedSprite = AssetDatabase.LoadAssetAtPath<Sprite>( "Assets/" + nameAbbrev + "/Base/Resources/Sprites/ButtonPressed.png" );
            btnTemp.MyName = Button.name;
            btnTemp.myData.touchzoneRect.sizeDelta = new Vector2( 55f, 55f );            
            Button.transform.localScale = Vector3.one;
            btnTemp.myData.touchzoneRect.anchoredPosition = RandomPos;
        }

        // CreateJoystick [MenuItem]
        [MenuItem( menuAbbrev + "Joystick" )]
        private static void CreateJoystick()
        {
            if( !tckGUIobj ) 
                CreateTouchManager();

            SetupController<JoystickUgui>( ref JoystickMain, tckGUIobj.transform, "Joystick" + FindObjectsOfType<JoystickUgui>().Length.ToString(), true );

            JoystickUgui joyTemp = JoystickMain.GetComponent<JoystickUgui>();

            joyTemp.myData.touchzoneRect = JoystickMain.GetComponent<RectTransform>();
            joyTemp.myData.touchzoneImage = JoystickMain.GetComponent<Image>();
            joyTemp.myData.touchzoneImage.sprite = AssetDatabase.LoadAssetAtPath<Sprite>( "Assets/" + nameAbbrev + "/Base/Resources/Sprites/Touchzone.png" );            

            SetupController<JoystickUgui>( ref JoystickBackgr, JoystickMain.transform, "JoystickBack", false );
            SetupController<JoystickUgui>( ref JoystickImage, JoystickBackgr.transform, "Joystick", false );

            joyTemp.joystickBackgroundImage = JoystickBackgr.GetComponent<Image>();
            joyTemp.joystickBackgroundRT = JoystickBackgr.GetComponent<RectTransform>();
            joyTemp.joystickBackgroundRT.sizeDelta = new Vector2( 75f, 75f ); 

            joyTemp.joystickImage = JoystickImage.GetComponent<Image>();            
            joyTemp.joystickRT = JoystickImage.GetComponent<RectTransform>();
            joyTemp.joystickRT.anchorMin = Vector2.zero;
            joyTemp.joystickRT.anchorMax = Vector2.one;
            joyTemp.joystickRT.sizeDelta = Vector2.zero;

            joyTemp.joystickBackgroundImage.sprite = AssetDatabase.LoadAssetAtPath<Sprite>( "Assets/" + nameAbbrev + "/Base/Resources/Sprites/JoystickBack.png" );
            joyTemp.joystickImage.sprite = AssetDatabase.LoadAssetAtPath<Sprite>( "Assets/" + nameAbbrev + "/Base/Resources/Sprites/Joystick.png" );

            joyTemp.myData.touchzoneRect.sizeDelta = new Vector2( 180f, 160f );
            
            joyTemp.MyName = JoystickMain.name;

            JoystickMain.transform.localScale = Vector3.one;
            joyTemp.myData.touchzoneRect.anchoredPosition = RandomPos;
        }

        // CreateTouchpad [MenuItem]
        [MenuItem( menuAbbrev + "Touchpad" )]
        private static void CreateTouchpad()
        {
            if( !tckGUIobj ) 
                CreateTouchManager();

            SetupController<TouchpadUgui>( ref Touchpad, tckGUIobj.transform, "Touchpad" + FindObjectsOfType<TouchpadUgui>().Length.ToString(), true );

            TouchpadUgui tpTemp = Touchpad.GetComponent<TouchpadUgui>();
            tpTemp.myData.touchzoneImage = Touchpad.GetComponent<Image>();
            tpTemp.myData.touchzoneRect = Touchpad.GetComponent<RectTransform>();
            tpTemp.myData.touchzoneImage.sprite = AssetDatabase.LoadAssetAtPath<Sprite>( "Assets/" + nameAbbrev + "/Base/Resources/Sprites/Touchzone.png" );
            tpTemp.MyName = Touchpad.name;
            tpTemp.myData.touchzoneRect.sizeDelta = new Vector2( 270f, 170f );

            Touchpad.transform.localScale = Vector3.one;
            tpTemp.myData.touchzoneRect.anchoredPosition = RandomPos;
        }

        // CreateDPad [MenuItem]
        [MenuItem( menuAbbrev + "DPad" )]
        private static void CreateDPad()
        {
            if( !tckGUIobj ) 
                CreateTouchManager();

            SetupController<DPadUgui>( ref DpadMain, tckGUIobj.transform, "DPad" + FindObjectsOfType<DPadUgui>().Length.ToString(), true );

            DPadUgui dpadTemp = DpadMain.GetComponent<DPadUgui>();
            dpadTemp.normalSprite = AssetDatabase.LoadAssetAtPath<Sprite>( "Assets/" + nameAbbrev + "/Base/Resources/Sprites/ArrowNormal.png" );
            dpadTemp.pressedSprite = AssetDatabase.LoadAssetAtPath<Sprite>( "Assets/" + nameAbbrev + "/Base/Resources/Sprites/ArrowPressed.png" );
            dpadTemp.MyName = DpadMain.name;
            dpadTemp.myData.touchzoneRect.sizeDelta = new Vector2( 200f, 200f );
            dpadTemp.myData.touchzoneImage.sprite = AssetDatabase.LoadAssetAtPath<Sprite>( "Assets/" + nameAbbrev + "/Base/Resources/Sprites/Touchzone.png" );

            SetupController<DPadArrowUgui>( ref DpadArrowUP, DpadMain.transform, "ArrowUP", true, DPadArrowUgui.ArrowTypes.UP );
            SetupController<DPadArrowUgui>( ref DpadArrowDOWN, DpadMain.transform, "ArrowDOWN", true, DPadArrowUgui.ArrowTypes.DOWN );
            SetupController<DPadArrowUgui>( ref DpadArrowLEFT, DpadMain.transform, "ArrowLEFT", true, DPadArrowUgui.ArrowTypes.LEFT );
            SetupController<DPadArrowUgui>( ref DpadArrowRIGHT, DpadMain.transform, "ArrowRIGHT", true, DPadArrowUgui.ArrowTypes.RIGHT );

            DpadArrowUP.GetComponent<Image>().sprite = dpadTemp.normalSprite;
            DpadArrowDOWN.GetComponent<Image>().sprite = dpadTemp.normalSprite;
            DpadArrowLEFT.GetComponent<Image>().sprite = dpadTemp.normalSprite;
            DpadArrowRIGHT.GetComponent<Image>().sprite = dpadTemp.normalSprite;

            DpadMain.transform.localScale = Vector3.one;
            dpadTemp.myData.touchzoneRect.anchoredPosition = RandomPos;
        }


        // CreateSteeringWheel [MenuItem]
        [MenuItem( menuAbbrev + "Steering Wheel" )]
        private static void CreateSteeringWheel()
        {
            if( !tckGUIobj )
                CreateTouchManager();

            SetupController<SteeringWheelUgui>( ref SteeringWheel, tckGUIobj.transform, "SteeringWheel" + FindObjectsOfType<SteeringWheelUgui>().Length.ToString(), true );

            SteeringWheelUgui swTemp = SteeringWheel.GetComponent<SteeringWheelUgui>();
            swTemp.myData.touchzoneImage = SteeringWheel.GetComponent<Image>();
            swTemp.myData.touchzoneRect = SteeringWheel.GetComponent<RectTransform>();
            swTemp.myData.touchzoneImage.sprite = AssetDatabase.LoadAssetAtPath<Sprite>( "Assets/" + nameAbbrev + "/Base/Resources/Sprites/SteeringWheel.png" );
            swTemp.MyName = SteeringWheel.name;
            swTemp.myData.touchzoneRect.sizeDelta = new Vector2( 125f, 125f );

            SteeringWheel.transform.localScale = Vector3.one;
            swTemp.myData.touchzoneRect.anchoredPosition = RandomPos;
        }


        // SetupController<Generic>
        private static void SetupController<TComp>(
            ref GameObject myGO, Transform myParent, string myName, bool needMyComponent,
            DPadArrowUgui.ArrowTypes myType = DPadArrowUgui.ArrowTypes.none ) where TComp : MonoBehaviour
        {
            myGO = new GameObject( myName, typeof( Image ) );
            myGO.GetComponent<Image>().color = ElementTransparency.colorHalfSprite;
            myGO.layer = LayerMask.NameToLayer( "UI" );
            myGO.transform.localScale = Vector3.one;            
            myGO.transform.SetParent( myParent );
            if( needMyComponent ) myGO.AddComponent<TComp>();

            if( myType != DPadArrowUgui.ArrowTypes.none )
            {
                myGO.GetComponent<DPadArrowUgui>().ArrowType = myType;
                CalcDPadSizeAndPos( DpadMain.GetComponent<RectTransform>(), myGO.GetComponent<RectTransform>(), myType );
            }                
        }

        // CalcDPadSizeAndPos
        private static void CalcDPadSizeAndPos( RectTransform mainRect, RectTransform childRect, DPadArrowUgui.ArrowTypes myType )
        {
            childRect.sizeDelta = new Vector2( mainRect.sizeDelta.x / 3.4f, mainRect.sizeDelta.y / 3.4f );

            switch( myType )
            {
                case DPadArrowUgui.ArrowTypes.UP:
                    childRect.anchoredPosition = new Vector2( 0f, -childRect.sizeDelta.y / 2f );                    
                    childRect.anchorMin = new Vector2( 0.5f, 1f );
                    childRect.anchorMax = new Vector2( 0.5f, 1f );
                    childRect.rotation = Quaternion.Euler( mainRect.rotation.x, mainRect.rotation.y, mainRect.rotation.z + 90f );
                    break;

                case DPadArrowUgui.ArrowTypes.DOWN:
                    childRect.anchoredPosition = new Vector2( 0f, childRect.sizeDelta.y / 2f );                    
                    childRect.anchorMin = new Vector2( 0.5f, 0f );
                    childRect.anchorMax = new Vector2( 0.5f, 0f );
                    childRect.rotation = Quaternion.Euler( mainRect.rotation.x, mainRect.rotation.y, mainRect.rotation.z + 270f );
                    break;

                case DPadArrowUgui.ArrowTypes.LEFT:
                    childRect.anchoredPosition = new Vector2( childRect.sizeDelta.x / 2f, 0f );                    
                    childRect.anchorMin = new Vector2( 0f, 0.5f );
                    childRect.anchorMax = new Vector2( 0f, 0.5f );
                    childRect.rotation = Quaternion.Euler( mainRect.rotation.x, mainRect.rotation.y, mainRect.rotation.z + 180f );
                    break;

                case DPadArrowUgui.ArrowTypes.RIGHT:
                    childRect.anchoredPosition = new Vector2( -childRect.sizeDelta.x / 2f, 0f );                    
                    childRect.anchorMin = new Vector2( 1f, 0.5f );
                    childRect.anchorMax = new Vector2( 1f, 0.5f );
                    childRect.rotation = Quaternion.Euler( mainRect.rotation.x, mainRect.rotation.y, mainRect.rotation.z );
                    break;
            }
        }
        //

        // RandomPos
        private static Vector2 RandomPos { get { return new Vector2( Random.Range( -200f, 200f ), Random.Range( -120f, 120f ) ); } }
    }
}