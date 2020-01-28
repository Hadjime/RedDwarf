using TMPro;
using UnityEngine;

namespace UI.Event
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class PickAxeChangeEvent : MonoBehaviour
    {
        public Inventory inventory;
        
        private TextMeshProUGUI pickAxeTextMeshProUgui;
        private void Awake()
        {
            pickAxeTextMeshProUgui = GetComponent<TextMeshProUGUI>();
            pickAxeTextMeshProUgui.text = inventory.AmountPickAxe.ToString();
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