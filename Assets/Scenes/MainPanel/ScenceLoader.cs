using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenceLoader : MonoBehaviour
{
    public string ScenesName;
    private void Awake()
    {
        EventCenter.AddListener(EventType.StartGame, ExistGame);
        EventCenter.AddListener(EventType.EndGame, LoadScene);
    }

    void LoadScene()
    {
        SceneManager.LoadScene(ScenesName);
    }

    void ExistGame()
    {
        Application.Quit();
    }
}
