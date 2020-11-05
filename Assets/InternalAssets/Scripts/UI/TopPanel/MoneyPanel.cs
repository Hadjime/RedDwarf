using TMPro;
using UnityEngine;
using InternalAssets.Scripts.Inventory;

namespace InternalAssets.Scripts.UI.Event
{
    //[RequireComponent(typeof(TextMeshProUGUI))]
    public class MoneyPanel : MonoBehaviour
    {
        public Inventory.Inventory playerInventory;
        public TextMeshProUGUI goldTMPtext;
        
        private void Awake()
        {
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
            goldTMPtext.text = playerInventory.AmountMoney.ToString();
        }
    }
}