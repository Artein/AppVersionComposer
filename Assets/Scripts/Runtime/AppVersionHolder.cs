using UnityEngine;

namespace AppVersioning
{
    public partial class AppVersionHolder : ScriptableObject
    {
        [SerializeField] private string _appVersion;

        public string AppVersion => _appVersion;
    }
}