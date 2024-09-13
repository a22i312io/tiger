using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fu : Koma
{
    public TileManager tileManager;
    
    // Start is called before the first frame update
    public Fu(int x, int z, int n) : base(x, z, n) { }

    internal static void Koma_Tile()
    {
        throw new NotImplementedException();
    }

    

    //public override void OnMouseDown()
    //{
    //    tileManager.Fu_Tile_Spawn(gameobj3);
    //}
}
