using UnityEditor;

namespace AppVersioning.Editor
{
    public static class AppVersionWindow
    {
        [MenuItem("Game/Show App Version")]
        private static void ShowWindow()
        {
            var appVersion = AppVersionComposer.Version;
            AppVersionHolder.Editor_Instance.Editor_SetAppVersion(appVersion);
            
            EditorUtility.DisplayDialog("App version", appVersion.BuildString(), "Ok");
        }
    }
}