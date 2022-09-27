using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace AppVersioning.Editor
{
    public class ChangeAppVersionBuildPreprocessor : IPreprocessBuildWithReport
    {
        int IOrderedCallback.callbackOrder => AppVersionHolder.Editor_Instance.Editor_BuildPreprocessorOrder;
        
        void IPreprocessBuildWithReport.OnPreprocessBuild(BuildReport report)
        {
            var appVersion = AppVersionComposer.Version;
            AppVersionHolder.Editor_Instance.Editor_SetAppVersion(appVersion);

            string appVersionStr;
            if (report.summary.platform == BuildTarget.iOS)
            {
                // iOS supports only SEMVER (https://semver.org/)
                appVersionStr = appVersion.BuildString(false, false);
            }
            else
            {
                if (report.summary.options.HasFlag(BuildOptions.Development))
                {
                    appVersionStr = appVersion.BuildString();
                }
                else
                {
                    appVersionStr = appVersion.BuildString(appendCommitHash: true, appendBranch: false);
                }
            }
            PlayerSettings.bundleVersion = appVersionStr;
        }
    }
}