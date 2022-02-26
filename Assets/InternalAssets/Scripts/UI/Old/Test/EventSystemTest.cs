using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace InternalAssets.Scripts.UI.Old.Test
{
	public class EventSystemTest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
	{
		private Image image;
		public void Awake()
		{
			image = GetComponent<Image>();
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			image.color = Color.blue;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			image.color = Color.white;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			image.color = Color.black;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			image.color = Color.green;
		}
	}
}
