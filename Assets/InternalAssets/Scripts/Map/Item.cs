using System;
using UnityEngine;


namespace InternalAssets.Scripts.Map
{
	[Obsolete]
	public class Item : MonoBehaviour, ISelect
	{
		public int price;

		public int GetItem()
		{
			Destroy(this.gameObject);
			return price;
		}
	}
}
