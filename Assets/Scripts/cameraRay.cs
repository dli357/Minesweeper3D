using UnityEngine;
using System.Collections;

public class cameraRay : MonoBehaviour {
    Vector3 location;
    Vector3 fwd;
    GameObject lastHit;
    GameObject leftClickedObject;
    GameObject rightClickedObject;
    bool mousedIn = false;
    bool startedOnLeft = false;
    bool startedOnRight = false;
    bool mouseLeftHold = false;
    bool mouseRightHold = false;

    void Start () {
        location = transform.position;
        fwd = transform.TransformDirection(Vector3.forward);
        mousedIn = false;
        startedOnLeft = false;
        startedOnRight = false;
        mouseLeftHold = false;
        mouseRightHold = false;
    }

	void Update () {
        location = transform.position;
        fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        //The block that controls mouse hovering
        if (Physics.Raycast(location, fwd, out hit)) {
            if (!mousedIn) {
                hit.transform.gameObject.SendMessage("mouseOver");
            } else if (!GameObject.ReferenceEquals(hit.transform.gameObject, lastHit)) {
                if (lastHit && lastHit.activeInHierarchy) {
                    lastHit.SendMessage("mouseOut");
                }
                hit.transform.gameObject.SendMessage("mouseOver");
            }
            mousedIn = true;
            lastHit = hit.transform.gameObject;
        } else {
            if (lastHit && lastHit.activeInHierarchy) {
                lastHit.SendMessage("mouseOut");
            }
            mousedIn = false;
        }
        //The block that controls OnPointerDown, OnPointerUp, and onClicked
        if (Input.GetMouseButton(0)) {
            if (mousedIn && !mouseLeftHold) {
                lastHit.SendMessage("mouseOnLeft");
                leftClickedObject = lastHit.gameObject;
                startedOnLeft = true;
            }
            mouseLeftHold = true;
        } else {
            if (startedOnLeft) {
                leftClickedObject.SendMessage("mouseOffLeft");
                if (mousedIn && GameObject.ReferenceEquals(leftClickedObject, lastHit)) {
                    leftClickedObject.SendMessage("mouseLeftClick");
                }
                startedOnLeft = false;
            }
            mouseLeftHold = false;
        }
        if (Input.GetMouseButton(1)) {
            if (mousedIn && !mouseRightHold) {
                lastHit.SendMessage("mouseOnRight");
                rightClickedObject = lastHit.gameObject;
                startedOnRight = true;
            }
            mouseRightHold = true;
        } else {
            if (startedOnRight) {
                rightClickedObject.SendMessage("mouseOffRight");
                if (mousedIn && GameObject.ReferenceEquals(rightClickedObject, lastHit)) {
                    rightClickedObject.SendMessage("mouseRightClick");
                }
                startedOnRight = false;
            }
            mouseRightHold = false;
        }
    }
}
