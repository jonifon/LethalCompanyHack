using GameNetcodeStuff;
using LethalCompanyHack.Modules.Styles;
using Unity.Netcode;
using UnityEngine;

namespace LethalCompanyHack.Modules.Hacks
{
    public class TrollingTab : MonoBehaviour
    {
        private static bool isFakeInjured = false;

        public static void RenderTab()
        {
            RenderSetZeroCreditsButton();

            RenderFakeInjuredButton();

            RenderExplosionButton();

            RenderKillEveryoneButton();

            RenderStartGameButton();

            RenderShipLeaveButton();

            RenderBridgeFallButton();

            RenderGiveOwnershipButton();
        }

        private static void RenderSetZeroCreditsButton()
        {
            if (GUILayout.Button("Set 0 credits", Button.GetStandardButtonStyle()))
            {
                Terminal terminal = FindObjectOfType<Terminal>();
                terminal.groupCredits = 0;
                terminal.SyncGroupCreditsServerRpc(0, terminal.numberOfItemsInDropship);
            }
        }

        private static void RenderFakeInjuredButton()
        {
            string buttonText = isFakeInjured ? "Fake MakeCriticallyInjured [TOGGLED]" : "Fake MakeCriticallyInjured";

            if (GUILayout.Button(buttonText, Button.GetStandardButtonStyle()))
            {
                isFakeInjured = !isFakeInjured;
                PlayerControllerB playerController = FindObjectOfType<PlayerControllerB>();

                if (isFakeInjured)
                {
                    playerController.MakeCriticallyInjured(true);
                }
                else
                {
                    playerController.MakeCriticallyInjured(false);
                }
            }
        }

        private static void RenderExplosionButton()
        {
            if (GUILayout.Button("Explosion (for u only)", Button.GetStandardButtonStyle()))
            {
                Vector3 localPlayerPosition = FindObjectOfType<PlayerControllerB>().transform.position;
                Landmine.SpawnExplosion(localPlayerPosition, true, 0f, 0f);
            }
        }

        private static void RenderKillEveryoneButton()
        {
            if (GUILayout.Button("Kill Everyone", Button.GetStandardButtonStyle()))
            {
                foreach (PlayerControllerB player in FindObjectsOfType<PlayerControllerB>())
                {
                    player.DamagePlayerFromOtherClientServerRpc(99999, Vector3.zero, 0);
                }
            }
        }

        private static void RenderStartGameButton()
        {
            if (GUILayout.Button("Start game", Button.GetStandardButtonStyle()))
            {
                FindObjectOfType<StartOfRound>().StartGameServerRpc();
            }
        }

        private static void RenderShipLeaveButton()
        {
            if (GUILayout.Button("Ship leave", Button.GetStandardButtonStyle()))
            {
                FindObjectOfType<StartOfRound>().EndGameServerRpc(0);
            }
        }

        private static void RenderBridgeFallButton()
        {
            if (GUILayout.Button("Bridge fall", Button.GetStandardButtonStyle()))
            {
                FindObjectOfType<BridgeTrigger>().BridgeFallServerRpc();
            }
        }

        private static void RenderGiveOwnershipButton()
        {
            if (GUILayout.Button("Give ownership", Button.GetStandardButtonStyle()))
            {
                foreach (StartOfRound startOfRound in UnityEngine.Object.FindObjectsOfType<StartOfRound>())
                {
                    startOfRound.localPlayerController.GetComponent<NetworkObject>().ChangeOwnership(startOfRound.localPlayerController.actualClientId);
                }
            }
        }
    }
}