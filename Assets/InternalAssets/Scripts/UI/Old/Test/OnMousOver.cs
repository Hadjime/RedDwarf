using UnityEngine;


namespace InternalAssets.Scripts.UI.Old.Test
{
	public class OnMousOver : MonoBehaviour
	{
		public GameObject card;
		private void OnMouseOver()
		{
			card.transform.localScale = new Vector3(1.5f, 1.5f, 1);
		}
		private void OnMouseExit()
		{
			card.transform.localScale = new Vector3(1, 1, 1);
		}
	}
}
