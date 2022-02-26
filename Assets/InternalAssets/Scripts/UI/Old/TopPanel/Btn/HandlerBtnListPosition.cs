using TMPro;
using UnityEngine;


namespace InternalAssets.Scripts.UI.Old.TopPanel.Btn
{
    public class HandlerBtnListPosition : MonoBehaviour
    {
        public ListPositionCtrl ListPositionCtrl;
        public TMP_Text text;

        public void GetCentereID()
        {
            text.text = "center ID = " + ListPositionCtrl.GetCenteredBox().listBoxID;
        }
    }
}