using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCanvas : MonoBehaviour
{

    [SerializeField] GameObject playButton, highscoresButton, highScoresMenu;

    private void Start()
    {
        Back();
    }


    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }


    public void DisableAllUI()
    {
        playButton.SetActive(false);
        highscoresButton.SetActive(false);    
        highScoresMenu.SetActive(false);
    }

    public void OpenHighScores()
    {
        DisableAllUI();
        highScoresMenu.SetActive(true);
    }

    public void Back()
    {
        DisableAllUI();

        playButton.SetActive(true);
        highscoresButton.SetActive(true);
    }
}
