using Lean.Localization;
using UnityEngine;

namespace UI
{
    public class ChangeLanguage : MonoBehaviour
    {
        public void SetLanguageIndex(int indexLanguage)
        {
            switch (indexLanguage)
            {
                case 0:
                    LeanLocalization.CurrentLanguage = "Russian";
                    break;
                case 1:
                    LeanLocalization.CurrentLanguage = "English";
                    break;
                default:
                    break;
            }
        }
    }
}