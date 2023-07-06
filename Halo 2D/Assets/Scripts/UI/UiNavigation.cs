using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiNavigation : MonoBehaviour
{
    //public GameObject Obj;


    public void PrimerBoton(GameObject FirstObject)
    {
        Selectable targetSelectable = FirstObject.GetComponent<Selectable>();

        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(FirstObject);

        if (targetSelectable != null && targetSelectable.interactable)
        {
            // Cambia el estado del objeto para que quede seleccionado
            //targetSelectable.interactable = false;
            //targetSelectable.transition = Selectable.Transition.None;
            targetSelectable.Select();
        }
        /*EventSystem.current.firstSelectedGameObject = FirstObject;

        Button B = FirstObject.GetComponent<Button>();
        if(B != null)
        {
            var go = B.gameObject;
            var ped = new PointerEventData(EventSystem.current);
            ExecuteEvents.Execute(go, ped, ExecuteEvents.selectHandler);
            ExecuteEvents.Execute(go, ped, ExecuteEvents.updateSelectedHandler);
        }*/
    }
}