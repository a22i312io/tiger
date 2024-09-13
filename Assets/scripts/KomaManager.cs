using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class  KomaManager
{
    public static int[,] KomaPlace = new int[9, 9]
    {
       { 1, 0, 1, 0, 0, 0, 2, 0, 2 },
       { 1, 1, 1, 0, 0, 0, 2, 2, 2 },
       { 1, 0, 1, 0, 0, 0, 2, 0, 2 },
       { 1, 0, 1, 0, 0, 0, 2, 0, 2 },
       { 1, 0, 1, 0, 0, 0, 2, 0, 2 },
       { 1, 0, 1, 0, 0, 0, 2, 0, 2 },
       { 1, 0, 1, 0, 0, 0, 2, 0, 2 },
       { 1, 1, 1, 0, 0, 0, 2, 2, 2 },
       { 1, 0, 1, 0, 0, 0, 2, 0, 2 }
    };
    
    public static void KomaMove(int x1, int z1, int x2, int z2, int player)
    {
        
        KomaPlace[x1, z1] = 0;
        if (player == 1)
        {
            KomaPlace[x2, z2] = 1;
        }
        else
        {
            KomaPlace[x2, z2] = 2;
        }
    }
}
