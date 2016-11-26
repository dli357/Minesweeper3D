﻿using UnityEngine;
using System.Collections;

public class faceCubeCatchController : MonoBehaviour {

    private Renderer matRenderer;
    private gameController gameController;

    public Material defaultFace;
    public Material defaultFaceHover;
    public Material defaultFaceDown;
    public Material deadFace;
    public Material deadFaceHover;
    public Material deadFaceDown;
    public Material winFace;
    public Material winFaceHover;
    public Material winFaceDown;

	// Use this for initialization
	void Start () {
        matRenderer = gameObject.GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setLostFace() {
        matRenderer.material = deadFace;
    }

    public void setWonFace() {
        matRenderer.material = winFace;
    }



    //Mouse functions
    public void setGameController(gameController gC) {
        gameController = gC;
    }

    void mouseLeftClick() {
        gameController.resetGame();
    }

    void mouseRightClick() {
        //No action on right click
    }

    void mouseOff() {
        if (gameController.getGameLost()) {
            matRenderer.material = deadFace;
        } else if (gameController.getGameWon()) {
            matRenderer.material = winFace;
        } else {
            matRenderer.material = defaultFace;
        }
    }
    void mouseOnLeft() {
        if (gameController.getGameLost()) {
            matRenderer.material = deadFaceDown;
        } else if (gameController.getGameWon()) {
            matRenderer.material = winFaceDown;
        } else {
            matRenderer.material = defaultFaceDown;
        }
    }
    void mouseOnRight() {
        //No action on right mouse down
    }
    void mouseOut() {
        if (gameController.getGameLost()) {
            matRenderer.material = deadFace;
        } else if (gameController.getGameWon()) {
            matRenderer.material = winFace;
        } else {
            matRenderer.material = defaultFace;
        }
    }
    void mouseOver() {
        if (gameController.getGameLost()) {
            matRenderer.material = deadFaceHover;
        } else if (gameController.getGameWon()) {
            matRenderer.material = winFaceHover;
        } else {
            matRenderer.material = defaultFaceHover;
        }
    }
}
