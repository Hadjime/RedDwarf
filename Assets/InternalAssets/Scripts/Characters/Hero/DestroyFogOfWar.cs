using UnityEngine;
using UnityEngine.Tilemaps;


namespace InternalAssets.Scripts.Characters.Hero
{
    public class DestroyFogOfWar : MonoBehaviour
    {
        public Grid grid;
        public Tilemap fogOfWar;
        private void OnCollisionStay2D(Collision2D other)
        {
            foreach (var contact in other.contacts)
            {
                Vector3Int point = grid.WorldToCell(contact.point);
                fogOfWar.SetTile(point, null);
            }
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            foreach (var contact in other.contacts)
            {
                Vector3Int point = grid.WorldToCell(contact.point);
                fogOfWar.SetTile(point, null);
            }
        }

        private void Update()
        {
        
        }
    }
}
