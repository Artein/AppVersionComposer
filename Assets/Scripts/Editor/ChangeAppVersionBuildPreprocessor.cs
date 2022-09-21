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
            PlayerSettings.bundleVersion = AppVersionComposer.BuildVersion;
        }
    }
}