using UnityEngine;

namespace LethalCompanyHack.Modules.Styles
{
    public static class Menu
    {
        public static GUIStyle GetMenuStyle()
        {
            Texture2D backgroundTexture = CreateTextureWithColor(new Color32(62, 62, 62, 100));
            GUIStyle menuStyle = new GUIStyle(GUIStyle.none);

            SetBackgroundTextures(menuStyle, backgroundTexture);

            menuStyle.overflow = new RectOffset(0, 0, 0, 0);

            return menuStyle;
        }

        public static GUIStyle GetTitleStyle()
        {
            GUIStyle titleStyle = new GUIStyle
            {
                normal = { textColor = Color.white },
                fontSize = 14,
                alignment = TextAnchor.MiddleCenter,
                padding = { top = 6, bottom = 5 }
            };

            return titleStyle;
        }

        private static Texture2D CreateTextureWithColor(Color color)
        {
            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, color);
            texture.Apply();
            return texture;
        }

        private static void SetBackgroundTextures(GUIStyle style, Texture2D texture)
        {
            style.normal.background = texture;
            style.hover.background = texture;
            style.active.background = texture;
            style.onActive.background = texture;
            style.onHover.background = texture;
            style.focused.background = texture;
            style.onFocused.background = texture;
        }
    }
}