﻿using System;
using InternalAssets.Scripts.Data;
using InternalAssets.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;


namespace InternalAssets.Scripts.UI.Windows
{
	public abstract class WindowBase : MonoBehaviour
	{
		[SerializeField] private Button closeButton;

		protected IPersistentProgressService ProgressService;
		protected PlayerProgress Progress => ProgressService.Progress;


		public void Constructor(IPersistentProgressService progressService) =>
			ProgressService = progressService;


		private void Awake()
		{
			OnAwake();
		}


		private void Start()
		{
			Initialize();
			SubscribeUpdates();
		}


		private void OnDestroy() =>
			Cleanup();


		private void OnAwake() =>
			closeButton.onClick.AddListener(() => Destroy(gameObject));


		protected virtual void Initialize() {}
		protected virtual void SubscribeUpdates() {}
		protected virtual void Cleanup() {}
	}
}
