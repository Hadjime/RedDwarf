using TMPro;
using UnityEngine;

namespace InternalAssets.Scripts.UI.Event
{
    //[RequireComponent(typeof(TextMeshProUGUI))]
    public class PickAxePanel : MonoBehaviour
    {
        public Inventory.Inventory inventory;
        
        public TextMeshProUGUI pickAxeTMPtext;
        private void Start()
        {
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
            pickAxeTMPtext.text = inventory.AmountPickAxe.ToString();
        }
    }
}