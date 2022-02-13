using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using InternalAssets.Scripts.Characters.Enemy;
using InternalAssets.Scripts.StaticData;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;


namespace InternalAssets.Scripts.Infrastructure.Services.StaticData
{
	public class StaticDataService : IStaticDataService
	{
		private const string MonstersLabel = "Monsters";
		
		private bool isActivateLog;
		private string loadingLog;
		private Dictionary<MonsterTypeId, MonstersStaticData> _monsters;


		public void LoadMonsters()
		{
			LoadAllAsync<MonstersStaticData>(MonstersLabel, onFinish: list =>
			{
				_monsters = list.ToDictionary(data => data.MonsterTypeId, data => data);
			});
		}


		public MonstersStaticData ForMonsters(MonsterTypeId monsterTypeId) =>
			_monsters.TryGetValue(monsterTypeId, out MonstersStaticData staticData) 
				? staticData 
				: null;


		private void LoadAllAsync<T> (string path, System.Action<List<T>> onFinish)
		{
			var startTimeResourcesLoading = new Stopwatch();
			startTimeResourcesLoading.Start();
            List<string> keys = new List<string>();

            int prevInd = -1;
            for (int i = 0; i < path.Length; i++)
            {
                if (path[i].Equals('/'))
                {
                    var key = path.Substring(prevInd + 1, i - prevInd - 1);
                    keys.Add(key);
                    prevInd = i;
                }
            }

            if (prevInd > 0)
            {
                var key = path.Substring(prevInd + 1, path.Length - prevInd - 1);
                keys.Add(key);
            }
            else
            {
                keys.Add(path);
            }

			List<T> list = new List<T>();
			Addressables.LoadResourceLocationsAsync(keys, Addressables.MergeMode.Intersection, null).Completed += resourceLocationHandle =>
            {
				int handledCount = 0;
                bool getComponent = typeof(T).IsSubclassOf(typeof(MonoBehaviour));

                foreach (IResourceLocation resourceLocation in resourceLocationHandle.Result)
                {
                    if (resourceLocation.ResourceType == typeof(GameObject))
                    {
                        //Debug.LogError("[Resources] load all " + path + " " + resourceLocation.ResourceType + " " + resourceLocation.InternalId + " " + resourceLocation.PrimaryKey + " " + resourceLocation.ProviderId);
                        Addressables.LoadAssetAsync<GameObject>(resourceLocation).Completed += handle =>
                            {
                                //Debug.LogError("[Resources] load all " + path + " " + handle.Result + " " + (typeof(T).IsSubclassOf(typeof(MonoBehaviour))), handle.Result);
                                if (getComponent) list.Add(handle.Result.GetComponent<T>());
                                handledCount++;
								if (handledCount == resourceLocationHandle.Result.Count)
								{
									onFinish(list);
									if (isActivateLog)
										loadingLog += $"[Resources] [{path}] - loading time = {startTimeResourcesLoading.Elapsed.Seconds.ToString()} \n";
								}
							};
                    }
                    else
                    {
                        Addressables.LoadAssetAsync<T>(resourceLocation).Completed += handle =>
                            {
                                list.Add(handle.Result);
                                handledCount++;
								if (handledCount == resourceLocationHandle.Result.Count)
								{
									onFinish(list);
									if (isActivateLog)
										loadingLog += $"[Resources] [{path}] - loading time = {startTimeResourcesLoading.Elapsed.Seconds.ToString()} \n";
								}
								;
                            };
                    }
                }
            };
        }
	}
}
