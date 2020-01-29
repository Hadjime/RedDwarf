using TMPro;
using UnityEngine;

namespace InternalAssets.Scripts.UI.Event
{
    //[RequireComponent(typeof(TextMeshProUGUI))]
    public class PickAxePanel : MonoBehaviour
    {
        public Inventory inventory;
        
        public TextMeshProUGUI pickAxeTextMeshProUgui;
        private void Awake()
        {
            //pickAxeTextMeshProUgui = GetComponent<TextMeshProUGUI>();
            //pickAxeTextMeshProUgui.text = inventory.AmountPickAxe.ToString();
            HandlePickAxeChange();
        }

        private void OnEnable()
        {
            EventManager.StartListening("OnPickAxeChange", HandlePickAxeChange);
        }

        private void OnDisable()
        {
            EventManager.StopListening("OnPickAxeChange", HandlePickAxeChange);
        }

        private void HandlePickAxeChange()
        {
            pickAxeTextMeshProUgui.text = inventory.AmountPickAxe.ToString();
        }
    }
}