using System;
using UnityEngine;

[CreateAssetMenu(menuName ="Item")]
public class AssetItem: ScriptableObject, IItem
{
    //public string Name =>  throw new NotImplementedException("Name not inmplement");
    public string Name => _name;
    public Texture2D UIIcon => _uiIcon;
    [SerializeField]private string _name ;
    [SerializeField]private Texture2D _uiIcon;
}

