using System.Collections.Generic;
using InternalAssets.Scripts.Inventory;
using TMPro;
using UnityEngine;


namespace InternalAssets.Scripts.UI.Old.CircularScrollingList
{
    public class UpdateData : MonoBehaviour
    {
        [SerializeField] private Inventory.Inventory inventory;
        private List<GameObject> listWeaponCards;

        private void Start()
        {
            listWeaponCards = GetComponent<DownloadItemFromInventoryToUIConytol>().GetListWeaponCards();
           
        }

        public void OnEnable()
        {
            EventManager.StartListening("OnItemChange", UpdateItemsData);
        }
        public void OnDisable()
        {
            EventManager.StopListening("OnItemChange", UpdateItemsData);
        }
        
        public void UpdateItemsData()
        {
            for (int i = 0; i < listWeaponCards.Count; i++)
            {
                listWeaponCards[i].transform.Find("ItemAmount").transform.Find("Text").GetComponent<TextMeshProUGUI>().text =
                    inventory.Items[i].Amount.ToString();
            }

            Debug.Log("Update item in shop complite.");
        }
    }
}