using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        LoadScene();
    }
    void LoadScene()
    {
        StartCoroutine(LevelLoader(SceneManager.GetActiveScene().buildIndex + 1));
    }
    IEnumerator LevelLoader(int LevelIndex)
    {
        yield return new WaitForSeconds(8);
        SceneManager.LoadScene(LevelIndex);
    }
}
