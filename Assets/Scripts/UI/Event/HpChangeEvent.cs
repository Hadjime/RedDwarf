using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Event
{
    public class HpChangeEvent : MonoBehaviour
    {
        public Inventory inventory;
        public Image bar;
        private void Awake()
        {
            HpChange();
        }

        private void OnEnable()
        {
            EventManager.StartListening("HPChange", HpChange);
        }

        private void OnDisable()
        {
            EventManager.StopListening("HPChange", HpChange);
        }

        private void HpChange()
        {
            bar.fillAmount = inventory.AmountHp / 100f;
        }
    }
}