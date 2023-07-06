using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PressButton : MonoBehaviour
{
    [SerializeField]
    Button Button;

    public KeyCode Vita = KeyCode.Joystick1Button1, Pc = KeyCode.Escape;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(Pc) || Input.GetKeyDown(Vita))
        {
            /*var go = Button.gameObject;
            var ped = new PointerEventData(EventSystem.current);
            ExecuteEvents.Execute(go, ped, ExecuteEvents.pointerClickHandler);
            ExecuteEvents.Execute(go, ped, ExecuteEvents.submitHandler);*/

            Button.onClick.Invoke();
        }
    }
}
