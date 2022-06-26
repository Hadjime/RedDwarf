using UnityEngine;


namespace InternalAssets.Scripts.Characters.Hero.Data
{
    [CreateAssetMenu(fileName = "PlayerDataDefault", menuName = "Scriptable Object/Player Data/New Player Data", order = 0)]
    public class PlayerData : ScriptableObject
    {
        [Header("Move State")] 
        [Range(0.1f, 50f)] public float movementVelocity = 2f;
        [Range(0.1f, 50f)] public float handAttack = 2f;
    }
}