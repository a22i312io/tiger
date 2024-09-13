using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koma: MonoBehaviour 
{
    // Start is called before the first frame update
    
    public enum Player
    {
        Player1,
        Player2
    }
    public Player player;
    public int x, z, n;

    public Koma(int x, int z, int n)
    {
        this.x = x;
        this.z = z;
        this.n = n;
    }

    

    

    void Update()
    {
        
    }

    //public virtual void OnMouseDown()
    //{

    //}
}
