using UnityEngine;
using UnityEngine.Tilemaps;


namespace InternalAssets.Scripts.Map.Grids
{
	public class GridsManager : MonoBehaviour
	{
		[SerializeField] private Grid grid;
		[SerializeField] private Tilemap land;
		[SerializeField] private Tilemap fogOfWar;
		

		public Grid Grid => grid;
		public Tilemap Land => land;
		public Tilemap FogOfWar => fogOfWar;
		public bool IsActiveFogOfWar => fogOfWar.gameObject.activeSelf;


		public void SetActiveFogOfWar(bool isActive) =>
			fogOfWar.gameObject.SetActive(isActive);
	}
}
