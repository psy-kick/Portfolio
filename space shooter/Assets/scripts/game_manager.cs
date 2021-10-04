using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class game_manager : MonoBehaviour
{
    [SerializeField]
    private bool _isgameover;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)&&_isgameover==true)
        {
            SceneManager.LoadScene(1);
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
    public void gameover()
    {
        _isgameover = true;
    }
    
}
