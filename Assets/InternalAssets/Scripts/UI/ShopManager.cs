using System.Collections;
using System.Collections.Generic;
using InternalAssets.Scripts.Inventory;
using UI;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public Inventory inventory;
    public GameObject content;
    public GameObject UImoney;

    private SnapScrolling snapScrolling;
    private int selectCardId;
    // Start is called before the first frame update
    void Start()
    {
        snapScrolling = content.GetComponent<SnapScrolling>();
    }
    public void Buy()
    {
        selectCardId = snapScrolling.GetSelectedCardId();
        if (inventory.AmountMoney >= inventory.items[selectCardId].price)
        {
            inventory.AmountMoney -= inventory.items[selectCardId].price;
            inventory.items[selectCardId].amount += 1;
        }
        else Debug.Log("Not money");
    }
    public void Sell()
    {
        selectCardId = snapScrolling.GetSelectedCardId();
        if (inventory.items[selectCardId].amount > 0)
        {
            inventory.AmountMoney += inventory.items[selectCardId].price;
            inventory.items[selectCardId].amount -= 1;
        }
        else Debug.Log("Not item");
    }

    public void ModifyHealth(float amount)
    {
        
    }

}
