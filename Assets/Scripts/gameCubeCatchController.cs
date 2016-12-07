using UnityEngine;
using System.Collections;

public class gameCubeCatchController : MonoBehaviour {
    private bool isMine = false;
    private bool isFlagged = false;
    private bool isOpen = false;
    private int number = 0;
    private gameController gameController;
    private Renderer matRenderer;

    public Material unopenedMat;
    public Material openedMat;
    public Material hoverMat;
    public Material mouseLeftDownMat;
    public Material mouseRightDownMat;

    public Material mineOpenedMat;
    public Material mineUnopenedMat;
    public Material correctFlagMat;
    public Material notCorrectFlagMat;

    public Material flaggedMat;
    public Material hoverFlagMat;
    public Material mouseRightDownFlagMat;


    void Awake() {
        matRenderer = gameObject.GetComponent<Renderer>();
    }


    public void setGameController(gameController controller) {
        gameController = controller;
    }

    public void setIsMine(bool b) {
        isMine = b;
    }

    public void setNumber(int input) {
        number = input;
    }

    public bool getIsMine() {
        return isMine;
    }

    public int getNumber() {
        return number;
    }

    public bool getIsFlagged() {
        return isFlagged;
    }

    public bool getIsOpen() {
        return isOpen;
    }

    public void incrementNumber() {
        number++;
    }

    private void toggleFlag() {
        if (gameController.getGameStarted()) {
            if (isFlagged) {
                isFlagged = false;
                gameController.decrementNumFlags();
                matRenderer.material = unopenedMat;
                if (!isMine) {
                    gameController.removeFromWrongFlagsList(gameObject);
                }
            } else {
                isFlagged = true;
                gameController.incrementNumFlags();
                matRenderer.material = flaggedMat;
                if (!isMine) {
                    gameController.addToWrongFlagsList(gameObject);
                }
            }
        }
    }

    public void showWrongFlagMat() {
        matRenderer.material = notCorrectFlagMat;
    }

    public void setOpen() {
        if (!isOpen) {
            if (isMine) {
                if (isFlagged) {
                    matRenderer.material = correctFlagMat;
                } else {
                    if (gameController.getGameLost()) {
                        matRenderer.material = mineUnopenedMat;
                    } else {
                        matRenderer.material = mineOpenedMat;
                    }
                }
            } else {
                matRenderer.material = openedMat;
            }
            isOpen = true;
        }
    }

    public void lostGame() {
        gameController.setGameLost();
        GameObject.Find("FaceCube(Clone)").GetComponent<faceCubeCatchController>().setLostFace();
    }

    public void wonGame() {
        gameController.setGameWon();
        GameObject.Find("FaceCube(Clone)").GetComponent<faceCubeCatchController>().setWonFace();
    }




    //Mouse functions
    public void mouseLeftClick() {
        if (!isFlagged 
            && !isOpen 
            && !gameController.getGameLost() 
            && !gameController.getGameWon()) {
            gameController.SendMessage("openGameCube", gameObject);
            gameController.checkWinConditions();
        }
    }

    public void mouseRightClick() {
        if (!isOpen 
            && !gameController.getGameLost()
            && !gameController.getGameWon()) {
            toggleFlag();
        }
    }

    public void mouseOff() {
        if (!isOpen 
            && !gameController.getGameLost()
            && !gameController.getGameWon()) {
            if (isFlagged) {
                matRenderer.material = flaggedMat;
            } else {
                matRenderer.material = unopenedMat;
            }
        }
    }

    public void mouseOnLeft() {
        if (!isOpen 
            && !gameController.getGameLost()
            && !gameController.getGameWon()) {
            if (!isFlagged) {
                matRenderer.material = mouseLeftDownMat;
            }
        }
    }

    public void mouseOnRight() {
        if (!isOpen 
            && !gameController.getGameLost()
            && !gameController.getGameWon()) {
            if (isFlagged) {
                matRenderer.material = mouseRightDownFlagMat;
            } else {
                matRenderer.material = mouseRightDownMat;
            }
        }
    }

    public void mouseOut() {
        if (!isOpen 
            && !gameController.getGameLost()
            && !gameController.getGameWon()) {
            if (isFlagged) {
                matRenderer.material = flaggedMat;
            } else {
                matRenderer.material = unopenedMat;
            }
        }
    }

    public void mouseOver() {
        if (!isOpen 
            && !gameController.getGameLost()
            && !gameController.getGameWon()) {
            if (isFlagged) {
                matRenderer.material = hoverFlagMat;
            } else {
                matRenderer.material = hoverMat;
            }
        }
    }
}
