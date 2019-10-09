using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour
{
    private DamageSystem m_DamageSystem;
    public float damageValue;
    // Start is called before the first frame update
    void Start()
    {
        m_DamageSystem = transform.GetComponentInParent<DamageSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage()
    {
        m_DamageSystem.Damage(damageValue == 0f ? 40f : damageValue);
    }
}
