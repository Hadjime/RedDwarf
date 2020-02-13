using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Event
{
    public class HealthPanel : MonoBehaviour
    {
        public Inventory inventory;
        public Image bar;

        [SerializeField] private float updateSpeedSeconds = 0.2f;
        private void Awake()
        {
            inventory.AmountHp = 100;
            //HpChange();
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

        private void HpChange()
        {
            bar.fillAmount = inventory.AmountHp / 100f;
        }
        private void HandleHpChangePct()
        {
            StartCoroutine(ChangeToPct(inventory.AmountHp / 100f));
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