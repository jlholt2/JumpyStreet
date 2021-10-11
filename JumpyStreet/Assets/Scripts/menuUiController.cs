using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuUiController : MonoBehaviour
{
    public GameObject helpScreen;
    public string sceneName;

    public void changeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void HelpScreen()
    {
        if (helpScreen.active)
        {
            helpScreen.SetActive(false);
        }
        else
        {
            helpScreen.SetActive(true);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Exit();
        }
    }
}
