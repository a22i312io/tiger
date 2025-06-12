using Car;
using Car.Move;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace Car.Player
{
    public class PlayerController : MonoBehaviour
    {
        // 接地の状態
        private bool _isGround = false;

        private float _speed = 0.0f;
        
        private Vector3 _force = Vector3.zero;
       
        private GameObject model = null;
      
        private List<GameObject> children = new List<GameObject>();

        public bool IsGround { get { return _isGround; } }

        //public Player.eKind Kind { get { return _kind; } set { _kind = value; } }
        public float Speed { set { _speed = value; } }
        public Vector3 Force { get { return _force; } set { _force = value; } }

        void Start()
        {
            
            model = transform.Find("Model").gameObject;
            if (model)
            {
                
                Transform transforms = model.GetComponentInChildren<Transform>();
                foreach (Transform obj in transforms)
                {
                    children.Add(obj.gameObject);
                }
            }
        }

        void Update()
        {
            foreach (GameObject obj in children)
            {
               
                if (obj.activeSelf)
                {
                    Animator anim = obj.GetComponent<Animator>();
                    if (anim)
                    {
               
                        float animSpeed = Mathf.Pow(_speed, 0.25f);
                        anim.SetFloat("Speed", animSpeed);
                    }
                }
            }
        }

        // Sleep（サーバーで衝突判定やシミュレーションを行う状態）
        public void Sleep()
        {

            GetComponent<BoxCollider>().enabled = false;
            
            GetComponent<Rigidbody>().isKinematic = true;

            GetComponent<Core>().enabled = false;

            GetComponent<Move.Move>().enabled = false;

            GetComponent<Input>().enabled = false;

            GetComponent<Accelerator>().enabled = false;

            GetComponent<Hover>().enabled = false;

            GetComponent<Steering>().enabled = false;
        }

        // WakeUp（ローカルで衝突判定やシミュレーションを行う状態）
        public void WakeUp()
        {
          
            GetComponent<SphereCollider>().enabled = true;
            
            GetComponent<Rigidbody>().isKinematic = false;

            GetComponent<Core>().enabled = true;

            GetComponent<Move.Move>().enabled = true;

            GetComponent<Input>().enabled = true;

            GetComponent<Accelerator>().enabled = true;

            GetComponent<Hover>().enabled = true;

            GetComponent<Steering>().enabled = true;
        }

    }
}