using System;
using InternalAssets.Scripts.UI.Services.Windows;
using UnityEngine;
using UnityEngine.UI;


namespace InternalAssets.Scripts.UI.Elements
{
	public class OpenWindowButton : MonoBehaviour
	{
		[SerializeField] private WindowId windowId;
		[SerializeField] private Button button;
		private IWindowService _windowsService;


		public void Constructor(IWindowService windowService) =>
			_windowsService = windowService;


		private void Awake() =>
			button.onClick.AddListener(OnOpen);


		private void OnDestroy() =>
			button.onClick.RemoveAllListeners();


		private void OnOpen() =>
			_windowsService.Open(windowId);
	}
}
