using System.Collections.Generic;
using UnityEngine;

namespace InternalAssets.Scripts.Map
{
    public class TileSetting : MonoBehaviour, ISelect
    {
        [SerializeField] private int maxHp;
        [SerializeField, Range(0,100)] private int hpTile;
        public int HpTile
        {
            get => hpTile;
            set
            {
                if (value <= 0)
                {
                    hpTile = 0;
                    Destroy(gameObject);
                }
                else
                {
                    hpTile = value;
                }
                if (hpTile > maxHp * 0.7f && hpTile <= maxHp)
                {
                    _spriteRenderer.sprite = listSpritesForVisualisationHealth[0];
                }
                if (hpTile > maxHp * 0.3f && hpTile < maxHp * 0.7f)
                {
                    _spriteRenderer.sprite = listSpritesForVisualisationHealth[1];
                }
                if (hpTile >= 0 && hpTile < maxHp * 0.3f)
                {
                    _spriteRenderer.sprite = listSpritesForVisualisationHealth[2];
                }
            }

        }

        public List<Sprite> listSpritesForVisualisationHealth;
        
        private SpriteRenderer _spriteRenderer;
        
        // Start is called before the first frame update
        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            HpTile = maxHp;
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        [ContextMenu("SetHp100")] public void SetHp100()
        {
            HpTile = 100;
        }
        
        [ContextMenu("SetHp50")] public void SetHp50()
        {
            HpTile = 50;
        }
        
        [ContextMenu("SetHp20")] public void SetHp20()
        {
            HpTile = 20;
        }
        [ContextMenu("SetHp0")] public void SetHp0()
        {
            HpTile = 0;
        }

        public int GetItem()
        {
            Destroy(this.gameObject);
            return 1;
        }
    }
}
