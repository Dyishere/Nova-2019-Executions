using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSpeedUp : MonoBehaviour
{
    public int preCDTime;
    public float EffectiveTime;
    public GameObject board;
    public GameObject gun;
    private int CDTime;
    private bool isActive;
    private bool isCD;

    // Start is called before the first frame update
    void Start()
    {
        InitProtectionField();
    }

    public void BeginProtectionField()
    {
        if (isActive || isCD)
        {
            return;
        }
        else if (GetComponentInParent<DamageSystem>().GetCurState() == DamageState.DEATH)
        {
            Debug.Log("该塔已损坏，无法发动技能");
            return;
        }
        else
        {
            isActive = true;
            Working();
            StartCoroutine(CDCountDown());
        }
    }
    private void Working()
    {
        if (!isActive)
        {

            return;
        }
        else
        {
            isActive = false;
            StartCoroutine(EffectiveTimeCountDown());
        }
    }
    private void InitProtectionField()
    {
        isCD = false;
        isActive = false;
    }
    IEnumerator CDCountDown()
    {
        isCD = true;
        while (CDTime <= preCDTime)
        {
            CDTime++;
            board.GetComponent<BoardController>().CheckTime(BoardType.speedUpCDType, preCDTime, CDTime);
            yield return new WaitForSeconds(1);
        }
        isCD = false;
    }
    IEnumerator EffectiveTimeCountDown()
    {
        gun.GetComponent<projectileActor>().ShootingSpeedUp();
        while (CDTime <= preCDTime)
        {
            CDTime++;
            board.GetComponent<BoardController>().CheckTime(BoardType.speedUpEffectType, preCDTime, CDTime);
            yield return new WaitForSeconds(1);
        }
        gun.GetComponent<projectileActor>().ShootingSpeedUp();
    }
}
