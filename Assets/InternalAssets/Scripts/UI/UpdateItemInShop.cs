using System.Collections.Generic;
using InternalAssets.Scripts.Inventory;
using TMPro;
using UnityEngine;

namespace InternalAssets.Scripts.UI
{
    public class UpdateItemInShop : MonoBehaviour
    {
        public Inventory.Inventory inventory;
        public RectTransform content;

        private List<GameObject> _items;

        private DownloadItemInShop _downloadItemInShop;

        // Update is called once per frame
        private void Start()
        {
            _downloadItemInShop = content.GetComponent<DownloadItemInShop>();
            _items = _downloadItemInShop.GetListWeaponCards();
        }

        private void Update()
        {
            
        }
        public void UpdateItemsData()
        {
            for (int i = 0; i < _items.Count; i++)
            {
                //items[i].transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = inventory.items[i].name;

                //items[i].transform.Find("ItemIcon").GetComponent<Image>().sprite = inventory.items[i].icon;

                //items[i].transform.Find("ItemPrice").transform.Find("Text").GetComponent<TextMeshProUGUI>().text = inventory.items[i].price.ToString() + " $";

                _items[i].transform.Find("ItemAmount").transform.Find("Text").GetComponent<TextMeshProUGUI>().text = inventory.items[i].amount.ToString();
            }

            Debug.Log("Update item in shop complite.");
        }
    }
}
