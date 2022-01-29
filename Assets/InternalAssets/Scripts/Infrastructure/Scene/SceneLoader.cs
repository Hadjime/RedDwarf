using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace InternalAssets.Scripts.Infrastructure.Scene
{
	public class SceneLoader
	{
		private readonly ICoroutineRunner _coroutineRunner;


		public SceneLoader(ICoroutineRunner coroutineRunner) =>
			_coroutineRunner = coroutineRunner;


		public void Load(string name, Action onLoaded = null)
		{
			_coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
		}
		
		
		private IEnumerator LoadScene(string name, Action onLoaded = null)
		{
			AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(name);

			while (!loadSceneAsync.isDone)
				yield return null;
			
			onLoaded?.Invoke();
		}
	}
}
