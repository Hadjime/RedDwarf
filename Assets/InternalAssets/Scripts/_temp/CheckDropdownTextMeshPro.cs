using System;
using TMPro;
using UnityEngine;

namespace InternalAssets.Scripts._temp
{
    public class CheckDropdownTextMeshPro : MonoBehaviour
    {
        private TMP_Dropdown dropdown;

        private void Start()
        {
            dropdown = GetComponent<TMP_Dropdown>();
        }

        public void Update()
        {
            var dOptions = dropdown.value;
            Debug.Log("Value = " + dOptions);
        }
    }
}