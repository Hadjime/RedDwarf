using UnityEngine;


namespace InternalAssets.Scripts.UI.Old.TopPanel.Btn
{
    public class BtnSwitchFogOfWar : MonoBehaviour
    {
        public void Switch()
        {
            EventManager.StartEvent("SwitchIsFogOfWar");
        }
    }
}