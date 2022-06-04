using AirFishLab.ScrollingList;
using UnityEngine;


namespace InternalAssets.Scripts.UI.Windows.GamePlay.ScrollingInventoryItems
{
	public class CircularScrollingInventory : CircularScrollingList
	{
		[SerializeField] private AttackButton attackBtn;
		
		public override void Initialize()
		{
			base.Initialize();
			setting.onCenteredBoxChanged.AddListener(OnCenteredBoxChanged);
			EnableBackLightOnlyInCenterElement(GetCenteredBox());
		}

		private void OnCenteredBoxChanged(ListBox beforeListBox, ListBox CenteredListBox)
		{
			attackBtn.SetAvailable(CheckAvailableElementInventory(CenteredListBox as ElementScrolling));
			EnableBackLightOnlyInCenterElement(CenteredListBox);
		}

		private bool CheckAvailableElementInventory(ElementScrolling elementScrolling) =>
			elementScrolling.ItemData.Amount > 0;

		private void EnableBackLightOnlyInCenterElement(ListBox centeredListBox)
		{
			foreach (ListBox listBox in listBoxes)
				if (listBox is ElementScrolling element)
					element.SetBackLight(listBox == centeredListBox);
		}
	}
}
