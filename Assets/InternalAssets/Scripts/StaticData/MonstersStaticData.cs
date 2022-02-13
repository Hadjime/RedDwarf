using InternalAssets.Scripts.Characters.Enemy;
using UnityEngine;


namespace InternalAssets.Scripts.StaticData
{
	[CreateAssetMenu(fileName = "MonsterData", menuName = "StaticData/Monster")]
	public class MonstersStaticData: ScriptableObject
	{
		public MonsterTypeId MonsterTypeId;
		[Range(1, 100)]
		public float Hp;
		[Range(1, 50)]
		public float Damage;
		[Range(0.1f, 10)]
		public float AttackCooldown = 1f;
		[Range(0, 20)]
		public float MoveSpeed;
		[Range(0.5f, 4)]
		public float EffectiveRadiusAttack = 0.4f;
		public GameObject Prefab;
	}
}
