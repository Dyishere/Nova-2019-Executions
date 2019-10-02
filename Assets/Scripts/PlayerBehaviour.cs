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

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (SteamVR_Actions.default_GrabPinch.state == true)
            Shoot();
        if (SteamVR_Actions.default_GrabGrip.state == true)
            default_GrabGripss();
        if (SteamVR_Actions.default_Teleport.state == true)
            default_Teleportss();
    }

    public void Shoot() {
        TestText.text = "Shooted!default_GrabPinch" + shootnum.ToString();
        shootnum++;
    }
    public void default_GrabGripss()
    {
        TestText.text = "Shooted!default_GrabGrip" + shootnum.ToString();
        shootnum++;
    }
    public void default_Teleportss()
    {
        TestText.text = "Shooted!default_Teleport" + shootnum.ToString();
        shootnum++;
    }

}
