using System.Collections.Generic;
using InternalAssets.Scripts.Characters.Enemy;
using InternalAssets.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace InternalAssets.Scripts.Map.Grids
{
	public class GridsManager : MonoBehaviour
	{
		[SerializeField] private Grid grid;
		[SerializeField] private Tilemap land;
		[SerializeField] private Tilemap fogOfWar;
		[field:SerializeField] public List<LootPiece> LootsInLevel;
		private IPersistentProgressService _persistentProgressService;


		public Grid Grid => grid;
		public Tilemap Land => land;
		public Tilemap FogOfWar => fogOfWar;
		public bool IsActiveFogOfWar => fogOfWar.gameObject.activeSelf;


		public void Initialize(IPersistentProgressService persistentProgressService)
		{
			_persistentProgressService = persistentProgressService;
			LootsInLevel ??= new List<LootPiece>();
			LootsInLevel.ForEach(loot => loot.Constructor(_persistentProgressService.Progress.WorldData));
		}
		public void SetActiveFogOfWar(bool isActive) =>
			fogOfWar.gameObject.SetActive(isActive);
	}
}
