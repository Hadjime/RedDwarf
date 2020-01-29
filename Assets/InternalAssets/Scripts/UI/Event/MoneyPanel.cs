using TMPro;
using UnityEngine;

namespace InternalAssets.Scripts.UI.Event
{
    //[RequireComponent(typeof(TextMeshProUGUI))]
    public class MoneyPanel : MonoBehaviour
    {
        public Inventory playerInventory;
        public TextMeshProUGUI textMeshProUgui;
        
        private TextMeshProUGUI moneyTextMeshProUgui;
        private void Awake()
        {
            //moneyTextMeshProUgui = GetComponent<TextMeshProUGUI>();
            //moneyTextMeshProUgui.text = playerInventory.AmountMoney.ToString();
            HandleMoneyChange();
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
            textMeshProUgui.text = playerInventory.AmountMoney.ToString();
        }
    }
}