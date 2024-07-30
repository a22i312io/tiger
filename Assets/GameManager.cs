using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameObject[] prefab = new GameObject[9];

    // Start is called before the first frame update
    void Start()
    {
        
        Koma[,] koma =  new Koma[9,9];

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (i == 0)
                {
                    if (j == 0 || j == 8)
                    {
                        koma[i,j] = new Kyousha(i, j, 0);
                        Instantiate(prefab[3], new Vector3(j, 0, i), Quaternion.identity);
                    }
                    if (j == 1 || j == 7)
                    {
                        koma[i,j] = new Keima(i, j, 0);
                        Instantiate(prefab[4], new Vector3(j, 0, i), Quaternion.identity);
                    }
                    if (j == 2 || j == 6)
                    {
                        koma[i,j] = new Gin(i, j, 0);
                        Instantiate(prefab[5], new Vector3(j, 0, i), Quaternion.identity);
                    }
                    if (j == 3 || j == 5)
                    {
                        koma[i,j] = new Kin(i, j, 0); 
                        Instantiate(prefab[6], new Vector3(j, 0, i), Quaternion.identity);
                    }
                    if (j == 4)
                    {
                        koma[i, j] = new Ou(i, j, 0);
                        Instantiate(prefab[7], new Vector3(j, 0, i), Quaternion.identity);
                    }
                }
                if (i == 1)
                {
                    if (j == 1)
                    {
                        koma[i, j] = new Kaku(i, j, 0);
                        Instantiate(prefab[1], new Vector3(j, 0, i), Quaternion.identity);
                    }
                    if (j == 7)
                    {
                        koma[i, j] = new Hisha(i, j, 0);
                        Instantiate(prefab[2], new Vector3(j, 0, i), Quaternion.identity);
                    }
                }
                if (i == 2)
                {
                    koma[i, j] = new Fu(i, j, 0);
                    Instantiate(prefab[0], new Vector3(j, 0, i), Quaternion.identity);
                }
                if (i == 6)
                {
                    koma[i, j] = new Fu(i, j, 1);
                    Instantiate(prefab[0], new Vector3(j, 0, i), Quaternion.Euler(180, 0, 0));
                }
                if (i == 7)
                {
                    if (j == 1)
                    {
                        koma[i, j] = new Kaku(i, j, 1);
                        Instantiate(prefab[1], new Vector3(j, 0, i), Quaternion.Euler(180, 0, 0));
                    }
                    if (j == 7)
                    {
                        koma[i, j] = new Hisha(i, j, 1);
                        Instantiate(prefab[2], new Vector3(j, 0, i), Quaternion.Euler(180, 0, 0));
                    }
                }
                if (i == 8)
                {
                    if (j == 0 || j == 8)
                    {
                        koma[i, j] = new Kyousha(i, j, 1);
                        Instantiate(prefab[3], new Vector3(j, 0, i), Quaternion.Euler(180, 0, 0));
                    }
                    if (j == 1 || j == 7)
                    {
                        koma[i, j] = new Keima(i, j, 1);
                        Instantiate(prefab[4], new Vector3(j, 0, i), Quaternion.Euler(180, 0, 0));
                    }
                    if (j == 2 || j == 6)
                    {
                        koma[i, j] = new Gin(i, j, 1);
                        Instantiate(prefab[5], new Vector3(j, 0, i), Quaternion.Euler(180, 0, 0));
                    }
                    if (j == 3 || j == 5)
                    {
                        koma[i, j] = new Kin(i, j, 1);
                        Instantiate(prefab[6], new Vector3(j, 0, i), Quaternion.Euler(180, 0, 0));
                    }
                    if (j == 4)
                    {
                        koma[i, j] = new Gyoku(i, j, 1);
                        Instantiate(prefab[8], new Vector3(j, 0, i), Quaternion.Euler(180, 0, 0));
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
            var layerMask = LayerMask.GetMask(new string[] { "Koma" });
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask)) {
                var clickedGameObject = hitInfo.collider.gameObject;
                clickedGameObject.transform.Translate(0, 0, 1);
                
            }
        }
    }
}
