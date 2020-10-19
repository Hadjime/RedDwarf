using UnityEngine;

namespace InternalAssets.Scripts.Inventory
{
    public class DownloadItemFromInventoryToShop: DownloadItemInBase
    {
        public override void RotateCardItem(GameObject instance)
        {
            instance.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}