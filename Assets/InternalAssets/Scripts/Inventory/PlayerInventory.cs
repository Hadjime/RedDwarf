using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new PlayerInventory", menuName = "Scriptable Object/New PlayerInventory", order =51)]
public class PlayerInventory : ScriptableObject
{
    public string namePlayer;
    public int amountGold;
    public int amountPickaxe;
}
