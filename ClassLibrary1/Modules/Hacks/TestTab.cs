using GameNetcodeStuff;
using LethalCompanyHack.Modules.Styles;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace LethalCompanyHack.Modules.Hacks
{
    public class TestTab : MonoBehaviour
    {
        private static Unity.Mathematics.Random random = new Unity.Mathematics.Random();

        private static bool isShipLightEnabled = false;

        public static void RenderTab()
        {
            RenderSpawnDeadBodyButton();

            RenderReviveButton();

            RenderExplodeMineButton();

            RenderShipLightButton();
        }

        public static void UpdateTab()
        {
            //Хуй знает че писать ёпта
        }

        private static void RenderSpawnDeadBodyButton()
        {
            if (GUILayout.Button("Spawn Dead Body/Effect", Button.GetStandardButtonStyle()))
            {
                PlayerControllerB localPlayerController = FindAnyObjectByType<PlayerControllerB>();
                var playerId = localPlayerController.playerClientId;
                Vector3 bodyVelocity = GameNetworkManager.Instance.localPlayerController.transform.position;
                localPlayerController.SpawnDeadBody((int)playerId, bodyVelocity, (int)CauseOfDeath.Unknown, GameNetworkManager.Instance.localPlayerController);
            }
        }

        private static void RenderReviveButton()
        {
            if (GUILayout.Button("Revive", Button.GetStandardButtonStyle()))
            {
                FindAnyObjectByType<StartOfRound>().ReviveDeadPlayers();
            }
        }

        private static void RenderExplodeMineButton()
        {
            if (GUILayout.Button("Explode Mine", Button.GetStandardButtonStyle()))
            {
                Landmine[] landmines = FindObjectsOfType<Landmine>();
                foreach (var landmine in landmines)
                {
                    landmine.ExplodeMineServerRpc();
                }
            }
        }

        private static void RenderShipLightButton()
        {
            string shipLightButtonText = isShipLightEnabled ? "Ship light [TOGGLED]" : "Ship light";

            if (GUILayout.Button(shipLightButtonText, Button.GetStandardButtonStyle()))
            {
                isShipLightEnabled = !isShipLightEnabled;
                FindAnyObjectByType<ShipLights>().SetShipLightsServerRpc(isShipLightEnabled);
            }
        }
    }
}