namespace PJ.Easy.UI
{
    public static class UIExtensions
    {
        public static bool IsEquals(this UIBase screen, UIBase otherScreen)
        {
            if (otherScreen == null) return false;

            return screen.GetInstanceID() == otherScreen.GetInstanceID();
        }
    }
}
