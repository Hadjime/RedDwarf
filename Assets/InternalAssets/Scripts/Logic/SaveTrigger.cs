using System;
using InternalAssets.Scripts.Infrastructure.Services;
using InternalAssets.Scripts.Infrastructure.Services.SaveLoad;
using InternalAssets.Scripts.Player;
using InternalAssets.Scripts.Utils.Log;
using UnityEngine;

namespace InternalAssets.Scripts.Logic
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class SaveTrigger : MonoBehaviour
    {
        private ISaveLoadService _saveLoadService;


        public BoxCollider2D collider2D;

        private void Reset()
        {
            collider2D = GetComponent<BoxCollider2D>();
        }

        private void Awake()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<PlayerMove>(out PlayerMove component))
                return;
            
            _saveLoadService.SaveProgress();
            CustomDebug.Log($"Progress saved");
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            if (!collider2D)
                return;
            
            Gizmos.color = new Color32(30, 200, 30, 130);
            Gizmos.DrawCube(transform.position, collider2D.size);
        }
    }
}