using System.Collections.Generic;
using InternalAssets.Scripts.Inventory.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InternalAssets.Scripts.Inventory
{
    public abstract class DownloadItemInBase : MonoBehaviour
    {
        public Inventory inventory;
        public RectTransform prefabCard;

        private List<GameObject> listWeaponCards = new List<GameObject>();
        private int _numberCard;

        public List<GameObject> GetListWeaponCards()
        {
            return listWeaponCards;
        }
    
        private void Awake()
        {
            DownloadItems();
        }
        private void Update()
        {
            
        }
        private void DownloadItems()
        {
            _numberCard = 0;
            foreach(var item in inventory.Items)
            {
                var instance = CreateAndSetNamePrefab(_numberCard);
                listWeaponCards.Add(instance);
                RotateCardItem(instance);
                InitializeCardFromInventory(instance, item);
                _numberCard++;
            }
            Debug.Log("Download item in shop complite.");
        }

        private GameObject CreateAndSetNamePrefab(int numberCard)
        {
            GameObject instance = Instantiate(prefabCard.gameObject, this.transform, false);
            instance.name = "CardWeapon_" + numberCard;
            return instance;
        }

        public abstract void RotateCardItem(GameObject instance);

        private void InitializeCardFromInventory(GameObject viewGameObject, IItem item)
        {
            Transform itemName = viewGameObject.transform.Find("ItemName");
            itemName.GetComponent<TextMeshProUGUI>().text = item.Name;

            Transform itemIcon = viewGameObject.transform.Find("ItemIcon");
            itemIcon.GetComponent<Image>().sprite = item.Icon;

            Transform itemPrice = viewGameObject.transform.Find("ItemPrice").transform.Find("Text");
            //itemPrice.GetComponent<TextMeshProUGUI>().text = item.price.ToString() + " $";  //$ после цифр
            itemPrice.GetComponent<TextMeshProUGUI>().text =  "$" + item.Price.ToString();    //$ перед цифр

            Transform itemAmount = viewGameObject.transform.Find("ItemAmount").transform.Find("Text");
            itemAmount.GetComponent<TextMeshProUGUI>().text = item.Amount.ToString();
        }

        public void UpdateItemsData()
        {
            for (int i = 0; i < listWeaponCards.Count; i++)
            {
                //items[i].transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = inventory.items[i].name;

                //items[i].transform.Find("ItemIcon").GetComponent<Image>().sprite = inventory.items[i].icon;

                //items[i].transform.Find("ItemPrice").transform.Find("Text").GetComponent<TextMeshProUGUI>().text = inventory.items[i].price.ToString() + " $";

                listWeaponCards[i].transform.Find("ItemAmount").transform.Find("Text").GetComponent<TextMeshProUGUI>().text = inventory.Items[i].Amount.ToString();
            }

            Debug.Log("Update item in shop complite.");
        }
    }
}
