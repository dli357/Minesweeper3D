using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenuScript : MonoBehaviour {
    private GameObject mainMenuCanvas;
    private GameObject instructionsCanvas;
    private GameObject difficultyCanvas;
    private GameObject loadingCanvas;
    private GameObject customDifficultyCanvas;
    private GameObject errorMessageCanvas;
    private bool mainMenuCanvasOn = true;
    private bool difficultyCanvasOn = false;
    private bool instructionsCanvasOn = false;
    private bool customDifficultyCanvasOn = false;
    private bool startedGame = false;
    public Material backgroundLoadingMaterial;
    private Material defaultSkybox;

    void Awake() {
        var objs = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject g in objs) {
            if (g.name.Equals("MainMenuController") && g.GetComponent<mainMenuScript>().getStartedGame()) {
                Destroy(g);
            }
        }
        mainMenuCanvas = GameObject.Find("MainMenuCanvas");
        instructionsCanvas = GameObject.Find("InstructionsCanvas");
        difficultyCanvas = GameObject.Find("DifficultyCanvas");
        loadingCanvas = GameObject.Find("LoadingCanvas");
        customDifficultyCanvas = GameObject.Find("CustomDifficultyCanvas");
        errorMessageCanvas = GameObject.Find("ErrorMessageCanvas");
        defaultSkybox = RenderSettings.skybox;
        instructionsCanvas.SetActive(false);
        difficultyCanvas.SetActive(false);
        loadingCanvas.SetActive(false);
        customDifficultyCanvas.SetActive(false);
        errorMessageCanvas.SetActive(false);
    }

    public bool getStartedGame() {
        return startedGame;
    }

    //Quit

    public void quit() {
        Application.Quit();
    }

    //Canvas toggler functions

    public void toggleMainMenuCanvas() {
        if (mainMenuCanvasOn) {
            mainMenuCanvas.SetActive(false);
            mainMenuCanvasOn = false;
        } else {
            mainMenuCanvas.SetActive(true);
            mainMenuCanvasOn = true;
        }
    }

    public void toggleDifficultyCanvas() {
        if (difficultyCanvasOn) {
            difficultyCanvas.SetActive(false);
            difficultyCanvasOn = false;
        } else {
            difficultyCanvas.SetActive(true);
            difficultyCanvasOn = true;
        }
    }

    public void toggleInstructionsCanvas() {
        if (instructionsCanvasOn) {
            instructionsCanvas.SetActive(false);
            instructionsCanvasOn = false;
        } else {
            instructionsCanvas.SetActive(true);
            instructionsCanvasOn = true;
        }
    }

    public void toggleCustomDifficultyCanvas() {
        if (customDifficultyCanvasOn) {
            customDifficultyCanvas.SetActive(false);
            customDifficultyCanvasOn = false;
        } else {
            customDifficultyCanvas.SetActive(true);
            customDifficultyCanvasOn = true;
        }
    }

    public void onErrorMessageCanvas(string message) {
        errorMessageCanvas.SetActive(true);
        errorMessageCanvas.transform.GetChild(1).GetComponent<Text>().text = message;
    }

    public void offErrorMessageCanvas() {
        errorMessageCanvas.transform.GetChild(1).GetComponent<Text>().text = null;
        errorMessageCanvas.SetActive(false);
    }

    public void onLoadingCanvas() {
        loadingCanvas.SetActive(true);
        RenderSettings.skybox = backgroundLoadingMaterial;
        RenderSettings.reflectionIntensity = 0;
    }

    public void offLoadingCanvas() {
        loadingCanvas.SetActive(false);
        RenderSettings.skybox = defaultSkybox;
        RenderSettings.reflectionIntensity = 1;
    }

    public void loadCustomDifficulty() {
        int w = 0;
        int h = 0;
        int nM = 0;
        try {
            string widthText = customDifficultyCanvas.transform.GetChild(1).GetChild(0).Find("Text").GetComponent<Text>().text;
            string heightText = customDifficultyCanvas.transform.GetChild(1).GetChild(1).Find("Text").GetComponent<Text>().text;
            string numMinesText = customDifficultyCanvas.transform.GetChild(1).GetChild(2).Find("Text").GetComponent<Text>().text;
            w = Convert.ToInt32(widthText);
            h = Convert.ToInt32(heightText);
            nM = Convert.ToInt32(numMinesText);
            if (w < 9 || h < 9 || nM < 10) {
                throw new FormatException();
            }
            if (w > 100 || h > 100 || nM > (w * h) - 10) {
                throw new OverflowException();
            }
            loadGame(w, h, nM);
        } catch (FormatException f) {
            string errorMessage = "You must enter valid positive integers. Width and Height must be greater than or equal to 9"
                + ", and number of mines must be greater than or equal to 10.";
            onErrorMessageCanvas(f.Message + ". " + errorMessage);
            toggleCustomDifficultyCanvas();
        } catch (OverflowException o) {
            string errorMessage = "Your integers are too large. Please enter smaller integers.";
            onErrorMessageCanvas(o.Message + " " + errorMessage);
            toggleCustomDifficultyCanvas();
        }
    }

    public void loadGame(string difficulty) {
        if (difficulty.Equals("easy")) {
            StartCoroutine(changeScene(9, 9, 10));
        } else if (difficulty.Equals("medium")) {
            StartCoroutine(changeScene(16, 16, 40));
        } else if (difficulty.Equals("expert")) {
            StartCoroutine(changeScene(30, 16, 99));
        }
    }

    public void loadGame(int width, int height, int numMines) {
        StartCoroutine(changeScene(width, height, numMines));
    }

    //Load into game function
    public IEnumerator changeScene(int width, int height, int numMines) {        
        AsyncOperation ao = SceneManager.LoadSceneAsync("Game Level");
        onLoadingCanvas();
        DontDestroyOnLoad(gameObject);
        while (!ao.isDone) {
            yield return null;
        }
        startGame(height, width, numMines);
        loadingCanvas = GameObject.Find("LoadingCanvas");
        startedGame = true;
    }

    public void startGame(int h, int w, int nM) {
        GameObject gameController = GameObject.Find("GameController");
        gameController.GetComponent<gameController>().setLevelParams(h, w, nM);
        gameController.GetComponent<gameController>().SendMessage("altStart");
    }
}
