using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using InternalAssets.Scripts.Inventory;

namespace InternalAssets.Scripts.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Image fill;
        [SerializeField] private Gradient gradient;
        [SerializeField] private float rateOfChangedHp = 0.2f;


        public void SetValue(float currentHp, float maxHp)
        {
            StartCoroutine(ChangeToPct(currentHp / maxHp));
        }

        private IEnumerator ChangeToPct(float pct)
        {
            float preChangePct = slider.value;
            float elapsedTime = 0f;

            while (elapsedTime < rateOfChangedHp)
            {
                elapsedTime += Time.deltaTime;
                slider.value = Mathf.Lerp(preChangePct, pct, elapsedTime / rateOfChangedHp);
                fill.color = gradient.Evaluate(slider.normalizedValue);
                yield return null;
            }
            slider.value = pct;
            fill.color = gradient.Evaluate(slider.normalizedValue);

        }
    }
}
