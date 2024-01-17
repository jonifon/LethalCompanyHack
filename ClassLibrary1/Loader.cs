using UnityEngine;

namespace LethalCompanyHack
{
    internal class Loader
    {
        public static GameObject MainClass;

        public static void Load()
        {
            Loader.MainClass = new GameObject("fl_Main");
            MainClass.AddComponent<MainMenu>();
            UnityEngine.Object.DontDestroyOnLoad(Loader.MainClass);
        }
    }
}