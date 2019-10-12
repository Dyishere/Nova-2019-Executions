using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private DamageSystem m_damageSystem;
    private bool[] isInState = new bool[4];
    // Start is called before the first frame update
    void Start()
    {
        m_damageSystem = GetComponent<DamageSystem>();
        for (int step = 0;step < 4; step++)
        {
            isInState[step] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckBossState(m_damageSystem.GetCurState());
    }

    private void CheckBossState(DamageState curState)
    {
        switch (curState)
        {
            case DamageState.FIRSTPERIOD:
                Debug.Log("Boss阶段一的持续调用区");
                if (!isInState[0])
                {
                    isInState[0] = true;
                    Debug.Log("Boss阶段一的一次性调用区");
                }
                break;
        }
        switch (curState)
        {
            case DamageState.SECONDPERIOD:
                Debug.Log("Boss阶段二的持续调用区");
                if (!isInState[1])
                {
                    isInState[1] = true;
                    Debug.Log("Boss阶段二的一次性调用区");
                }
                break;
        }
        switch (curState)
        {
            case DamageState.THIRDPERIOD:
                Debug.Log("Boss阶段三的持续调用区");
                if (!isInState[2])
                {
                    isInState[2] = true;
                    Debug.Log("Boss阶段三的一次性调用区");
                }
                break;
        }
        switch (curState)
        {
            case DamageState.DEATH:
                Debug.Log("Boss死亡的持续调用区");
                if (!isInState[3])
                {
                    isInState[3] = true;
                    Debug.Log("Boss死亡的一次性调用区");
                }
                break;
        }
    }
}
