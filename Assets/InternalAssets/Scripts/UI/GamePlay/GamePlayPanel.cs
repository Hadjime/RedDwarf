using System;
using InternalAssets.Scripts.Characters.Hero;
using InternalAssets.Scripts.Player;
using UnityEngine;

namespace InternalAssets.Scripts.UI.GamePlay
{
    public class GamePlayPanel : MonoBehaviour
    {
        [SerializeField] private HealthBar healthBar;


        private IHealth _heroHealth;

        public void Constructor(IHealth heroHealth)
        {
            _heroHealth = heroHealth;
            _heroHealth.HpChanged += OnUpdateHealthBar;
        }

        private void OnDestroy() => 
            _heroHealth.HpChanged -= OnUpdateHealthBar;

        private void OnUpdateHealthBar() => 
            healthBar.SetValue(_heroHealth.CurrentHp, _heroHealth.MaxHp);
    }
}