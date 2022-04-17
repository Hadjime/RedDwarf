﻿using AirFishLab.ScrollingList;


namespace InternalAssets.Scripts.UI.Old.CircularScrollingList
{
	public class MyListBank: BaseListBank
	{
		private int[] _contents = {
			1, 2, 3, 4, 5, 6, 7, 8, 9, 10
		};

		public override object GetListContent(int index)
		{
			return _contents[index].ToString();
		}

		public override int GetListLength()
		{
			return _contents.Length;
		}
	}
}
