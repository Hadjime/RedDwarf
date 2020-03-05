using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestroyFogOfWar : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap fogOfWar;
    private void OnCollisionEnter2D(Collision2D other)
    {
        foreach (var contact in other.contacts)
        {
            Vector3Int point = grid.WorldToCell(contact.point);
            fogOfWar.SetTile(point, null);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        foreach (var contact in other.contacts)
        {
            Vector3Int point = grid.WorldToCell(contact.point);
            fogOfWar.SetTile(point, null);
        }
    }
}
