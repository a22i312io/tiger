using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    public GameObject tile;
    
    public void Tile_Spawn(int x, int z)
    {
        
        Instantiate(tile, new Vector3(x, 0.01f, z), Quaternion.identity);
    }

    public void Tile_Destroy()
    {
        GameObject [] objects = GameObject.FindGameObjectsWithTag("Tile");
        foreach(GameObject destroy_tile in objects)
        {
            Destroy(destroy_tile);
        }
    }
    //private void Instantiate(object tile, Vector3 vector3, Quaternion identity)
    //{
    //    throw new NotImplementedException();
    //}

    public void Fu_Tile_Spawn(int x, int z, int komaplayer)
    {
        if (komaplayer == 1)
        {
            if (KomaManager.KomaPlace[x, z + 1] != 1)
            {
                Tile_Spawn(x, z + 1);
            }
        }
        else if(komaplayer == 2) 
        {
            if (KomaManager.KomaPlace[x, z - 1] != 2)
            {
                Tile_Spawn(x, z - 1);
            }
        }
        
    }

    
    public void Ou_Tile_Spawn(int x, int z, int komaplayer)
    {
        int l, m;
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                l = x + i;
                m = z + j;
                if(!(i == 0 && j == 0))
                {
                    if((l >=0 && l <= 8) && (m >= 0 && m <= 8))
                    {
                        if (komaplayer == 1)
                        {
                            if (KomaManager.KomaPlace[l, m] != 1)
                            {
                                Tile_Spawn(l, m);
                            }
                        }
                        else
                        {
                            if(KomaManager.KomaPlace[l, m] != 2)
                            {
                                Tile_Spawn(l, m);
                            }
                        }
                    }
                }
            }
        }
    }

    public void Kin_Tile_Spawn(int x, int z, int komaplayer)
    {
        int l = -1, m = -1;

        if (komaplayer == 1)
        {
            int[,] nums = new int[3, 3]
            {
            {0, 1, 1 },
            {1, 0, 1 },
            {0, 1, 1}
            };


            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (nums[i, j] == 1)
                    {
                        l = x + i - 1;
                        m = z + j - 1;
                    }

                    if ((l >= 0 && l <= 8) && (m >= 0 && m <= 8))
                    {
                        if (KomaManager.KomaPlace[l, m] != 1)
                        {
                            Tile_Spawn(l, m);
                        }
                    }
                }
            }
        }
        else
        {
            int[,] nums = new int[3, 3]
            {
            {1, 1, 0 },
            {1, 0, 1 },
            {1, 1, 0}
            };


            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (nums[i, j] == 1)
                    {
                        l = x + i - 1;
                        m = z + j - 1;
                    }

                    if ((l >= 0 && l <= 8) && (m >= 0 && m <= 8))
                    {
                        if (KomaManager.KomaPlace[l, m] != 2){
                            Tile_Spawn(l, m);
                        }
                    }
                }
            }
        }

    }

    public void Gin_Tile_Spawn(int x, int z, int komaplayer)
    {
        int l = -1, m = -1;
        if (komaplayer == 1)
        {
            int[,] nums = new int[3, 3]
            {
            {1, 0, 1 },
            {0, 0, 1 },
            {1, 0, 1}
            };

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (nums[i, j] == 1)
                    {
                        l = x + i - 1;
                        m = z + j - 1;
                    }

                    if ((l >= 0 && l <= 8) && (m >= 0 && m <= 8))
                    {
                        if (KomaManager.KomaPlace[l, m] != 1)
                        {
                            Tile_Spawn(l, m);
                        }
                    }
                }
            }
        }
        else
        {
            int[,] nums = new int[3, 3]
            {
            {1, 0, 1 },
            {1, 0, 0 },
            {1, 0, 1}
            };

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (nums[i, j] == 1)
                    {
                        l = x + i - 1;
                        m = z + j - 1;
                    }

                    if ((l >= 0 && l <= 8) && (m >= 0 && m <= 8))
                    {
                        if (KomaManager.KomaPlace[l, m] != 2)
                        {
                            Tile_Spawn(l, m);
                        }
                    }
                }
            }
        }
    }

    public void Keima_Tile_Spawn(int x, int z, int komaplayer)
    {
        if (komaplayer == 1)
        {
            int l1 = x - 1;
            int l2 = x + 1;
            int m = z + 2;
            if (l1 >= 0)
            {
                if (KomaManager.KomaPlace[l1, m] != 1)
                {
                    Tile_Spawn(l1, m);
                }
            }
            if (l2 <= 8)
            {
                if (KomaManager.KomaPlace[l2, m] != 1)
                {
                    Tile_Spawn(l2, m);
                }
            }
        }
        else
        {
            int l1 = x - 1;
            int l2 = x + 1;
            int m = z - 2;
            if (l1 >= 0)
            {
                if (KomaManager.KomaPlace[l1, m] != 2)
                {
                    Tile_Spawn(l1, m);
                }
            }
            if (l2 <= 8)
            {
                if (KomaManager.KomaPlace[l2, m] != 2)
                {
                    Tile_Spawn(l2, m);
                }
            }
        }
    }

    public void Kyousha_Tile_Spawn(int x, int z, int komaplayer)
    {
        if (komaplayer == 1)
        {
            for (int i = 1; i <= 8; i++)
            {
                int m = z + i;
                if (m <= 8)
                {
                    if (KomaManager.KomaPlace[x, m] == 0) 
                    { 
                        Tile_Spawn(x, m);
                    }else if(KomaManager.KomaPlace[x, m] == 1)
                    {
                        return;
                    }
                    else
                    {
                        Tile_Spawn(x, m);
                        return;
                    }
                }
            }
        }
        else
        {
            for (int i = 1; i <= 8; i++)
            {
                int m = z - i;
                if (m >= 0)
                {
                    if (KomaManager.KomaPlace[x, m] == 0)
                    {
                        Tile_Spawn(x, m);
                    }
                    else if (KomaManager.KomaPlace[x, m] == 1)
                    {
                        Tile_Spawn(x, m);
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
    }

    public void Hisha_Tile_Spawn(int x, int z, int komaplayer)
    {
        int l = -1;
        int m = -1;
        
        for(int i = 1; i <= 8; i++)
        {
            l = x + i;
            if(l <= 8)
            {
                if(komaplayer == 1)
                {
                    if(KomaManager.KomaPlace[l, z] == 0)
                    {
                        Tile_Spawn(l, z);
                    }else if (KomaManager.KomaPlace[l, z] == 1)
                    {
                        break;
                    }
                    else
                    {
                        Tile_Spawn(l, z);
                        break;
                    }
                }
                else
                {
                    if (KomaManager.KomaPlace[l, z] == 0)
                    {
                        Tile_Spawn(l, z);
                    }
                    else if (KomaManager.KomaPlace[l, z] == 1)
                    {
                        Tile_Spawn(l, z);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        for (int i = -1; i >= -8; i--)
        {
            l = x + i;
            if (l >= 0)
            {
                if (komaplayer == 1)
                {
                    if (KomaManager.KomaPlace[l, z] == 0)
                    {
                        Tile_Spawn(l, z);
                    }
                    else if (KomaManager.KomaPlace[l, z] == 1)
                    {
                        break;
                    }
                    else
                    {
                        Tile_Spawn(l, z);
                        break;
                    }
                }
                else
                {
                    if (KomaManager.KomaPlace[l, z] == 0)
                    {
                        Tile_Spawn(l, z);
                    }
                    else if (KomaManager.KomaPlace[l, z] == 1)
                    {
                        Tile_Spawn(l, z);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        for (int i = 1; i <= 8; i++)
        {
            m = z + i;
            if (m <= 8)
            {
                if (komaplayer == 1)
                {
                    if (KomaManager.KomaPlace[x, m] == 0)
                    {
                        Tile_Spawn(x, m);
                    }
                    else if (KomaManager.KomaPlace[x, m] == 1)
                    {
                        break;
                    }
                    else
                    {
                        Tile_Spawn(x, m);
                        break;
                    }
                }
                else
                {
                    if (KomaManager.KomaPlace[x, m] == 0)
                    {
                        Tile_Spawn(x, m);
                    }
                    else if (KomaManager.KomaPlace[x, m] == 1)
                    {
                        Tile_Spawn(x, m);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        for (int i = -1; i >= -8; i--)
        {
            m = z + i;
            if (m >= 0)
            {
                if (komaplayer == 1)
                {
                    if (KomaManager.KomaPlace[x, m] == 0)
                    {
                        Tile_Spawn(x, m);
                    }
                    else if (KomaManager.KomaPlace[x, m] == 1)
                    {
                        break;
                    }
                    else
                    {
                        Tile_Spawn(x, m);
                        break;
                    }
                }
                else
                {
                    if (KomaManager.KomaPlace[x, m] == 0)
                    {
                        Tile_Spawn(x, m);
                    }
                    else if (KomaManager.KomaPlace[x, m] == 1)
                    {
                        Tile_Spawn(x, m);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
    

    public void Kaku_Tile_Spawn(int x, int z, int komaplayer)
    {
        int b1 = -x + z;
        int b2 = x + z;
        //for (int i = 0; i <= 8; i++)
        //{
        //    int l1 = i - b1;
        //    int m1 = i + b1;
        //    int l2 = -i + b2;
        //    int m2 = -i + b2;
        //    if (i != x)
        //    {

        //        if (/*(i >= 0 && i <= 8) && */(m1 >= 0 && m1 <= 8))
        //        {
        //            Tile_Spawn(i, m1);
        //        }


        //        if (/*(i >= 0 && i <= 8) && */(m2>= 0 && m2 <= 8))
        //        {
        //            Tile_Spawn(i, m2);
        //        }
        //    }
        //}
        for(int i = x + 1; i <= 8; i++)
        {
            int l1 = i - b1;
            int m1 = i + b1;
            int l2 = -i + b2;
            int m2 = -i + b2;
            if(komaplayer == 1)
            {
                if(m1 <= 8)
                {
                    if(KomaManager.KomaPlace[i, m1] == 0)
                    {
                        Tile_Spawn(i, m1);
                    }else if (KomaManager.KomaPlace[i, m1] == 1)
                    {
                        break;
                    }
                    else
                    {
                        Tile_Spawn(i, m1);
                        break;
                    }
                }
            }else
            {
                if (m1 <= 8)
                {
                    if (KomaManager.KomaPlace[i, m1] == 0)
                    {
                        Tile_Spawn(i, m1);
                    }
                    else if (KomaManager.KomaPlace[i, m1] == 1)
                    {
                        Tile_Spawn(i, m1);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        for (int i = x + 1; i <= 8; i++)
        {
            int l2 = -i + b2;
            int m2 = -i + b2;
            if (komaplayer == 1)
            {
                if (m2 <= 8)
                {
                    if (KomaManager.KomaPlace[i, m2] == 0)
                    {
                        Tile_Spawn(i, m2);
                    }
                    else if (KomaManager.KomaPlace[i, m2] == 1)
                    {
                        break;
                    }
                    else
                    {
                        Tile_Spawn(i, m2);
                        break;
                    }
                }
            }
            else
            {
                if (m2 <= 8)
                {
                    if (KomaManager.KomaPlace[i, m2] == 0)
                    {
                        Tile_Spawn(i, m2);
                    }
                    else if (KomaManager.KomaPlace[i, m2] == 1)
                    {
                        Tile_Spawn(i, m2);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        for (int i = x - 1; i >= 0; i--)
        {
            int m1 = i + b1;
          
            if (komaplayer == 1)
            {
                if (m1 >= 0)
                {
                    if (KomaManager.KomaPlace[i, m1] == 0)
                    {
                        Tile_Spawn(i, m1);
                    }
                    else if (KomaManager.KomaPlace[i, m1] == 1)
                    {
                        break;
                    }
                    else
                    {
                        Tile_Spawn(i, m1);
                        break;
                    }
                }
            }
            else
            {
                if (m1 >= 0)
                {
                    if (KomaManager.KomaPlace[i, m1] == 0)
                    {
                        Tile_Spawn(i, m1);
                    }
                    else if (KomaManager.KomaPlace[i, m1] == 1)
                    {
                        Tile_Spawn(i, m1);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        for (int i = x - 1; i >= 0; i--)
        {
            int m2 = -i + b2;

            if (komaplayer == 1)
            {
                if (m2 >= 0)
                {
                    if (KomaManager.KomaPlace[i, m2] == 0)
                    {
                        Tile_Spawn(i, m2);
                    }
                    else if (KomaManager.KomaPlace[i, m2] == 1)
                    {
                        break;
                    }
                    else
                    {
                        Tile_Spawn(i, m2);
                        break;
                    }
                }
            }
            else
            {
                if (m2 >= 0)
                {
                    if (KomaManager.KomaPlace[i, m2] == 0)
                    {
                        Tile_Spawn(i, m2);
                    }
                    else if (KomaManager.KomaPlace[i, m2] == 1)
                    {
                        Tile_Spawn(i, m2);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}
