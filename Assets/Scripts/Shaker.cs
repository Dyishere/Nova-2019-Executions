using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class Shaker : MonoBehaviour
    {
        private Player player = null;

        public void RightHandShake() {
            player = Player.instance;

            if (player == null)
            {
                Debug.LogError("<b>[SteamVR Interaction]</b> Teleport: No Player instance found in map.");
                Destroy(gameObject);
                return;
            }

            player.rightHand.TriggerHapticPulse(500);
        }
        public void LeftHandShake()
        {
            player = Player.instance;

            if (player == null)
            {
                Debug.LogError("<b>[SteamVR Interaction]</b> Teleport: No Player instance found in map.");
                Destroy(gameObject);
                return;
            }

            player.leftHand.TriggerHapticPulse(500);
        }
    }
}