using UnityEngine;


namespace InternalAssets.Scripts.UI.Old.Test
{
	public class RectSizeDelta : MonoBehaviour
	{
		private RectTransform rectTransform;
		// Start is called before the first frame update
		void Start()
		{
			rectTransform = GetComponent<RectTransform>();
		}

		// Update is called once per frame
		void Update()
		{
			Debug.Log(rectTransform.sizeDelta);
		}
	}
}
