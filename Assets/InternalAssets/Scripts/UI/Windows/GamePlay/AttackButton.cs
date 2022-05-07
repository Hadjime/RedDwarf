using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;


namespace InternalAssets.Scripts.UI.Windows.GamePlay
{
	public class AttackButton: MonoBehaviour
	{
		[SerializeField] private CanvasGroup canvasGroup;
		[SerializeField] private Button attackBtn;
		[SerializeField] private OnScreenButton onScreenButton;

		public void SetAvailable(bool isAvailable)
		{
			canvasGroup.alpha = isAvailable ? 1 : 0.5f;
			attackBtn.interactable = isAvailable;
			onScreenButton.enabled = isAvailable;
		}
	}
}
