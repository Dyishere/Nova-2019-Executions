using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class TestDamage : MonoBehaviour
{
    public Text test;
    private DamageSystem m_damageSystem;
    // Start is called before the first frame update
    void Start()
    {
        m_damageSystem = GetComponent<DamageSystem>();
       test = GameObject.Find("Text2").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Pause.GetInstance().GetState() == false)
        {
            if (SteamVR_Actions.default_Menu.stateDown == true)
            {
                test.text = "default_Menu";
            }
            if (SteamVR_Actions.buggy_Reset.stateDown == true)
            {
                test.text = "p_buggy_Reset";
            }
        }

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
