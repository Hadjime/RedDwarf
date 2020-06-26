using InternalAssets.Scripts.Map;
using UnityEngine;
using InternalAssets.Scripts.Inventory;

namespace InternalAssets.Scripts.Player
{
    public class TakeItems : MonoBehaviour
    {
        public Inventory.Inventory inventory;
        public void OnTriggerEnter2D(Collider2D other)
        {

            var item2 = other.GetComponent<ISelect>();
            if (item2 != null)
            {
                inventory.AmountMoney += item2.GetItem();
            }
        }
    }
}
