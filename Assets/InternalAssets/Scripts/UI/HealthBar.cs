using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace InternalAssets.Scripts.UI
{
    public class HealthBar : MonoBehaviour
    {
        public Inventory inventory;
        public Slider slider;
        public Image fill;
        public Gradient gradient;
        private float updateSpeedSeconds = 0.2f;

        public void Start()
        {
            //SetMaxHealth();
            HandleHpChangePct();
        }

        public void Update()
        {
            //SetMaxHealth();
        }

        public void SetMaxHealth()
        {
            slider.value = 1f;
            fill.color = gradient.Evaluate(1f);
        }

        public void ApplyDamage(int damage)
        {
            slider.value += damage / 100f;
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
        private void OnEnable()
        {
            EventManager.StartListening("OnHPChange", HandleHpChangePct);
        }

        private void OnDisable()
        {
            EventManager.StopListening("OnHPChange", HandleHpChangePct);
        }
        
        private void HandleHpChangePct()
        {
            StartCoroutine(ChangeToPct(inventory.AmountHp / 100f));
        }

        private IEnumerator ChangeToPct(float pct)
        {
            float preChangePct = slider.value;
            float elapsed = 0f;

            while (elapsed < updateSpeedSeconds)
            {
                elapsed += Time.deltaTime;
                slider.value = Mathf.Lerp(preChangePct, pct, elapsed / updateSpeedSeconds);
                fill.color = gradient.Evaluate(slider.normalizedValue);
                yield return null;
            }
            slider.value = pct;
            fill.color = gradient.Evaluate(slider.normalizedValue);

        }
    }
}
