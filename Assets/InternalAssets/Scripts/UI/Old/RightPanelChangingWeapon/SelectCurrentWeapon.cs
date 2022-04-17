using AirFishLab.ScrollingList;
using UnityEngine;
using UnityEngine.EventSystems;


namespace InternalAssets.Scripts.UI.Old.RightPanelChangingWeapon
{
    public class SelectCurrentWeapon : EventTrigger
    {
        [SerializeField] public ListPositionCtrl listPositionCtrl;

        private void Start()
        {
            listPositionCtrl = GetComponent<ListPositionCtrl>();
        }

        public override void OnEndDrag(PointerEventData data)
        {
            EventManager.TriggerEventWithOneParametr("ChangingCurrentWeapon", listPositionCtrl.GetCenteredBox().listBoxID);
            Debug.Log("Changing weapon");
        }
    }
}