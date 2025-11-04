using System.Collections.Generic;
using UnityEngine;
using PJ.Easy.Utils;

namespace PJ.Easy.UI
{
    public static class UIController
    {
        private static Dictionary<System.Type, UIBase> currentScreens = new Dictionary<System.Type, UIBase>();
        private static Stack<UIBase> stackedScreens = new Stack<UIBase>();
        private static UIBase activeScreen;

        private static T FindScreen<T>() where T : UIBase
        {
            if (currentScreens.ContainsKey(typeof(T)))
            {
                if (currentScreens[typeof(T)] == null)
                {
                    currentScreens.Remove(typeof(T));
                    return FindScreen<T>();
                }
                else
                    return (T)currentScreens[typeof(T)];
            }
            else
            {
                T screen = Object.FindAnyObjectByType<T>();
                if (screen != null)
                {
                    currentScreens.Add(typeof(T), screen);
                    return screen;
                }
            }
            Log(FIND_SCREEN, $"Can not find screen of type {typeof(T)}. Make sure it's scene object is reference");
            return null;
        }

        public static T ShowScreen<T>() where T : UIBase
        {
            T screen = FindScreen<T>();
            screen.OnShowScreen();

            if (screen != null)
            {
                activeScreen = screen;
                if (activeScreen.IsStackable)
                {
                    if (stackedScreens.TryPeek(out UIBase topScreen))
                    {
                        if (!activeScreen.IsEquals(topScreen))
                            stackedScreens.Push(activeScreen);
                    }
                    else
                    {
                        stackedScreens.Push(activeScreen);
                    }
                }
            }

            Log(SHOW_SCREEN, $"{screen} | Active Screen : {activeScreen} | Stacked Screens Count : {stackedScreens.Count}".ToCyan());
            return screen;
        }

        public static T HideScreen<T>() where T : UIBase
        {
            T screen = FindScreen<T>();
            screen.OnHideScreen();

            if (screen != null)
            {
                if (stackedScreens.TryPeek(out UIBase topScreen) && screen.IsEquals(topScreen))
                    stackedScreens.Pop();
                if (stackedScreens.TryPeek(out UIBase newTopScreen))
                    activeScreen = newTopScreen;
            }

            Log(HIDE_SCREEN, $"{screen} | Active Screen : {activeScreen} | Stacked Screens Count : {stackedScreens.Count}".ToCyan());
            return screen;
        }

        public static T GetScreen<T>() where T : UIBase
        {
            return FindScreen<T>();
        }

        public static bool IsActiveScreen(UIBase screen)
        {
            if (activeScreen == null) return false;

            return screen.GetInstanceID() == activeScreen.GetInstanceID();
        }

        #region Logs
        private static bool loggingEnabled = true;
        private const string TAG = "[ScreenService]";
        private const string FIND_SCREEN = "FindScreen";
        private const string SHOW_SCREEN = "ShowScreen";
        private const string HIDE_SCREEN = "HideScreen";

        internal static void Log(string action, object message)
        {
            if (!loggingEnabled) return;
            Debug.Log($"{TAG} [{action}] | {message}");
        }

        public static void SetLogging(bool enabled)
        {
            loggingEnabled = enabled;
        }
        #endregion
    }
}