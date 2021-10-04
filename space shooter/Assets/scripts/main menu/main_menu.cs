using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class main_menu : MonoBehaviour
{
    public void loadgame()
    {
        SceneManager.LoadScene(1);
    }
    public void Endgame()
    {
        Application.Quit();
    }

}
