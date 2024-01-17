using GameNetcodeStuff;
using LethalCompanyHack.Modules.Styles;
using UnityEngine;

namespace LethalCompanyHack.Modules.Hacks
{
    public class MainTab : MonoBehaviour
    {
        private static bool isGodmodeEnabled = false;

        public static void RenderTab()
        {
            RenderTeleportButtons();
            RenderRegenerationButton();
            RenderHealthButtons();
            RenderGodmodeButton();
            RenderNightModeButton();
        }

        private static void RenderTeleportButtons()
        {
            if (GUILayout.Button("Teleport to structure", Button.GetStandardButtonStyle()))
            {
                new EntranceTeleport().TeleportPlayer();
            }

            if (GUILayout.Button("Teleport to ship", Button.GetStandardButtonStyle()))
            {
                PlayerControllerB localPlayer = FindAnyObjectByType<GameNetworkManager>().localPlayerController;
                Vector3 spawnPosition = FindAnyObjectByType<StartOfRound>().playerSpawnPositions[localPlayer.playerClientId].position;

                localPlayer.TeleportPlayer(spawnPosition, true, 160f, false, true);
            }
        }

        private static void RenderRegenerationButton()
        {
            if (GUILayout.Button("Regeneration", Button.GetStandardButtonStyle()))
            {
                PlayerControllerB localPlayer = FindAnyObjectByType<GameNetworkManager>().localPlayerController;
                localPlayer.DamagePlayerFromOtherClientServerRpc(-99, localPlayer.transform.position, 0);

                localPlayer.MakeCriticallyInjured(false);
            }
        }

        private static void RenderHealthButtons()
        {
            if (GUILayout.Button("-90hp", Button.GetStandardButtonStyle()))
            {
                PlayerControllerB localPlayer = FindAnyObjectByType<GameNetworkManager>().localPlayerController;
                localPlayer.DamagePlayerFromOtherClientServerRpc(90, localPlayer.transform.position, 0);
            }
        }

        private static void RenderGodmodeButton()
        {
            if (GUILayout.Button(isGodmodeEnabled ? "GodMode [TOGGLED]" : "Godmode", Button.GetStandardButtonStyle()))
            {
                isGodmodeEnabled = !isGodmodeEnabled;
                FindAnyObjectByType<StartOfRound>().allowLocalPlayerDeath = !isGodmodeEnabled;
            }
        }

        private static void RenderNightModeButton()
        {
            if (GUILayout.Button("Night mode", Button.GetStandardButtonStyle()))
            {
                SetNightVisionSettings(FindAnyObjectByType<GameNetworkManager>().localPlayerController);
                SetNightVisionSettings(FindObjectOfType<PlayerControllerB>());
            }
        }

        private static void SetNightVisionSettings(PlayerControllerB player)
        {
            player.nightVision.enabled = true;
            player.nightVision.intensity = 3000f;
            player.nightVision.range = 10000f;
        }
    }
}