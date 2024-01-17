using GameNetcodeStuff;
using LethalCompanyHack.Modules.Styles;
using UnityEngine;

namespace LethalCompanyHack.Modules.Hacks
{
    public class AdditionalTab : MonoBehaviour
    {
        private static bool isFastClimbEnabled = false;
        private static bool isSpeedHackEnabled = false;
        private static bool isInfStaminaEnabled = false;
        private static bool isSuperJumpEnabled = false;
        private static bool isAntiFOGEnabled = false;
        private static bool isCloakVisible = false;

        public static void RenderTab()
        {
            RenderCreditButtons();
            RenderToggleButtons();
        }

        public static void UpdateTab()
        {
            InfStamina();
            SetCloak();
        }

        private static void RenderCreditButtons()
        {
            RenderCreditButton("Add 500 credits", 500);
            RenderCreditButton("Add 50 credits", 50);
            RenderCreditButton("Take out 500 credits", -500);
        }

        private static void RenderCreditButton(string buttonText, int amount)
        {
            if (GUILayout.Button(buttonText, Button.GetStandardButtonStyle()))
            {
                Terminal terminal = FindObjectOfType<Terminal>();
                terminal.groupCredits += amount;
                terminal.SyncGroupCreditsServerRpc(terminal.groupCredits, terminal.numberOfItemsInDropship);
            }
        }

        private static void RenderToggleButtons()
        {
            RenderToggleButton("Fast climb", ref isFastClimbEnabled, 15f, 4f);
            RenderToggleButton("SpeedHack", ref isSpeedHackEnabled, 10.5f, 4.5f);
            RenderToggleButton("A lot of stamina", ref isInfStaminaEnabled);
            RenderToggleButton("Super Jump", ref isSuperJumpEnabled, 50f, 15f);
            RenderToggleButton("Anti FOG", ref isAntiFOGEnabled);
            RenderToggleButton("Cloak visible", ref isCloakVisible);
        }

        private static void RenderToggleButton(string buttonText, ref bool toggle, float onValue = 0f, float offValue = 0f)
        {
            if (GUILayout.Button(toggle ? $"{buttonText} [TOGGLED]" : buttonText, Button.GetStandardButtonStyle()))
            {
                toggle = !toggle;

                if (toggle)
                {
                    ApplyToggleValue(buttonText, onValue);
                }
                else
                {
                    ApplyToggleValue(buttonText, offValue);
                }
            }
        }

        private static void ApplyToggleValue(string buttonText, float value)
        {
            PlayerControllerB localPlayer = FindAnyObjectByType<GameNetworkManager>().localPlayerController;

            switch (buttonText)
            {
                case "Fast climb":
                    localPlayer.climbSpeed = value;
                    break;

                case "SpeedHack":
                    localPlayer.movementSpeed = value;
                    FindObjectOfType<PlayerControllerB>().movementSpeed = value;
                    break;

                case "Super Jump":
                    localPlayer.jumpForce = value;
                    FindAnyObjectByType<PlayerControllerB>().jumpForce = value;
                    break;
            }
        }

        private static void InfStamina()
        {
            if (!isInfStaminaEnabled)
                return;

            PlayerControllerB localPlayer = FindAnyObjectByType<GameNetworkManager>().localPlayerController;

            localPlayer.sprintMeter = 10f;
            localPlayer.sprintTime = 10f;
            FindAnyObjectByType<PlayerControllerB>().sprintMeter = 10f;
            FindAnyObjectByType<PlayerControllerB>().isExhausted = false;
            FindAnyObjectByType<PlayerControllerB>().sprintMeterUI.fillAmount = 1f;
            FindAnyObjectByType<PlayerControllerB>().sprintTime = 10f;
        }

        private static void SetCloak()
        {
            if (!isCloakVisible)
                return;

            HUDManager.Instance.SetClockVisible(true);
            FindAnyObjectByType<HUDManager>().SetClockVisible(true);
        }
    }
}