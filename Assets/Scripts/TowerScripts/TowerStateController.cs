using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStateController : MonoBehaviour
{
    private DamageSystem m_damageSystem;
    private bool[] isInState = new bool[4];
    // Start is called before the first frame update
    void Start()
    {
        m_damageSystem = GetComponent<DamageSystem>();
        for (int step = 0; step < 4; step++)
        {
            isInState[step] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckTowerState(m_damageSystem.GetCurState());
    }

    private void CheckTowerState(DamageState curState)
    {
        switch (curState)
        {
            case DamageState.FIRSTPERIOD:
                Debug.Log("tower" + name +"阶段一的持续调用区");
                if (!isInState[0])
                {
                    isInState[0] = true;
                    Debug.Log("tower" + name + "阶段一的一次性调用区");
                }
                break;
            case DamageState.SECONDPERIOD:
                Debug.Log("tower" + name + "阶段二的持续调用区");
                if (!isInState[1])
                {
                    isInState[1] = true;
                    Debug.Log("tower" + name + "阶段二的一次性调用区");
                }
                break;
            case DamageState.THIRDPERIOD:
                Debug.Log("tower" + name + "阶段三的持续调用区");
                if (!isInState[2])
                {
                    isInState[2] = true;
                    Debug.Log("tower" + name + "阶段三的一次性调用区");
                }
                break;
            case DamageState.DEATH:
                Debug.Log("tower" + name + "死亡的持续调用区");
                if (!isInState[3])
                {
                    isInState[3] = true;
                    Debug.Log("tower" + name + "r死亡的一次性调用区");
                }
                break;
        }
    }
}