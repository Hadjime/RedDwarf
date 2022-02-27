using System;
using InternalAssets.Scripts.Infrastructure.Services;


namespace InternalAssets.Scripts.Infrastructure.Ads
{
	public interface IAdsService : IService
	{
		bool TestMode { get; }
		bool IsRewardedVideoReady { get; }
		void Initialize(bool isTestMode);
		void ShowRewardedVideo(Action onVideoFinished);
	}
}
