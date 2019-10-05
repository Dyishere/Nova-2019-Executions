using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerBehaviour : MonoBehaviour
{
    //public GameObject Bullet;
    public Text TestText;
    int shootnum = 0;
    public GameObject Boss;
    public GameObject Monster;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        TestDamageText();

        if (Monster.GetComponent<DamageSystem>().GetCurState() == DamageState.DEATH)
            TestText.text = "小怪死了";
        /*
        if (SteamVR_Actions.default_GrabPinch.state == true)
            Shoot();
        if (SteamVR_Actions.default_GrabGrip.state == true)
            default_GrabGripss();
        if (SteamVR_Actions.default_Teleport.state == true)
            default_Teleportss();
            */
    }

    private void Shoot() {
        TestText.text = "Shooted!default_GrabPinch" + shootnum.ToString();
        shootnum++;
    }
    private void default_GrabGripss()
    {
        TestText.text = "Shooted!default_GrabGrip" + shootnum.ToString();
        shootnum++;
    }
    private void default_Teleportss()
    {
        TestText.text = "Shooted!default_Teleport" + shootnum.ToString();
        shootnum++;
    }
    private void TestDamageText()
    {
        TestText.text = name + "血量为" + Boss.GetComponent<DamageSystem>().GetCurHealth() + "，状态为" + Boss.GetComponent<DamageSystem>().GetCurState();

    }

}
