using InternalAssets.Scripts.Data;
using TMPro;
using UnityEngine;


namespace InternalAssets.Scripts.UI.GamePlay
{
	public class LootCounter : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI CounterText;
		private WorldData _worldData;


		public void Constructor(WorldData worldData)
		{
			_worldData = worldData;
			_worldData.LootData.Changed += OnUpdateCounter;
		}


		private void Start() =>
			OnUpdateCounter();


		private void OnUpdateCounter()
		{
			CounterText.text = $"{_worldData.LootData.Collected}";
		}
	}
}
