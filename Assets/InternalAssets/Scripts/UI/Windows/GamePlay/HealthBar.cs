using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace InternalAssets.Scripts.UI.Windows.GamePlay
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Image fill;
        [SerializeField] private Gradient gradient;
        [SerializeField] private float rateOfChangedHp = 0.2f;
        [SerializeField] private Image heroIcon;
        [SerializeField] private List<Sprite> heroIconsFirVisualisationHealth;


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
            SetHeroIcon(pct);
        }

        private void SetHeroIcon(float pct)
        {
            const float maxStep = 1f;
            const float minStep = 0f;
            const float step1 = 0.7f;
            const float step2 = 0.3f;
            
            if (heroIconsFirVisualisationHealth == null)
                return;
			
            if (pct > step1 && pct <= maxStep)
            {
                heroIcon.sprite = heroIconsFirVisualisationHealth[0];
            }
            if (pct > step2 && pct < step1)
            {
                heroIcon.sprite = heroIconsFirVisualisationHealth[1];
            }
            if (pct >= minStep && pct < step2)
            {
                heroIcon.sprite = heroIconsFirVisualisationHealth[2];
            }
        }
    }
}
