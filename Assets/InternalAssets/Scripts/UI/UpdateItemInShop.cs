using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UpdateItemInShop : MonoBehaviour
{
    public Inventory inventory;
    public RectTransform content;

    private List<GameObject> items;
    // Update is called once per frame
    void Update()
    {
        items = content.GetComponent<DownloadItemInShop>().GetWeaponCards();
    }
    public void UpdateItemsInShop()
    {
        for (int i = 0; i < items.Count; i++)
        {
            //items[i].transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = inventory.items[i].name;

            //items[i].transform.Find("ItemIcon").GetComponent<Image>().sprite = inventory.items[i].icon;

            //items[i].transform.Find("ItemPrice").transform.Find("Text").GetComponent<TextMeshProUGUI>().text = inventory.items[i].price.ToString() + " $";

            items[i].transform.Find("ItemAmount").transform.Find("Text").GetComponent<TextMeshProUGUI>().text = inventory.items[i].amount.ToString();
        }

        Debug.Log("Update item in shop complite.");
    }
}
