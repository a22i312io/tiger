using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            Instantiate(prefab, new Vector3(-4.0f + i, 0, -4.0f), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            Instantiate(prefab, new Vector3(4.0f - i, 0, 4.0f), Quaternion.EulerRotation(0, Mathf.PI, 0));
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
