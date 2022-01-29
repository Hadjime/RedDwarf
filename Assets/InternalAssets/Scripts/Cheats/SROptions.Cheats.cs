using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using InternalAssets.Scripts.Player;
using UnityEngine;

public partial class SROptions
{
	[Category("Inventory")]
	public void Add_5_Axe()
	{
		TakeItems takeItems = GameObject.FindObjectOfType<TakeItems>();
		if (takeItems == null) 
			return;
		takeItems.inventory.AmountPickAxe += 5;
	}


	[Category("Inventory")]
	public void Add_10_Gold()
	{
		TakeItems takeItems = GameObject.FindObjectOfType<TakeItems>();
		if (takeItems == null)
			return;
		takeItems.inventory.AmountMoney += 10;
	}


	[Category("Inventory")]
	public void Increase_10_HP()
	{
		TakeItems takeItems = GameObject.FindObjectOfType<TakeItems>();
		if (takeItems == null)
			return;
		takeItems.inventory.AmountHp += 10;
	}


	[Category("Inventory")]
	public void Reduce_10_HP()
	{
		TakeItems takeItems = GameObject.FindObjectOfType<TakeItems>();
		if (takeItems == null)
			return;
		takeItems.inventory.AmountHp -= 10;
	}
}
