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
            EventManager.StartListening("OnMoneyChange", HandleMoneyChange);
        }

        private void OnDisable()
        {
            EventManager.StopListening("OnMoneyChange", HandleMoneyChange);
        }

        private void HandleMoneyChange()
        {
            moneyTextMeshProUgui.text = inventory.AmountMoney.ToString();
        }
    }
}