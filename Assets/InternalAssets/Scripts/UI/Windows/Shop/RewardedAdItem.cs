using System;
using System.Collections.Generic;
using System.Reflection;
using InternalAssets.Scripts.Infrastructure.Ads;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;


namespace InternalAssets.Scripts.UI.Windows.Shop
{
	public class RewardedAdItem : MonoBehaviour
	{
		[SerializeField] private Button showAdBtn;
		[SerializeField] private List<GameObject> adActiveObjects;
		[SerializeField] private List<GameObject> adInactiveObjects;
		private IAdsService _adsService;


		public void Constructor(IAdsService adsService)
		{
			_adsService = adsService;
		}

		public void Initialize()
		{
			showAdBtn.onClick.AddListener(OnShowAdClick);

			RefreshAvailableAd();
		}


		public void Subscribe()
		{
			
		}


		public void Cleanup()
		{
			
		}


		private void OnShowAdClick()
		{
			
		}


		private void RefreshAvailableAd()
		{
			
		}
	}
}
