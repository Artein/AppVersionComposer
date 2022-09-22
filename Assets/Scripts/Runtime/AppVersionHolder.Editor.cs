#if UNITY_EDITOR

using System.IO;
using UnityEditor;
using UnityEngine;

namespace AppVersioning
{
    public partial class AppVersionHolder
    {
        private static AppVersionHolder _instance;
        
        public static AppVersionHolder Editor_Instance
        {
            get
            {
                const string assetPath = "Assets/Resources/AppVersionHolder.asset";
                if (_instance == null)
                {
                    _instance = AssetDatabase.LoadAssetAtPath<AppVersionHolder>(assetPath);
                }

                if (_instance == null)
                {
                    Debug.Log($"Didn't find 'assetPath'. Creating a new one..");

                    var resourcesFullPath = Path.Combine(Application.dataPath, "Resources");
                    if (!Directory.Exists(resourcesFullPath))
                    {
                        Directory.CreateDirectory(resourcesFullPath);
                    }
                    _instance = CreateInstance(assetPath);
                }

                return _instance;
            }
        }

        public void Editor_SetAppVersion(AppVersionData appVersionData)
        {
            _appVersion = appVersionData;
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssetIfDirty(this);
        }

        private new static AppVersionHolder CreateInstance(string path)
        {
            var asset = ScriptableObject.CreateInstance<AppVersionHolder>();
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssetIfDirty(asset);
            return asset;
        }
    }
}

#endif // UNITY_EDITOR