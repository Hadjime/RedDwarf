﻿using System;
using System.Collections;
using InternalAssets.Scripts.Data;
using InternalAssets.Scripts.Tools;
using TMPro;
using UnityEngine;


namespace InternalAssets.Scripts.Characters.Enemy
{
	public class LootPiece : MonoBehaviour
	{
		[SerializeField] private Loot defaultLoot = new Loot(){Value = 10};
		[SerializeField] private SpriteRenderer lootIcon; 
		[SerializeField] private GameObject PickupFxPrefab;
		[SerializeField] private TextMeshPro LooText;
		
		private Loot _loot;
		private bool _isPicked;
		private WorldData _worldData;


		public void Constructor(WorldData worldData) =>
			_worldData = worldData;


		public void Initialize(Loot loot) =>
			_loot = loot;


		private void OnTriggerEnter2D(Collider2D other) =>
			Pickup();


		private void Pickup()
		{
			if (_isPicked)
				return;

			_isPicked = true;
			
			TryUpdateWorldData();
			HideIcon();
			PLayPickupFx();
			ShowText();
			StartCoroutine(StartDestroyTimer());
		}


		private void TryUpdateWorldData() =>
			_worldData?.LootData.Collect(_loot ?? defaultLoot);





		private void HideIcon() =>
			lootIcon.enabled = false;


		private void PLayPickupFx() =>
			Instantiate(PickupFxPrefab, transform.position, Quaternion.identity);


		private void ShowText()
		{
			string lootText = _loot != null ? _loot.Value.ToString() : defaultLoot.Value.ToString();
			LooText.text = $"{lootText}";
			LooText.gameObject.SetActive(true);
		}


		private IEnumerator StartDestroyTimer()
		{
			yield return Coroutines.GetWaitRealTime(1.5f);
			Destroy(gameObject);
		}
	}
}
