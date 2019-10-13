﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    private GameObject[] tower = new GameObject[3];
    private float playingTime;
    public GameObject boss;
    private bool isGameWin;
    private float gameOverTime;
    // Start is called before the first frame update
    void Start()
    {
        
        for (int step = 0; step < 3; step++)
        {
            tower[step] = GameObject.Find("platform").transform.GetChild(step).gameObject;
        }
        isGameWin = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGameOver();
    }
    /// <summary>
    /// 返回当前游玩时间
    /// </summary>
    /// <returns></returns>
    public float CurPlayingTime()
    {
        return playingTime;
    }
    private void CheckGameOver()
    {
        if (boss.GetComponent<DamageSystem>().GetCurState() == DamageState.DEATH)
        {
            gameOverTime += Time.deltaTime;
            if (gameOverTime > 22f)
            {
                Debug.Log("Boss已死，玩家胜利，游戏结束");
                Destroy(boss);
                Destroy(this);
            }
        }
        for (int step = 0; step < 3; step++)
        {
            isGameWin = true;
            if (tower[step].GetComponent<DamageSystem>().GetCurState() != DamageState.DEATH)
            {
                isGameWin = false;
                break;
            }
        }
        if (isGameWin)
        {
            Debug.Log("三塔已炸，Boss胜利，游戏结束");
            Destroy(this);
        }
    }
    IEnumerator GameTimeCounting()
    {
        while (!isGameWin || boss.GetComponent<DamageSystem>().GetCurState() != DamageState.DEATH)
        {
            playingTime++;
            yield return new WaitForSeconds(1);
        }
    }
}