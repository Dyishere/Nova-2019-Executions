using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDamageOnWeakPoint : MonoBehaviour
{
    private WeakPoint m_WeakPoint;
    // Start is called before the first frame update
    void Start()
    {
        m_WeakPoint = GetComponent<WeakPoint>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter()
    {
        transform.localScale *= 2f;
    }

    private void OnMouseExit()
    {
        transform.localScale /= 2f;
    }

    private void OnMouseDown()
    {
        m_WeakPoint.Damage();
    }
}
