using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour {
    private int height;
    private int width;
    private int boardHeight;
    private int boardWidth;
    private int numMines;
    private int numFlags;
    private int cubesComputing;
    private float gameTime;
    private bool gameStarted;
    private bool gameLost;
    private bool gameWon;
    private bool isPaused;
    public GameObject gameBoard;
    public GameObject gameCube;
    public GameObject backgroundQuad;
    public GameObject minesLeftText;
    public GameObject timeElapsedText;
    public GameObject faceCube;
    public GameObject digitDisplay;
    public GameObject pauseMenu;
    private mainMenuScript mmc;
    private fourDigitHexDisplay timerDisplay;
    private fourDigitHexDisplay minesLeftDisplay;
    private GameObject gameBoardClone;

    private GameObject[] mines;
    private GameObject[,] gameCubes;
    private ArrayList notMineFlags;

    // Update is called once per frame
    void Update() {
        if (gameStarted) {
            if (!gameWon && !gameLost) {
                gameTime += Time.deltaTime;
                timerDisplay.GetComponent<fourDigitHexDisplay>().setValue((int)gameTime);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            togglePause();
        }
    }

    //Getters and Setters
    public void addToWrongFlagsList(GameObject g) {
        notMineFlags.Add(g);
    }
    public bool getGameWon() {
        return gameWon;
    }
    public bool getGameLost() {
        return gameLost;
    }
    public bool getGameStarted() {
        return gameStarted;
    }
    public bool getIsPaused() {
        return isPaused;
    }
    public GameObject[,] getGameCubes() {
        return gameCubes;
    }
    public int getWidth() {
        return width;
    }
    public int getHeight() {
        return height;
    }
    public void decrementNumFlags() {
        numFlags--;
        minesLeftDisplay.GetComponent<fourDigitHexDisplay>().setValue(numMines - numFlags);
    }
    public void incrementNumFlags() {
        numFlags++;
        minesLeftDisplay.GetComponent<fourDigitHexDisplay>().setValue(numMines - numFlags);
    }
    public void setGameWon() {
        gameWon = true;
        GameObject.Find("FaceCube(Clone)").GetComponent<faceCubeCatchController>().setWonFace();
    }
    public void setGameLost() {
        gameLost = true;
        GameObject.Find("FaceCube(Clone)").GetComponent<faceCubeCatchController>().setLostFace();
        foreach (GameObject m in mines) {
            m.GetComponent<gameCubeCatchController>().setOpen();
        }
        foreach (GameObject w in notMineFlags) {
            w.GetComponent<gameCubeCatchController>().showWrongFlagMat();
        }
    }
    public void removeFromWrongFlagsList(GameObject g) {
        notMineFlags.Remove(g);
    }


    //Do not use Start or Awake
    //This method launches after any necessary params are set
    //Use this for initialization
    void altStart() {
        gameStarted = false;
        gameWon = false;
        gameLost = false;
        isPaused = false;
        gameTime = 0;
        numFlags = 0;
        cubesComputing = 0;
        mines = new GameObject[numMines];
        gameCubes = new GameObject[width, height];
        notMineFlags = new ArrayList();
        calculateBoardParams();
        mmc = GameObject.Find("MainMenuController").GetComponent<mainMenuScript>();
        StartCoroutine(generateBackgroundQuad());
        StartCoroutine(generateGameCubes());
    }

    private void calculateBoardParams() {
        boardHeight = height * 40 + 125;
        boardWidth = width * 40 + 50;
    }

    public IEnumerator checkWinConditions() {
        while (cubesComputing != 0) {
            yield return null;
        }
        //Only run after all game cubes are opened by the recursive function
        bool won = true;
        for (int i = 0; i < height; i++) {
            for (int j = 0; j < width; j++) {
                if (!gameCubes[j, i].GetComponent<gameCubeCatchController>().getIsMine()) {
                    if (!gameCubes[j, i].GetComponent<gameCubeCatchController>().getIsOpen()) {
                        won = false;
                    }
                }
            }
        }
        if (won) {
            setGameWon();
        }
    }

    private IEnumerator generateBackgroundQuad() {
        gameBoardClone = (GameObject) Instantiate(gameBoard, new Vector3(0, 0, 275), new Quaternion(0, 0, 0, 0));
        GameObject backgroundQuadClone = (GameObject) Instantiate(backgroundQuad, gameBoardClone.transform, false);
        backgroundQuadClone.transform.position = new Vector3(0, 0, 275);
        backgroundQuadClone.transform.localScale = new Vector3(boardWidth, boardHeight);
        GameObject faceCubeClone = (GameObject) Instantiate(faceCube, gameBoardClone.transform, false);
        faceCubeClone.transform.position = new Vector3(0, boardHeight / 2 - 40, 275);
        faceCubeClone.transform.rotation = Quaternion.Euler(0, 0, 180);
        faceCubeClone.GetComponent<faceCubeCatchController>().setGameController(gameObject.GetComponent<gameController>());
        GameObject timeElapsedTextClone = (GameObject) Instantiate(timeElapsedText, gameBoardClone.transform, false);
        timeElapsedTextClone.transform.position = new Vector3(-180, boardHeight / 2 - 10, 275);
        GameObject minesLeftTextClone = (GameObject) Instantiate(minesLeftText, gameBoardClone.transform, false);
        minesLeftTextClone.transform.position = new Vector3(70, boardHeight / 2 - 10, 275);
        timerDisplay = ((GameObject) Instantiate(digitDisplay, gameBoardClone.transform, false)).GetComponent<fourDigitHexDisplay>();
        timerDisplay.transform.position = new Vector3(-115, boardHeight / 2 - 62, 275);
        timerDisplay.transform.localScale = new Vector3(4, 4, 4);
        timerDisplay.name = "TimerDigitDisplay";
        minesLeftDisplay = ((GameObject) Instantiate(digitDisplay, gameBoardClone.transform, false)).GetComponent<fourDigitHexDisplay>();
        minesLeftDisplay.transform.position = new Vector3(115, boardHeight / 2 - 62, 275);
        minesLeftDisplay.transform.localScale = new Vector3(4, 4, 4);
        minesLeftDisplay.name = "MinesLeftDigitDisplay";
        minesLeftDisplay.GetComponent<fourDigitHexDisplay>().setValue(numMines);
        yield return null;
    }

    private IEnumerator generateGameCubes() {
        int y = (boardHeight / 2) - 120;
        int z = 275;
        for (int i = 0; i < height; i++) {
            int x = -1 * (width * 40 / 2) + 20;
            for (int j = 0; j < width; j++) {
                gameCubes[j, i] = (GameObject) Instantiate(gameCube, gameBoardClone.transform, false);
                gameCubes[j, i].transform.position = new Vector3(x, y, z);
                gameCubes[j, i].name = (i + 1) + ":" + (j + 1);
                gameCubes[j, i].GetComponent<gameCubeCatchController>().setGameController(gameObject.GetComponent<gameController>());
                x += 40;
            }
            y -= 40;
            yield return null;
        }
        mmc.offLoadingCanvas();
    }

    public IEnumerator openGameCube(GameObject gameCube) {
        cubesComputing++;
        yield return null;
        gameCubeCatchController cubeController = gameCube.GetComponent<gameCubeCatchController>();
        string[] coords = gameCube.name.Split(':');
        int y = Convert.ToInt32(coords[0]);
        int x = Convert.ToInt32(coords[1]);
        if (!gameStarted) {
            //If game has not started, spawn mines and create safe zone
            gameStarted = true;
            spawnMines(x, y);
            StartCoroutine(openGameCube(gameCube));
        } else if (!cubeController.getIsOpen()
            && !cubeController.getIsFlagged()) {
            //Make sure box is both unopened and unflagged
            cubeController.setOpen();
            if (cubeController.getIsMine()) {
                //Lose game if you open a mine
                setGameLost();
            } else if (cubeController.getNumber() == 0) {
                //If zero, recursively open until you hit a number
                for (int i = 0; i < 9; i++) {
                    int searchX = x + ((i % 3) - 1);
                    int searchY = y + ((i / 3) - 1);
                    if ((searchX != x || searchY != y) && searchX > 0 && searchY > 0 && searchX <= width && searchY <= height) {
                        StartCoroutine(openGameCube(gameCubes[searchX - 1, searchY - 1]));
                    }
                }
            } else {
                //If a number, just open it
                if (!cubeController.getIsMine()) {
                    gameCube.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
        }
        cubesComputing--;
    }

    public void resetGame() {
        Destroy(gameBoardClone);
        altStart();
    }

    public void returnToMenu() {
        mmc.onLoadingCanvas();
        SceneManager.LoadSceneAsync("Main Menu");
    }

    public void setLevelParams(int h, int w, int nM) {
        height = h;
        width = w;
        numMines = nM;
    }

    //Spawns the mines and sets the numbers of all the blocks around them
    private void spawnMines(int notX, int notY) {
        System.Random rand = new System.Random();
        for (int i = 0; i < numMines; i++) {
            int x;
            int y;
            bool isSet = false;
            while (!isSet) {
                bool inNoZone = false;
                x = rand.Next(width) + 1;
                y = rand.Next(height) + 1;
                //Keep mines from spawning in a 3x3 space centered around where you clicked
                for (int j = 0; j < 9; j++) {
                    int prohibX = notX + ((j % 3) - 1);
                    int prohibY = notY + ((j / 3) - 1);
                    if (x == prohibX && y == prohibY) {
                        inNoZone = true;
                        break;
                    }
                }
                if (!gameCubes[x - 1, y - 1].GetComponent<gameCubeCatchController>().getIsMine() && !inNoZone) {
                    mines[i] = gameCubes[x - 1, y - 1];
                    mines[i].GetComponent<gameCubeCatchController>().setIsMine(true);
                    isSet = true;
                    //Increment all the blocks number around the mine
                    for (int k = 0; k < 9; k++) {
                        int searchX = x + ((k % 3) - 1);
                        int searchY = y + ((k / 3) - 1);
                        if ((searchX == x && searchY == y) || (searchX < 1) || (searchY < 1) || (searchX > width) || (searchY > height)) {
                            continue;
                        }
                        gameCubes[searchX - 1, searchY - 1].GetComponent<gameCubeCatchController>().incrementNumber();
                    }
                }
            }
        }
        //Set the text for all the numbers
        for (int l = 0; l < height; l++) {
            for (int m = 0; m < width; m++) {
                GameObject gameCube = gameCubes[m, l];
                if (!gameCube.GetComponent<gameCubeCatchController>().getIsMine()
                    && gameCube.GetComponent<gameCubeCatchController>().getNumber() != 0) {
                    gameCube.transform.GetChild(0).GetComponent<TextMesh>().text
                        = gameCube.GetComponent<gameCubeCatchController>().getNumber().ToString();
                }
            }
        }
    }

    public void togglePause() {
        if (isPaused) {
            pauseMenu.SetActive(false);
            gameBoardClone.SetActive(true);
            isPaused = false;
        } else {
            gameBoardClone.SetActive(false);
            pauseMenu.SetActive(true);
            isPaused = true;
        }
    }
}
