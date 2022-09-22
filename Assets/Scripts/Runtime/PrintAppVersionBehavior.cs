using TMPro;
using UnityEngine;

namespace AppVersioning
{
    [DisallowMultipleComponent]
    public class PrintAppVersionBehavior : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private TMP_Text _fullText;
        [SerializeField] private AppVersionHolder _appVersionHolder;
        
        private void Awake()
        {
            _text.text = Application.version;
            _fullText.text = _appVersionHolder.AppVersion.BuildString(appendCommitHash: true, appendBranch: true);
        }
    }
}