using UnityEngine;
using System.Collections;
using System;

public class gameCubeCatchController : MonoBehaviour {
    private bool isMine = false;
    private bool isFlagged = false;
    private bool isOpen = false;
    private int number = 0;
    private int numFlagsAround = 0;
    private int x;
    private int y;
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
        string[] coords = gameObject.name.Split(':');
        y = Convert.ToInt32(coords[0]);
        x = Convert.ToInt32(coords[1]);
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

    public int getFlagNumber() {
        return numFlagsAround;
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

    public void incrementFlagNum() {
        numFlagsAround++;
    }

    public void decrementFlagNum() {
        numFlagsAround--;
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
                for (int i = 0; i < 9; i++) {
                    int searchX = x + ((i % 3) - 1);
                    int searchY = y + ((i / 3) - 1);
                    if ((searchX != x || searchY != y)
                        && searchX > 0 && searchY > 0
                        && searchX <= gameController.getWidth()
                        && searchY <= gameController.getHeight()) {
                        gameController.getGameCubes()[searchX - 1, searchY - 1].GetComponent<gameCubeCatchController>().decrementFlagNum();
                    }
                }
            } else {
                isFlagged = true;
                gameController.incrementNumFlags();
                matRenderer.material = flaggedMat;
                if (!isMine) {
                    gameController.addToWrongFlagsList(gameObject);
                }
                for (int i = 0; i < 9; i++) {
                    int searchX = x + ((i % 3) - 1);
                    int searchY = y + ((i / 3) - 1);
                    if ((searchX != x || searchY != y)
                        && searchX > 0 && searchY > 0
                        && searchX <= gameController.getWidth()
                        && searchY <= gameController.getHeight()) {
                        gameController.getGameCubes()[searchX - 1, searchY - 1].GetComponent<gameCubeCatchController>().incrementFlagNum();
                    }
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
    }

    public void wonGame() {
        gameController.setGameWon();
    }




    //Mouse functions
    public void mouseLeftClick() {
        if (gameController.getCubesComputing() == 0 && !gameController.getIsRestarting()) {
            if (!isFlagged
                && !isOpen
                && !gameController.getGameLost()
                && !gameController.getGameWon()) {
                gameController.SendMessage("openGameCube", gameObject);
                StartCoroutine(gameController.checkWinConditions());
            } else if (numFlagsAround == number
                && isOpen
                && number != 0) {
                for (int i = 0; i < 9; i++) {
                    int searchX = x + ((i % 3) - 1);
                    int searchY = y + ((i / 3) - 1);
                    if ((searchX != x || searchY != y)
                        && searchX > 0 && searchY > 0
                        && searchX <= gameController.getWidth()
                        && searchY <= gameController.getHeight()) {
                        gameController.SendMessage("openGameCube",
                            gameController.getGameCubes()[searchX - 1, searchY - 1]);
                    }
                }
                StartCoroutine(gameController.checkWinConditions());
            }
        }
    }

    public void mouseRightClick() {
        if (!isOpen 
            && !gameController.getGameLost()
            && !gameController.getGameWon()) {
            toggleFlag();
        }
    }

    public void mouseOffLeft() {
        
    }

    public void mouseOffRight() {
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
