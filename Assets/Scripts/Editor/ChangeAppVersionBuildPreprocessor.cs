using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace AppVersioning.Editor
{
    public class ChangeAppVersionBuildPreprocessor : IPreprocessBuildWithReport
    {
        int IOrderedCallback.callbackOrder => 0;
        
        void IPreprocessBuildWithReport.OnPreprocessBuild(BuildReport report)
        {
            var buildVersion = AppVersionComposer.BuildVersion;
            AppVersionHolder.Editor_Instance.Editor_SetAppVersion(buildVersion);
            PlayerSettings.bundleVersion = buildVersion;
        }
    }
}