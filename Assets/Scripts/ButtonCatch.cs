using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ButtonCatch : MonoBehaviour {

    void mouseLeftClick() {
        var pointer = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(gameObject, pointer, ExecuteEvents.pointerClickHandler);
    }

    void mouseRightClick() {
        mouseLeftClick();
    }

    void mouseOff() {
        var pointer = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(gameObject, pointer, ExecuteEvents.pointerUpHandler);
    }
    void mouseOnLeft() {
        var pointer = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(gameObject, pointer, ExecuteEvents.pointerDownHandler);
    }
    void mouseOnRight() {
        mouseOnLeft();
    }
    void mouseOut() {
        var pointer = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(gameObject, pointer, ExecuteEvents.pointerExitHandler);
    }
    void mouseOver()
    {
        var pointer = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(gameObject, pointer, ExecuteEvents.pointerEnterHandler);
    }
}
