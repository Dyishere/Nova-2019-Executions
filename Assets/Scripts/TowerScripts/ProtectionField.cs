using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionField : MonoBehaviour
{ 
    public float CDTime;
    public float EffectiveTime;
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
            StartCoroutine(CD());
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
            StartCoroutine(EffectiveTimeCD());
        }
    }
    private void InitProtectionField()
    {
        isCD = false;
        isActive = false;
        effection = transform.GetChild(0).gameObject;
        effection.gameObject.SetActive(false);
    }
    IEnumerator CD()
    {
        isCD = true;
        yield return new WaitForSeconds(CDTime);
        isCD = false;
    }
    IEnumerator EffectiveTimeCD()
    {
        transform.GetComponentInParent<DamageSystem>().Protect();
        effection.gameObject.SetActive(true);
        yield return new WaitForSeconds(EffectiveTime);
        effection.gameObject.SetActive(false);
        transform.GetComponentInParent<DamageSystem>().Protect();
    }
}
