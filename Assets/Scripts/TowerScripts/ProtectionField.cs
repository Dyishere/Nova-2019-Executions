using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionField : MonoBehaviour
{ 
    public int preCDTime;
    public int preEffectiveTime;
    private int CDTime;
    private int EffectiveTime;
    private GameObject effection;
    private bool isActive;
    private bool isCD;
    private bool isPause;
    // Start is called before the first frame update
    void Start()
    {
        InitProtectionField();
    }
    private void Update()
    {
        isPause = Pause.GetInstance().GetState();
    }

    public float GetCurValue(string title)
    {
        switch (title)
        {
            case "preCDTime":
                return preCDTime;
            case "CDTime":
                return CDTime;
            case "preEffectiveTime":
                return preEffectiveTime;
            case "EffectiveTime":
                return EffectiveTime;
        }
        return 233;//debug
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
        EffectiveTime = preEffectiveTime;
    }
    
    IEnumerator CDCountDown()
    {
        CDTime = preCDTime;
        isCD = true;
        while (CDTime > 0)
        {
            CDTime--;
            if (isPause)
            {
                yield return !isPause;
            }
            yield return new WaitForSeconds(1);
        }
            EffectiveTime = preEffectiveTime;
            isCD = false;
    }
    IEnumerator EffectiveTimeCountDown()
    {
        transform.GetComponentInParent<DamageSystem>().Protect();
        effection.gameObject.SetActive(true);
        while (EffectiveTime > 0)
        {
            EffectiveTime--;
            if (isPause)
            {
                yield return !isPause;
            }
            yield return new WaitForSeconds(1);
        }
            effection.gameObject.SetActive(false);
            transform.GetComponentInParent<DamageSystem>().Protect();
    }
}
