using TMPro;
using UnityEngine;

namespace UI.Event
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class MoneyChangeEvent : MonoBehaviour
    {
        public Inventory inventory;
        
        private TextMeshProUGUI moneyTextMeshProUgui;
        private void Awake()
        {
            moneyTextMeshProUgui = GetComponent<TextMeshProUGUI>();
            moneyTextMeshProUgui.text = inventory.AmountMoney.ToString();
        }

        private void OnEnable()
        {
            EventManager.StartListening("MoneyChange", MoneyChange);
        }

        private void OnDisable()
        {
            EventManager.StopListening("MoneyChange", MoneyChange);
        }

        private void MoneyChange()
        {
            moneyTextMeshProUgui.text = inventory.AmountMoney.ToString();
        }
    }
}