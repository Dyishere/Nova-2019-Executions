using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TeleportButton : MonoBehaviour
{

    public HoverButton hoverButton;

    public GameObject player;

    public Transform teleportPos;

    private void Awake()
    {

    }

    private void Start()
    {
        hoverButton.onButtonDown.AddListener(OnButtonDown);
    }

    private void OnButtonDown(Hand hand)
    {
        if (teleportPos.GetComponentInParent<DamageSystem>().GetCurState() == DamageState.DEATH)
        {
            Debug.Log("该塔已损坏，无法传送");
            return;
        }
        StartCoroutine(DoTeleport());
    }

    public void TeleportToNextPlatform()
    {
        if (teleportPos.GetComponentInParent<DamageSystem>().GetCurState() == DamageState.DEATH)
        {
            Debug.Log("该塔已损坏，无法传送");
            return;
        }
        player.transform.position =  teleportPos.position;
        CheckPlatform();
    }

    private void CheckPlatform()
    {
        switch (name)
        {
            case "TeleportPointB":
                PlayerInPlatform.GetInstance().PlatformB();
                break;
            case "TeleportPointA":
                PlayerInPlatform.GetInstance().PlatformA();
                break;
            case "TeleportPointC":
                PlayerInPlatform.GetInstance().PlatformC();
                break;
        }
    }

    private IEnumerator DoTeleport()
    {
        player.transform.position = teleportPos.position;
        yield return null;
    }
}
