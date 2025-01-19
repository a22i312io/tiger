using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

namespace PlayerSystem
{
    public class Controller : MonoBehaviour
    {
        private GameObject model = null;

        private List<GameObject> gameObjects = new List<GameObject>();

        void Start()
        {
            // Modelƒm[ƒh‚ğ’T‚·
            model = transform.Find("Model").gameObject;
            if (model)
            {
                // Modelƒm[ƒh‚Ìq‚ğchildren‚É‹l‚Ş
                Transform transforms = model.GetComponentInChildren<Transform>();
                foreach (Transform obj in transforms)
                {
                    gameObjects.Add(obj.gameObject);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }


    }
}
