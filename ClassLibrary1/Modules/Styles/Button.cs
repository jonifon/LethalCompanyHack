using UnityEngine;

namespace LethalCompanyHack.Modules.Styles
{
    public static class Button
    {
        private static readonly Color32 DarkButtonColor = new Color32(6, 6, 6, 204);
        private static readonly Color32 HoveredTextColor = new Color32(180, 44, 114, 255);

        public static GUIStyle GetCategoryButtonStyle()
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            SetButtonStyle(buttonStyle, DarkButtonColor, HoveredTextColor);
            return buttonStyle;
        }

        public static GUIStyle GetStandardButtonStyle()
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            SetButtonStyle(buttonStyle, new Color32(14, 14, 14, 204), HoveredTextColor);
            return buttonStyle;
        }

        private static void SetButtonStyle(GUIStyle style, Color32 backgroundColor, Color32 hoveredTextColor)
        {
            Texture2D backgroundTexture = CreateTextureWithColor(backgroundColor);
            style.normal.background = backgroundTexture;
            style.hover.background = null;
            style.active.background = null;

            style.normal.textColor = Color.white;
            style.hover.textColor = hoveredTextColor;
            style.onHover.textColor = style.hover.textColor;

            style.fontSize = 14;
            style.border = new RectOffset(0, 0, 0, 0);
        }

        private static Texture2D CreateTextureWithColor(Color color)
        {
            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, color);
            texture.Apply();
            return texture;
        }
    }
}