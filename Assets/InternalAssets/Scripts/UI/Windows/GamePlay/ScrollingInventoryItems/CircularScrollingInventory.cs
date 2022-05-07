using AirFishLab.ScrollingList;
using UnityEngine;
using UnityEngine.UI;


namespace InternalAssets.Scripts.UI.Windows.GamePlay.ScrollingInventoryItems
{
	public class CircularScrollingInventory : CircularScrollingList
	{
		[SerializeField] private AttackButton attackBtn;
		public override void Initialize()
		{
			base.Initialize();
			setting.onCenteredBoxChanged.AddListener(OnCenteredBoxChanged);
		}

		private void OnCenteredBoxChanged(ListBox beforeListBox, ListBox CenteredListBox)
		{
			if (CenteredListBox is ElementScrolling elementScrolling)
				attackBtn.SetAvailable(CheckAvailableElementInventory(elementScrolling));
		}

		private bool CheckAvailableElementInventory(ElementScrolling elementScrolling) =>
			elementScrolling.ItemData.Amount > 0;
	}
}
