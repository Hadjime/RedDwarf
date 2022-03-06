using System;
using InternalAssets.Scripts.UI.Services.Windows;
using InternalAssets.Scripts.UI.Windows;
using UnityEngine;


namespace InternalAssets.Scripts.StaticData.Windows
{
	[Serializable]
	public class WindowConfig : ISerializationCallbackReceiver
	{
		[SerializeField] private string elementName;
		public WindowId WindowId;
		public WindowBase Prefab;


		public void OnBeforeSerialize() {}


		public void OnAfterDeserialize() =>
			elementName = WindowId.ToString();
	}
}
