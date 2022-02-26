using TMPro;
using UnityEngine;


namespace InternalAssets.Scripts.UI.Old.TopPanel
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