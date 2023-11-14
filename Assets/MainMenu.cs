using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadEasyScene()
    {
        SceneManager.LoadScene("Easy");
    }

    public void LoadMediumScene()
    {
        SceneManager.LoadScene("Medium");
    }

    public void LoadHardScene()
    {
        SceneManager.LoadScene("Hard");
    }
}
