using GameNetcodeStuff;
using LethalCompanyHack.Modules.Styles;
using UnityEngine;

namespace LethalCompanyHack.Modules.Hacks
{
    internal class PlayersTab : MonoBehaviour
    {
        public static void RenderTab()
        {
            GUILayout.Label("GAMASENSE PLAYER LIST:");

            foreach (PlayerControllerB player in FindObjectsOfType<PlayerControllerB>())
            {
                RenderPlayerInfo(player);
            }
        }

        private static void RenderPlayerInfo(PlayerControllerB player)
        {
            GUILayout.BeginHorizontal();

            RenderPlayerName(player);
            RenderTeleportButton(player);
            RenderKillButton(player);
            RenderSoundButton(player);

            GUILayout.EndHorizontal();
        }

        private static void RenderPlayerName(PlayerControllerB player)
        {
            GUILayout.Label($"{player.playerUsername}");
        }

        private static void RenderTeleportButton(PlayerControllerB player)
        {
            if (GUILayout.Button("Teleport to player", Button.GetStandardButtonStyle()))
            {
                Vector3 pos = player.transform.position;
                FindAnyObjectByType<GameNetworkManager>().localPlayerController.TeleportPlayer(pos, true, 160f, false, true);
            }
        }

        private static void RenderKillButton(PlayerControllerB player)
        {
            if (GUILayout.Button("Kill", Button.GetStandardButtonStyle()))
            {
                player.DamagePlayerFromOtherClientServerRpc(9999, player.transform.position, 0);
            }
        }

        private static void RenderSoundButton(PlayerControllerB player)
        {
            if (GUILayout.Button("Sound", Button.GetStandardButtonStyle()))
            {
                player.DamagePlayerFromOtherClientServerRpc(0, player.transform.position, 0);
            }
        }
    }
}