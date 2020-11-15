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

            var item = other.GetComponent<ISelect>();
            if (item != null)
            {
                inventory.AmountMoney += item.GetItem();
            }
        }
    }
}
