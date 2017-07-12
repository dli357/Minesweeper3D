using UnityEngine;
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
        gameController.crReset();
    }

    void mouseRightClick() {
        //No action on right click
    }

    void mouseOffLeft() {
        if (gameController.getGameLost()) {
            matRenderer.material = deadFace;
        } else if (gameController.getGameWon()) {
            matRenderer.material = winFace;
        } else {
            matRenderer.material = defaultFace;
        }
    }
    void mouseOffRight() {
        //No action non right mouse down
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
