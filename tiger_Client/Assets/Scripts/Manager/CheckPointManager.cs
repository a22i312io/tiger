using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private List<CheckPoint> _checkpoints = new List<CheckPoint>();
    private int _next = 0;
    private int num;
    [SerializeField] Collider _goalline;
    void Start()
    {
        num = this.transform.childCount;
        if (num == 0)
        {
            return;
        }
        else
        {
            GetChild(this.gameObject, 0, num);
        }
        for(int i = 0; i < num; i++)
        {
            _checkpoints[i].Number = i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_checkpoints[_next].IsPassed)
        {
            _checkpoints[_next].IsPassed = false;
            if (_next == num-1)
            {
                _goalline.enabled = true;
                _next = 0;
                
            }
            else
            {
                _next++;
            }
            _checkpoints[_next].OnCollider();
        }
    }

    private void GetChild(GameObject obj, int index, int max)
    {
        _checkpoints.Add(transform.GetChild(index).GetComponent<CheckPoint>()) ;
        index++;
        if(index< max)
        {
            GetChild(obj, index, max);
        }
    }
}
