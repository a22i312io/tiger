using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private TileManager TileManager;
    public GameObject[] prefab = new GameObject[9];
    
    private GameObject LastKoma;
    private int Koma_player;


    // Start is called before the first frame update
    void Start()
    {

        GameObject obj = null;

        //Koma[,] koma =  new Koma[9,9];

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (i <= 2)
                {
                    if (i == 0)
                    {
                        if (j == 0 || j == 8)
                        {
                            //koma[i,j] = new Kyousha(i, j, 0);
                            obj = Instantiate(prefab[3], new Vector3(j, 0, i), Quaternion.identity);
                            var script = obj.GetComponent<Koma>();
                            script.player = Koma.Player.Player1;
                        }
                        if (j == 1 || j == 7)
                        {
                            //koma[i,j] = new Keima(i, j, 0);
                            obj = Instantiate(prefab[4], new Vector3(j, 0, i), Quaternion.identity);
                            var script = obj.GetComponent<Koma>();
                            script.player = Koma.Player.Player1;
                        }
                        if (j == 2 || j == 6)
                        {
                            //koma[i,j] = new Gin(i, j, 0);
                            obj = Instantiate(prefab[5], new Vector3(j, 0, i), Quaternion.identity);
                            var script = obj.GetComponent<Koma>();
                            script.player = Koma.Player.Player1;
                        }
                        if (j == 3 || j == 5)
                        {
                            //koma[i,j] = new Kin(i, j, 0); 
                            obj = Instantiate(prefab[6], new Vector3(j, 0, i), Quaternion.identity);
                            var script = obj.GetComponent<Koma>();
                            script.player = Koma.Player.Player1;
                        }
                        if (j == 4)
                        {
                            //koma[i, j] = new Ou(i, j, 0);
                            obj = Instantiate(prefab[7], new Vector3(j, 0, i), Quaternion.identity);
                            var script = obj.GetComponent<Koma>();
                            script.player = Koma.Player.Player1;
                        }
                    }
                    if (i == 1)
                    {
                        if (j == 1)
                        {
                            //koma[i, j] = new Kaku(i, j, 0);
                            obj = Instantiate(prefab[1], new Vector3(j, 0, i), Quaternion.identity);
                            var script = obj.GetComponent<Koma>();
                            script.player = Koma.Player.Player1;
                        }
                        if (j == 7)
                        {
                            //koma[i, j] = new Hisha(i, j, 0);
                            obj = Instantiate(prefab[2], new Vector3(j, 0, i), Quaternion.identity);
                            var script = obj.GetComponent<Koma>();
                            script.player = Koma.Player.Player1;
                        }
                    }
                    if (i == 2)
                    {
                        //koma[i, j] = new Fu(i, j, 0);
                        obj = Instantiate(prefab[0], new Vector3(j, 0, i), Quaternion.identity);
                        var script = obj.GetComponent<Koma>();
                        script.player = Koma.Player.Player1;
                    }
                    

                }
                if (i >= 6)
                {
                    if (i == 6)
                    {
                        //koma[i, j] = new Fu(i, j, 1);
                        obj = Instantiate(prefab[0], new Vector3(j, 0, i), Quaternion.Euler(180, 0, 0));
                        var script = obj.GetComponent<Koma>();
                        script.player = Koma.Player.Player2;
                    }
                    if (i == 7)
                    {
                        if (j == 1)
                        {
                            //koma[i, j] = new Kaku(i, j, 1);
                            obj = Instantiate(prefab[2], new Vector3(j, 0, i), Quaternion.Euler(180, 0, 0));
                            var script = obj.GetComponent<Koma>();
                            script.player = Koma.Player.Player2;
                        }
                        if (j == 7)
                        {
                            //koma[i, j] = new Hisha(i, j, 1);
                            obj = Instantiate(prefab[1], new Vector3(j, 0, i), Quaternion.Euler(180, 0, 0));
                            var script = obj.GetComponent<Koma>();
                            script.player = Koma.Player.Player2;
                        }
                    }
                    if (i == 8)
                    {
                        if (j == 0 || j == 8)
                        {
                            //koma[i, j] = new Kyousha(i, j, 1);
                            obj = Instantiate(prefab[3], new Vector3(j, 0, i), Quaternion.Euler(180, 0, 0));
                            var script = obj.GetComponent<Koma>();
                            script.player = Koma.Player.Player2;
                        }
                        if (j == 1 || j == 7)
                        {
                            //koma[i, j] = new Keima(i, j, 1);
                            obj = Instantiate(prefab[4], new Vector3(j, 0, i), Quaternion.Euler(180, 0, 0));
                            var script = obj.GetComponent<Koma>();
                            script.player = Koma.Player.Player2;
                        }
                        if (j == 2 || j == 6)
                        {
                            //koma[i, j] = new Gin(i, j, 1);
                            obj = Instantiate(prefab[5], new Vector3(j, 0, i), Quaternion.Euler(180, 0, 0));
                            var script = obj.GetComponent<Koma>();
                            script.player = Koma.Player.Player2;
                        }
                        if (j == 3 || j == 5)
                        {
                            // koma[i, j] = new Kin(i, j, 1);
                            obj = Instantiate(prefab[6], new Vector3(j, 0, i), Quaternion.Euler(180, 0, 0));
                            var script = obj.GetComponent<Koma>();
                            script.player = Koma.Player.Player2;
                        }
                        if (j == 4)
                        {
                            //koma[i, j] = new Gyoku(i, j, 1);
                            obj = Instantiate(prefab[8], new Vector3(j, 0, i), Quaternion.Euler(180, 0, 0));
                            var script = obj.GetComponent<Koma>();
                            script.player = Koma.Player.Player2;
                        }
                    }


                }
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {
         
        if (Input.GetMouseButtonDown(0))
        {
            
            var layerMask1 = LayerMask.GetMask(new string[] { "Koma" });
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            var manager = GameObject.Find("TileManager");
            
            var tileManager = manager.GetComponent<TileManager>();
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask1)) {
                var clickedKoma = hitInfo.collider.gameObject;
                Koma koma = clickedKoma.GetComponent<Koma>();
                var name = clickedKoma.tag;
                int Koma_x = Mathf.RoundToInt(clickedKoma.transform.position.x);
                int Koma_z = Mathf.RoundToInt(clickedKoma.transform.position.z);
                
                if(koma.player == Koma.Player.Player1)
                {
                    Koma_player = 1;
                }
                else
                {
                    Koma_player = 2;
                }
                tileManager.Tile_Destroy();
                if (name == "Fu")
                {
                    tileManager.Fu_Tile_Spawn(Koma_x, Koma_z, Koma_player);
                }

                if (name == "Ou")
                {
                    tileManager.Ou_Tile_Spawn(Koma_x, Koma_z, Koma_player);
                }
                if (name == "Kin")
                {
                    tileManager.Kin_Tile_Spawn(Koma_x, Koma_z, Koma_player);
                }
                if (name == "Gin")
                {
                    tileManager.Gin_Tile_Spawn(Koma_x, Koma_z, Koma_player);
                }
                if (name == "Keima")
                {
                    tileManager.Keima_Tile_Spawn(Koma_x, Koma_z, Koma_player);
                }
                if (name == "Kyousha")
                {
                    tileManager.Kyousha_Tile_Spawn(Koma_x, Koma_z, Koma_player);
                }
                if (name == "Hisha")
                {
                    tileManager.Hisha_Tile_Spawn(Koma_x, Koma_z, Koma_player);
                }
                if (name == "Kaku")
                {
                    tileManager.Kaku_Tile_Spawn(Koma_x, Koma_z, Koma_player);
                }
                //clickedGameObject.transform.Translate(0, 0, 1);
                LastKoma = clickedKoma;
            }

            var layerMask2 = LayerMask.GetMask(new string[] { "Tile" });
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask2))
            {
                var clickedTile = hitInfo.collider.gameObject;
                var Tile_x = Mathf.RoundToInt(clickedTile.transform.position.x);
                var Tile_z = Mathf.RoundToInt(clickedTile.transform.position.z);
                int Koma_x = Mathf.RoundToInt(LastKoma.transform.position.x);
                int Koma_z = Mathf.RoundToInt(LastKoma.transform.position.z);
                tileManager.Tile_Destroy();

                LastKoma.transform.position = new Vector3(Tile_x, 0, Tile_z);

                Koma lastkoma = LastKoma.GetComponent<Koma>();


                if (lastkoma.player == Koma.Player.Player1)
                {
                    KomaManager.KomaMove(Koma_x, Koma_z, Tile_x, Tile_z, 1);
                }
                else
                {
                    KomaManager.KomaMove(Koma_x, Koma_z, Tile_x, Tile_z, 2);
                }
            }
        }
    }
}
