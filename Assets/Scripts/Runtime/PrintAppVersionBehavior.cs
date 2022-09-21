using TMPro;
using UnityEngine;

namespace AppVersioning
{
    [DisallowMultipleComponent]
    public class PrintAppVersionBehavior : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        
        private void Awake()
        {
            _text.text = Application.version;
        }
    }
}