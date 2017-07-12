﻿using UnityEngine;
using System.Collections;

public class keyboardCameraScript : MonoBehaviour {

    private mainMenuScript mmc;
    private gameController gc;
    private int speed = 2;

	// Use this for initialization
	void Start () {
        mmc = GameObject.Find("MainMenuController").GetComponent<mainMenuScript>();
        gc = GameObject.Find("GameController").GetComponent<gameController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!mmc.getIsLoading() && !gc.getIsPaused()) {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
                speed = 5;
            } else {
                speed = 2;
            }
            if (Input.GetKey(KeyCode.W)) {
                transform.Translate(Vector3.forward * speed);
            }
            if (Input.GetKey(KeyCode.A)) {
                transform.Translate(Vector3.left * speed);
            }
            if (Input.GetKey(KeyCode.S)) {
                transform.Translate(Vector3.back * speed);
            }
            if (Input.GetKey(KeyCode.D)) {
                transform.Translate(Vector3.right * speed);
            }
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) {
                transform.Translate(Vector3.down * speed);
            }
            if (Input.GetKey(KeyCode.Space)) {
                transform.Translate(Vector3.up * speed);
            }
        }
	}
}
