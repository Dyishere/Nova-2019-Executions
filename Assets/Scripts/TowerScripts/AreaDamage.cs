using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            other.gameObject.GetComponent<DamageSystem>().Damage(other.gameObject.GetComponent<DamageSystem>().GetCurHealth());
        }
    }
}
