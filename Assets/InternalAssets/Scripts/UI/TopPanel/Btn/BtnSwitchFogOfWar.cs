using System;
using UnityEngine;

namespace InternalAssets.Scripts.UI.Btn
{
    public class BtnSwitchFogOfWar : MonoBehaviour
    {
        public void Switch()
        {
            EventManager.StartEvent("SwitchIsFogOfWar");
        }
    }
}