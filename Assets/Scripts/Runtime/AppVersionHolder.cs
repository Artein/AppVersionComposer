using UnityEngine;

namespace AppVersioning
{
    public partial class AppVersionHolder : ScriptableObject, IAppVersionProvider
    {
        [SerializeField] private AppVersionData _appVersion;
        [SerializeField] private int _editor_buildPreprocessorOrder;

        public AppVersionData AppVersion => _appVersion;
        public int Editor_BuildPreprocessorOrder => _editor_buildPreprocessorOrder;
    }
}