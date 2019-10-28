using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerShop : MonoBehaviour
{
    public Inventory inventory;
    public GameObject content;
    public GameObject UImoney;

    private SnapScrolling SnapScrolling;
    private int selectCardID;
    // Start is called before the first frame update
    void Start()
    {
        SnapScrolling = content.GetComponent<SnapScrolling>();
    }
    public void Buy()
    {
        selectCardID = SnapScrolling.GetSelectedCardID();
        if (inventory.amountMoney >= inventory.items[selectCardID].price)
        {
            inventory.amountMoney -= inventory.items[selectCardID].price;
            inventory.items[selectCardID].amount += 1;
        }
        else Debug.Log("Not money");
    }
    public void Sell()
    {
        selectCardID = SnapScrolling.GetSelectedCardID();
        if (inventory.items[selectCardID].amount > 0)
        {
            inventory.amountMoney += inventory.items[selectCardID].price;
            inventory.items[selectCardID].amount -= 1;
        }
        else Debug.Log("Not item");
    }


}
