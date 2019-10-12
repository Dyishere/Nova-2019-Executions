using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSpeedUp : MonoBehaviour
{
    public int preCDTime;
    public int preEffectiveTime;
    public GameObject gun;
    private int CDTime;
    private int EffectiveTime;
    private bool isActive;
    private bool isCD;
    private bool isPause;
    // Start is called before the first frame update
    void Start()
    {
        InitShootingSpeed();
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
    public void BeginSpeedUp()
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
            gun.GetComponent<projectileActor>().ShootingSpeedUp(true);
            isActive = false;
            StartCoroutine(EffectiveTimeCountDown());
        }
    }
    private void InitShootingSpeed()
    {
        isCD = false;
        isActive = false;
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
        while (EffectiveTime > 0)
        {
            EffectiveTime--;
            if (isPause)
            {
                yield return !isPause;
            }
            yield return new WaitForSeconds(1);
        }
        gun.GetComponent<projectileActor>().ShootingSpeedUp(false);
    }
}
