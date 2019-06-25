using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<AssetItem> Items;

    public void OnEnable()
    {
        Renderer(Items);
    }
    public void Renderer(List<AssetItem> items)
    {

    }
}
