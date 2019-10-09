using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPointController : MonoBehaviour
{
    private Transform[] weakPoint = new Transform[3];
    public float delayShowTime;
    public DamageSystem m_DamageSystem;
    public int curWeakPointNum;
    public bool isWorking;
    // Start is called before the first frame update
    void Start()
    {
        m_DamageSystem = GetComponent<DamageSystem>();
        InitWeakPoint();
        Invoke("StartCycle", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Pause.GetInstance().GetState() == false && !isWorking)
        {
            StartCycle();
        }
    }

    private void InitWeakPoint()
    {
        while (curWeakPointNum < 3)
        {
            weakPoint[curWeakPointNum] = transform.GetChild(curWeakPointNum);
            weakPoint[curWeakPointNum].gameObject.SetActive(false);
            curWeakPointNum++;
        }
        curWeakPointNum = 0;
        weakPoint[curWeakPointNum].gameObject.SetActive(true);
        isWorking = true;
    }

    public void StartCycle()
    {
        isWorking = true;
        StartCoroutine(WeakPointCycle());
    }

    private IEnumerator WeakPointCycle()
    {
        while (m_DamageSystem.GetCurState() != DamageState.DEATH)
        {

            weakPoint[curWeakPointNum].gameObject.SetActive(false);
            curWeakPointNum = Random.Range(0, 3);
            weakPoint[curWeakPointNum].gameObject.SetActive(true);
            yield return new WaitForSeconds(delayShowTime);
            if (Pause.GetInstance().GetState() == true)
            {
                isWorking = false;
                break;
            }
        }
    }

    private void ShowWeakPoint()
    {

    }

}
