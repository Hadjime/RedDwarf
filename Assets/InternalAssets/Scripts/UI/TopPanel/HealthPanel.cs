using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace InternalAssets.Scripts.UI.Event
{
    public class HealthPanel : MonoBehaviour
    {
        public Inventory.Inventory inventory;
        public Image bar;

        [SerializeField] private float updateSpeedSeconds = 0.2f;
        private void Start()
        {
            inventory.AmountHp = 100;
            HandleHpChangePct();
        }

        private void OnEnable()
        {
            EventManager.StartListening("OnHPChange", HandleHpChangePct);
        }

        private void OnDisable()
        {
            EventManager.StopListening("OnHPChange", HandleHpChangePct);
        }

        // Instantly changing HP
        private void HpChange()
        {
            bar.fillAmount = inventory.AmountHp / 100f;
        }
        
        //Gradually changing GP
        private void HandleHpChangePct()
        {
            var hpPct = inventory.AmountHp / 100f;
            StartCoroutine(ChangeToPct(hpPct));
        }

        private IEnumerator ChangeToPct(float pct)
        {
            float preChangePct = bar.fillAmount;
            float elapsed = 0f;

            while (elapsed < updateSpeedSeconds)
            {
                elapsed += Time.deltaTime;
                bar.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / updateSpeedSeconds);
                yield return null;
            }

            bar.fillAmount = pct;
        }
    }
}