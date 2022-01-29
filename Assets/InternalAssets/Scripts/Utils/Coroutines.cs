using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InternalAssets.Scripts.Tools
{
	public sealed class Coroutines : MonoBehaviour
	{
		private static readonly Dictionary<float, WaitForSeconds> WaitForSecondsDictionary = new Dictionary<float, WaitForSeconds>();
		
		private static Coroutines instance
		{
			get
			{
				if (_instance == null)
				{
					var go = new GameObject("===[Coroutine Handler]===");
					_instance = go.AddComponent<Coroutines>();
					DontDestroyOnLoad(go);
				}

				return _instance;
			}
		}

		private static Coroutines _instance;


		public static Coroutine StartRoutine(IEnumerator enumerator)
		{
			return instance.StartCoroutine(enumerator);
		}


		public static void StopRoutine(IEnumerator enumerator)
		{
			if (enumerator != null)
				instance.StopCoroutine(enumerator);
		}


		public static void StopRoutine(Coroutine coroutine)
		{
			if (coroutine != null)
				instance.StopCoroutine(coroutine);
		}


		public static WaitForSeconds GetWait(float time)
		{
			if (WaitForSecondsDictionary.TryGetValue(time, out var wait)) return wait;

			WaitForSecondsDictionary[time] = new WaitForSeconds(time);
			return WaitForSecondsDictionary[time];
		}
	}
}