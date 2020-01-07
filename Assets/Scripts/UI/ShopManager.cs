using System.Collections;
using System.Collections.Generic;
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
        selectCardId = snapScrolling.GetSelectedCardID();
        if (inventory.AmountMoney >= inventory.items[selectCardId].price)
        {
            inventory.AmountMoney -= inventory.items[selectCardId].price;
            inventory.items[selectCardId].amount += 1;
        }
        else Debug.Log("Not money");
    }
    public void Sell()
    {
        selectCardId = snapScrolling.GetSelectedCardID();
        if (inventory.items[selectCardId].amount > 0)
        {
            inventory.AmountMoney += inventory.items[selectCardId].price;
            inventory.items[selectCardId].amount -= 1;
        }
        else Debug.Log("Not item");
    }


}
