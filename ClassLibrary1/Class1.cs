using LethalCompanyHack.Modules.Hacks;
using LethalCompanyHack.Modules.Styles;
using System.Runtime.InteropServices;
using UnityEngine;

namespace LethalCompanyHack
{
    public class MainMenu : MonoBehaviour
    {
        private bool showMainMenu = true;

        public Rect mainMenuRect = new Rect(10f, 10f, 420f, 150f);
        public int mainMenuID = 1;

        private bool isDragging = false;
        private Vector2 offset;
        private const int VK_INSERT = 0x2D;

        private enum MenuCategory
        {
            Main,
            Additional,
            Trolling,
            Test,
            Players
        }

        private MenuCategory currentCategory = MenuCategory.Main;

        private void OnGUI()
        {
            if (showMainMenu)
            {
                HandleDrag();

                mainMenuRect = GUILayout.Window(mainMenuID, mainMenuRect, Menu_MainMenu, string.Empty, Menu.GetMenuStyle());
            }
        }

        private void Update()
        {
            if (GetAsyncKeyState(VK_INSERT))
            {
                showMainMenu = !showMainMenu;
            }

            AdditionalTab.UpdateTab();
            TestTab.UpdateTab();
        }

        private void HandleDrag()
        {
            GUI.DragWindow();

            if (Event.current.type == EventType.MouseDown && mainMenuRect.Contains(Event.current.mousePosition))
            {
                isDragging = true;
                offset = Event.current.mousePosition - new Vector2(mainMenuRect.x, mainMenuRect.y);
            }
            else if (Event.current.type == EventType.MouseUp)
            {
                isDragging = false;
            }

            if (isDragging)
            {
                mainMenuRect.x = Event.current.mousePosition.x - offset.x;
                mainMenuRect.y = Event.current.mousePosition.y - offset.y;
            }
        }

        private void Menu_MainMenu(int id)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("LethalCompanyHack", Menu.GetTitleStyle());
            GUILayout.EndHorizontal();

            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();

            foreach (MenuCategory category in Enum.GetValues(typeof(MenuCategory)))
            {
                if (GUILayout.Button(category.ToString(), Button.GetCategoryButtonStyle()))
                {
                    currentCategory = category;
                }
            }

            GUILayout.EndHorizontal();

            RenderTabForCategory();

            GUILayout.EndVertical();
            GUI.DragWindow();
        }

        private void RenderTabForCategory()
        {
            GUILayout.BeginVertical();

            switch (currentCategory)
            {
                case MenuCategory.Main:
                    MainTab.RenderTab();
                    break;

                case MenuCategory.Additional:
                    AdditionalTab.RenderTab();
                    break;

                case MenuCategory.Trolling:
                    TrollingTab.RenderTab();
                    break;

                case MenuCategory.Test:
                    TestTab.RenderTab();
                    break;

                case MenuCategory.Players:
                    PlayersTab.RenderTab();
                    break;
            }

            GUILayout.EndVertical();
        }

        [DllImport("User32.dll")]
        private static extern bool GetAsyncKeyState(int ArrowKeys);
    }
}