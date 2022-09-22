using UnityEngine;

namespace AppVersioning
{
    public partial class AppVersionHolder : ScriptableObject, IAppVersionProvider
    {
        [SerializeField] private string _appVersion;

        public string AppVersion => _appVersion;
    }
}