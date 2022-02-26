using System;
using InternalAssets.Scripts.UI.Services.Windows;
using InternalAssets.Scripts.UI.Windows;


namespace InternalAssets.Scripts.StaticData.Windows
{
	[Serializable]
	public class WindowConfig
	{
		public WindowId WindowId;
		public WindowBase Prefab;
	}
}
