using System;
using InternalAssets.Scripts.Infrastructure.Services;
using InternalAssets.Scripts.Utils.Log;
using UnityEngine;
using UnityEngine.Advertisements;


namespace InternalAssets.Scripts.Infrastructure.Ads
{
	public class AdsService: IAdsService, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
	{
		private const string ANDROID_GAME_ID = "4632295";
		private const string IOS_GAME_ID = "4632294";

		private const string REWARDED_VIDEO_PLACEMENT_ID = "rewardedVideo";


		private Color messageColor = Color.yellow;
		private string _gameId;
		private bool _isTestMode;
		private Action _onVideoFinished;
		public bool TestMode => _isTestMode;
		public Action RewardedVideoReady;



		public void Initialize(bool isTestMode)
		{
			_isTestMode = isTestMode;
			
			switch (Application.platform)
			{
				case RuntimePlatform.Android:
					_gameId = ANDROID_GAME_ID;
					break;
				case RuntimePlatform.IPhonePlayer:
					_gameId = IOS_GAME_ID;
					break;
				case RuntimePlatform.WindowsEditor:
					_gameId = IOS_GAME_ID;
					break;
				default:
					CustomDebug.Log("Unsupported platform for ads");
					break;
			}
			
			Advertisement.Initialize(_gameId, _isTestMode, this);
			Advertisement.Load(REWARDED_VIDEO_PLACEMENT_ID, this);
		}


		public void ShowRewardedVideo(Action onVideoFinished)
		{
			Advertisement.Show(REWARDED_VIDEO_PLACEMENT_ID, this);
			_onVideoFinished = onVideoFinished;
		}


		public bool IsRewardedVideoReady => !Advertisement.isShowing;

		public void OnInitializationComplete() =>
			CustomDebug.Log($"[ADS] Initialize complete", messageColor);


		public void OnInitializationFailed(UnityAdsInitializationError error, string message) =>
			CustomDebug.LogError($"[ADS] {error} - {message}");


		public void OnUnityAdsAdLoaded(string placementId)
		{
			RewardedVideoReady?.Invoke();
			CustomDebug.Log($"[ADS] OnUnityAdsAdLoaded : {placementId}");
		}


		public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) =>
			CustomDebug.LogError($"[ADS] OnUnityAdsFailedToLoad : {placementId}");

		public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) =>
			CustomDebug.LogError($"[ADS] OnUnityAdsShowFailure : placementId - {error} - {message}");


		public void OnUnityAdsShowStart(string placementId) =>
			CustomDebug.Log($"[ADS] OnUnityAdsShowStart : {placementId}", messageColor);


		public void OnUnityAdsShowClick(string placementId) =>
			CustomDebug.Log($"[ADS] OnUnityAdsShowClick : {placementId}", messageColor);


		public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
		{
			switch (showCompletionState)
			{
				case UnityAdsShowCompletionState.SKIPPED:
					CustomDebug.LogError($"[ADS] OnUnityAdsShowComplete : {placementId} - SKIPPED");
					break;

				case UnityAdsShowCompletionState.COMPLETED:
					_onVideoFinished?.Invoke();
					break;

				case UnityAdsShowCompletionState.UNKNOWN:
					CustomDebug.LogError($"[ADS] OnUnityAdsShowComplete : {placementId} - UNKNOWN");
					break;

				default:
					CustomDebug.LogError($"[ADS] OnUnityAdsShowComplete : {placementId} - unreal variant");
					break;
			}

			_onVideoFinished = null;
		}
	}
}
