using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamageController : MonoBehaviour
{
    public int preCDTime;
    public float movingSpeed;
    public GameObject board;
    private GameObject Effection;
    private GameObject endPos;
    private Vector3 startPos;
    private int CDTime;
    private bool isActive;
    private bool isCD;

    // Start is called before the first frame update
    void Start()
    {
        InitAreaDamage();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Pause.GetInstance().GetState() == false)
        {
            Working();
        }
    }

    public void BeginAreaDamage()
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
            Effection.gameObject.SetActive(true);
            isActive = true;
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
            Effection.transform.Translate((endPos.transform.position - startPos) * movingSpeed * Time.deltaTime);
            if (endPos.transform.position.z < Effection.transform.position.z)
            {
                Effection.gameObject.SetActive(false);
                isActive = false;
                Effection.transform.position = startPos;
            }
        }
    }
    private void InitAreaDamage()
    {
        isCD = false;
        isActive = false;
        Effection = transform.Find("Effection").gameObject;
        endPos = transform.Find("EndPos").gameObject;
        Effection.gameObject.SetActive(false);
        startPos = transform.position;
    }

    IEnumerator CDCountDown()
    {
        isCD = true;
        while (CDTime <= preCDTime)
        {
            CDTime++;
            board.GetComponent<BoardController>().CheckTime(BoardType.areaDamegeCDType, preCDTime, CDTime);
            yield return new WaitForSeconds(1);
        }
        isCD = false;
        CDTime = 0;
    }
}
