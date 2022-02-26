using UnityEngine;


namespace InternalAssets.Scripts.UI.Old.Test
{
	public class HowManyAnchors : MonoBehaviour
	{
		private RectTransform rectTransform;

		void Start()
		{
			rectTransform = GetComponent<RectTransform>();
		}


		void Update()
		{
			Debug.Log(rectTransform.anchoredPosition);
		}
	}
}
