using System;
using UnityEngine;

namespace InternalAssets.Scripts._temp
{
    public class MyListBox : BaseListBank
    {
        private int[] _contents = {
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10
        };
        private void Start()
        {
            //throw new NotImplementedException();
        }

        public override string GetListContent(int index)
        {
            return _contents[index].ToString();
        }

        public override int GetListLength()
        {
            return _contents.Length;
        }
    }
}