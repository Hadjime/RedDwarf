using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InternalAssets.Scripts.Inventory
{
    public class DownloadItemInShop : MonoBehaviour
    {
        public global::InternalAssets.Scripts.Inventory.Inventory inventory;
        public RectTransform prefab;
        public RectTransform content;

        private List<GameObject> listWeaponCards = new List<GameObject>();
    
        public List<GameObject> GetListWeaponCards()
        {
            return listWeaponCards;
        }
    
        private void Start()
        {
            DownloadItemsInShop();
        }
        
        private void DownloadItemsInShop()
        {
            int numberCard = 0;
            foreach(var item in inventory.items)
            {
                numberCard++;
                GameObject instance = Instantiate(prefab.gameObject, content, false);
                listWeaponCards.Add(instance);
                instance.name = "CardWeapon_" + numberCard;
                InitializeCardFromInventory(instance, item);
            }
            Debug.Log("Download item in shop complite.");
        }
        
        private void InitializeCardFromInventory(GameObject viewGameObject, InventoryItem item)
        {
            Transform itemName = viewGameObject.transform.Find("ItemName");
            itemName.GetComponent<TextMeshProUGUI>().text = item.name;

            Transform itemIcon = viewGameObject.transform.Find("ItemIcon");
            itemIcon.GetComponent<Image>().sprite = item.icon;

            Transform itemPrice = viewGameObject.transform.Find("ItemPrice").transform.Find("Text");
            //itemPrice.GetComponent<TextMeshProUGUI>().text = item.price.ToString() + " $";  //$ после цифр
            itemPrice.GetComponent<TextMeshProUGUI>().text =  "$" + item.price.ToString();    //$ перед цифр

            Transform itemAmount = viewGameObject.transform.Find("ItemAmount").transform.Find("Text");
            itemAmount.GetComponent<TextMeshProUGUI>().text = item.amount.ToString();
        }
    }
}
