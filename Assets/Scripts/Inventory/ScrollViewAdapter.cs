using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollViewAdapter : MonoBehaviour
{
    public Inventory_2 inventory;
    public RectTransform prefab;
    public RectTransform content;

    private void Start()
    {
        UpdateItems();
    }
    public void UpdateItems()
    {
        int numberCard = 0;
        foreach(var item in inventory.items)
        {
            numberCard++;
            var instance = Instantiate(prefab.gameObject);
            instance.transform.SetParent(content, false);
            instance.name = "CardWeapon_" + numberCard;
            InitializeItemFromScriptableObject(instance, item);
        }
        Debug.Log("Generate item complite.");
    }
    
    void InitializeItemFromScriptableObject(GameObject viewGameObject, InventoryItem_2 item)
    {
        Transform nameWeapon = viewGameObject.transform.Find("NameWeapon");
        nameWeapon.GetComponent<TextMeshProUGUI>().text = item.name;

        Transform iconWeapon = viewGameObject.transform.Find("IconWeapon");
        iconWeapon.GetComponent<Image>().sprite = item.icon;

        Transform priceWeapon = viewGameObject.transform.Find("PriceWeapon").transform.Find("Text");
        priceWeapon.GetComponent<TextMeshProUGUI>().text = item.price.ToString() + " $";

        Transform amountWeapon = viewGameObject.transform.Find("AmountWeapon");
        amountWeapon.GetComponent<TextMeshProUGUI>().text = 0.ToString();
    }
}
