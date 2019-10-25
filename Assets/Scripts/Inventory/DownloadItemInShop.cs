using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DownloadItemInShop : MonoBehaviour
{
    public Inventory inventory;
    public RectTransform prefab;
    public RectTransform content;

    private List<GameObject> instWeaponCards = new List<GameObject>();
    private void Awake()
    {
        UpdateItemsInShop();
    }
    public List<GameObject> GetWeaponCards()
    {
        return instWeaponCards;
    }
    public void UpdateItemsInShop()
    {
        int numberCard = 0;
        foreach(var item in inventory.items)
        {
            numberCard++;
            GameObject instance = Instantiate(prefab.gameObject);
            instWeaponCards.Add(instance);
            instance.transform.SetParent(content, false);
            instance.name = "CardWeapon_" + numberCard;
            InitializeItemFromScriptableObject(instance, item);
        }
        Debug.Log("Download item in shop complite.");
    }
    
    private void InitializeItemFromScriptableObject(GameObject viewGameObject, InventoryItem item)
    {
        Transform itemName = viewGameObject.transform.Find("ItemName");
        itemName.GetComponent<TextMeshProUGUI>().text = item.name;

        Transform itemIcon = viewGameObject.transform.Find("ItemIcon");
        itemIcon.GetComponent<Image>().sprite = item.icon;

        Transform itemPrice = viewGameObject.transform.Find("ItemPrice").transform.Find("Text");
        itemPrice.GetComponent<TextMeshProUGUI>().text = item.price.ToString() + " $";

        Transform itemAmount = viewGameObject.transform.Find("ItemAmount").transform.Find("Text");
        itemAmount.GetComponent<TextMeshProUGUI>().text = item.amount.ToString();
    }
}
