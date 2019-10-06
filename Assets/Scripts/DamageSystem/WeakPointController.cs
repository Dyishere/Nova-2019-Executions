using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPointController : MonoBehaviour
{
    private Transform[] weakPoint = new Transform[3];
    public float delayShowTime;
    private DamageSystem m_DamageSystem;
    private int curWeakPointNum;
    // Start is called before the first frame update
    void Start()
    {
        m_DamageSystem = GetComponent<DamageSystem>();
        Invoke("InitWeakPoint", 2f);
    }

    // Update is called once per frame
    void Update()
    {

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
        StartCoroutine(WeakPointCycle());
    }

    IEnumerator WeakPointCycle()
    {

        while (m_DamageSystem.GetCurState() != DamageState.DEATH)
        {
            ShowWeakPoint();
            yield return new WaitForSeconds(delayShowTime);
        }
    }

    private void ShowWeakPoint()
    {
        weakPoint[curWeakPointNum].gameObject.SetActive(false);
        curWeakPointNum = Random.Range(0, 3);
        weakPoint[curWeakPointNum].gameObject.SetActive(true);
    }

}
