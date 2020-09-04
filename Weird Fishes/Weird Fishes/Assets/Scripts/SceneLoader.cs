using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public void LoadNextScene() {
        //build index returns the active scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        //this loads the next scene
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadFirstScene() {
        SceneManager.LoadScene(0);
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void QuitGame() {
        Application.Quit();
    }
}
