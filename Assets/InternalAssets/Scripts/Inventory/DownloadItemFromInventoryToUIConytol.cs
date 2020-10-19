using UnityEngine;

namespace InternalAssets.Scripts.Inventory
{
    public class DownloadItemFromInventoryToUIConytol: DownloadItemInBase
    {
        public override void RotateCardItem(GameObject instance)
        {
            instance.transform.rotation = Quaternion.Euler(0f, -45f, 0f);
        }
    }
}