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
        StartCoroutine(DoTeleport());
    }

    public void TeleportToNextPlatform()
    {
        Vector3 pos = teleportPos.position;
        player.transform.position = pos;
    }

    private IEnumerator DoTeleport()
    {
        player.transform.position = teleportPos.position;
        yield return null;
    }
}
