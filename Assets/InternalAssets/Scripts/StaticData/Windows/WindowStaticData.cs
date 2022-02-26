using System.Collections.Generic;
using UnityEngine;


namespace InternalAssets.Scripts.StaticData.Windows
{
	[CreateAssetMenu(fileName = "WindowStaticData", menuName = "Static Data/Window static data", order = 0)]
	public class WindowStaticData : ScriptableObject
	{
		public List<WindowConfig> Configs;
	}
}
