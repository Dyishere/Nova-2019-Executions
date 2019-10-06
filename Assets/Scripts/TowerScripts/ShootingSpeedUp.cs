using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSpeedUp : MonoBehaviour
{
    public float CDTime;
    public float EffectiveTime;
    public GameObject gun;
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
    }
    IEnumerator CD()
    {
        isCD = true;
        yield return new WaitForSeconds(CDTime);
        isCD = false;
    }
    IEnumerator EffectiveTimeCD()
    {
        gun.GetComponent<projectileActor>().ShootingSpeedUp();
        yield return new WaitForSeconds(EffectiveTime);
        gun.GetComponent<projectileActor>().ShootingSpeedUp();
    }
}
