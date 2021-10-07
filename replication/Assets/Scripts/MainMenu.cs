using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text GameOverText;
    public void LoadScene(int levels)
    {
        SceneManager.LoadScene(levels);
    }
    public void EndGame()
    {
        Application.Quit();
    }
    public void GameOver()
    {
        GameOverText.gameObject.SetActive(true);
    }
}
