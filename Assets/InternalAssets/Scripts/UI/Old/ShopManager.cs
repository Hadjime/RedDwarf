using UnityEngine;


namespace InternalAssets.Scripts.UI.Old
{
    public class ShopManager : MonoBehaviour
    {
        public Inventory.Inventory inventory;
        public GameObject content;
        public GameObject UImoney;

        private SnapScrolling snapScrolling;
        private int selectCardId;

        void Start()
        {
            snapScrolling = content.GetComponent<SnapScrolling>();
        }
        public void Buy()
        {
            selectCardId = snapScrolling.GetSelectedCardId();
            if (inventory.AmountMoney >= inventory.Items[selectCardId].Price)
            {
                inventory.AmountMoney -= inventory.Items[selectCardId].Price;
                inventory.Items[selectCardId].Amount += 1;
            }
            else Debug.Log("Not money");
        }
        public void Sell()
        {
            selectCardId = snapScrolling.GetSelectedCardId();
            if (inventory.Items[selectCardId].Amount > 0)
            {
                inventory.AmountMoney += inventory.Items[selectCardId].Price;
                inventory.Items[selectCardId].Amount -= 1;
            }
            else Debug.Log("Not item");
        }

        public void ModifyHealth(float amount)
        {
        
        }

    }
}
