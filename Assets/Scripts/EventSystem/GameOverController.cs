using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    private GameObject[] tower = new GameObject[3];
    public GameObject boss;
    private bool isGameWin;
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

    private void CheckGameOver()
    {
        if (boss.GetComponent<DamageSystem>().GetCurState() == DamageState.DEATH)
        {
            Debug.Log("Boss已死，玩家胜利，游戏结束");
            Destroy(this);
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
}
