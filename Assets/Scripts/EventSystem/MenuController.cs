using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class MenuController : MonoBehaviour
{
    private GameObject board;
    private bool isShow;
    private void Start()
    {
        InitBoard();
    }
    private void Update()
    {
        if (SteamVR_Actions.default_Menu.stateDown == true)
        {
            PauseGame();
        }
    }
    private void InitBoard()
    {
        board = GameObject.Find("TotalBoard");
        board.SetActive(false);
        isShow = false;
    }
    public void ContiuneGame()
    {
        isShow = !isShow;
        board.SetActive(isShow);
    }
    public void QuitGame()
    {
        Debug.Log("退出");
    }
    public void PauseGame()
    {
        isShow = !isShow;
        board.SetActive(isShow);
    }
}
