using UnityEngine;

namespace InternalAssets.Scripts.Map
{
    public class Item : MonoBehaviour, ISelect
    {
        public int price;

        public int GetItem()
        {
            Destroy(this.gameObject);
            return price;
        }

    }
}
