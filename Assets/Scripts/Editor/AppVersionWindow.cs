using UnityEditor;

namespace AppVersioning.Editor
{
    public static class AppVersionWindow
    {
        [MenuItem("Game/Show App Version")]
        private static void ShowWindow()
        {
            var buildVersion = AppVersionComposer.BuildVersion;
            AppVersionHolder.Editor_Instance.Editor_SetAppVersion(buildVersion);
            EditorUtility.DisplayDialog("App version", buildVersion, "Ok");
        }
    }
}