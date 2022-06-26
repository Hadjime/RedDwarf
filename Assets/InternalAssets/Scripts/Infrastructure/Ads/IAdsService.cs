using System;
using InternalAssets.Scripts.Infrastructure.Services;
using InternalAssets.Scripts.Infrastructure.Services.StaticDI;


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
