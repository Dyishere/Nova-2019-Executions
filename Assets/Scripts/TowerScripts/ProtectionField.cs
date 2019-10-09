using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionField : MonoBehaviour
{ 
    public int preCDTime;
    public int preEffectiveTime;
    public GameObject board;
    private int CDTime;
    private int EffectiveTime;
    private GameObject effection;
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
        else if (GetComponentInParent<DamageSystem>().GetCurState() == DamageState.DEATH)
        {
            Debug.Log("该塔已损坏，无法发动技能");
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
        effection = transform.GetChild(0).gameObject;
        effection.gameObject.SetActive(false);
    }
    
    IEnumerator CDCountDown()
    {
        isCD = true;
        while (CDTime <= preCDTime)
        {
            CDTime++;
            board.GetComponent<BoardController>().CheckTime(BoardType.protectFieldCDType, preCDTime, CDTime);
            yield return new WaitForSeconds(1);
        }
        CDTime = 0;
        isCD = false;
    }
    IEnumerator EffectiveTimeCountDown()
    {
        transform.GetComponentInParent<DamageSystem>().Protect();
        effection.gameObject.SetActive(true);
        while (EffectiveTime <= preEffectiveTime)
        {
            EffectiveTime++;
            board.GetComponent<BoardController>().CheckTime(BoardType.protectFieldEffectType, preEffectiveTime, EffectiveTime);
            yield return new WaitForSeconds(1);
        }
        EffectiveTime = 0;
        effection.gameObject.SetActive(false);
        transform.GetComponentInParent<DamageSystem>().Protect();
    }
}
