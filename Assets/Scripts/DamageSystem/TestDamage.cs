using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDamage : MonoBehaviour
{
    private DamageSystem m_damageSystem;
    // Start is called before the first frame update
    void Start()
    {
        m_damageSystem = GetComponent<DamageSystem>();
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
        m_damageSystem.Damage(20);
        Debug.Log(name + "血量为" + m_damageSystem.GetCurHealth() + "，状态为" + m_damageSystem.GetCurState());
    }
}
