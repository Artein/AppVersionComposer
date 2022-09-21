using UnityEditor;

namespace Editor
{
    public static class AppVersionWindow
    {
        [MenuItem("Game/Show App Version")]
        private static void ShowWindow()
        {
            EditorUtility.DisplayDialog("App version", AppVersionComposer.BuildVersion, "Ok");
        }
    }
}