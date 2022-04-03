using System;
using System.Collections.Generic;


namespace InternalAssets.Scripts.Data
{
	[Serializable]
	public class PurchaseData
	{
		public List<BoughtIAP> BoughtIAPs = new List<BoughtIAP>();
		public Action OnChanged;
		public PurchaseData() {}


		public void AddPurchase(string id)
		{
			BoughtIAP boughtIap = BoughtIAPs.Find(iap => iap.IAPid == id);
			if (boughtIap != null)
				boughtIap.Count++;
			else
				BoughtIAPs.Add(new BoughtIAP { IAPid = id, Count = 1 });

			OnChanged?.Invoke();
		}
	}
}
