using UnityEngine;
using System.Collections;

public class cameraRay : MonoBehaviour {
    Vector3 location;
    Vector3 fwd;
    GameObject lastHit;
    GameObject clickedObject;
    bool mousedIn = false;
    bool leftMouseDown = false;
    bool rightMouseDown = false;

    void Start () {
        location = transform.position;
        fwd = transform.TransformDirection(Vector3.forward);
	}

	void Update () {
        location = transform.position;
        fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(location, fwd, out hit)) {
            mousedIn = true;
            if (lastHit != null && !lastHit.Equals(hit.transform.gameObject) && lastHit.activeInHierarchy) {
                lastHit.SendMessage("mouseOut");
            }
            lastHit = hit.transform.gameObject;
            if (Input.GetMouseButton(0)) {
                if (Input.GetMouseButtonDown(0)) {
                    clickedObject = lastHit;
                    leftMouseDown = true;
                    rightMouseDown = false;
                }
                if (clickedObject != null && clickedObject.Equals(lastHit)) {
                    hit.transform.gameObject.SendMessage("mouseOnLeft");
                }
            }
            if (Input.GetMouseButton(1)) {
                if (Input.GetMouseButtonDown(1)) {
                    clickedObject = lastHit;
                    rightMouseDown = true;
                    leftMouseDown = false;
                }
                if (clickedObject != null && clickedObject.Equals(lastHit)) {
                    hit.transform.gameObject.SendMessage("mouseOnRight");
                }
            }
            if (clickedObject != null && clickedObject.Equals(lastHit) && !Input.GetMouseButton(0) && !Input.GetMouseButton(1)) {
                clickedObject = null;
                hit.transform.gameObject.SendMessage("mouseOver");
                if (leftMouseDown) {
                    hit.transform.gameObject.SendMessage("mouseLeftClick");
                    leftMouseDown = false;
                }
                if (rightMouseDown) {
                    hit.transform.gameObject.SendMessage("mouseRightClick");
                    rightMouseDown = false;
                }
            } else if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1)) {
                hit.transform.gameObject.SendMessage("mouseOver");
            }
        } else {
            if (clickedObject != null && clickedObject.Equals(lastHit)) {
                clickedObject = null;
                lastHit.SendMessage("mouseOff");
            } else {
                if (mousedIn) {
                    if (lastHit.activeInHierarchy) {
                        lastHit.SendMessage("mouseOut");
                    }
                    mousedIn = false;
                }
            }
        }
	}
}
