using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Car.Player
{
    public class PlayerController : MonoBehaviour
    {
        private bool _isCamera = false;
        [SerializeField] private GameObject _cameraTarget;
        void Start() {
            
        }

        void Update() {
            if (!_isCamera)
            {
                _cameraTarget = GameObject.Find("CameraTarget");
                // カメラの注視点にプレイヤー位置をコピー
                _cameraTarget.transform.position = this.transform.position;
                _cameraTarget.transform.parent = this.transform;
                _cameraTarget.transform.localPosition += new Vector3(0, 0f, 1.7f);
                _isCamera = true;
            }
        }

        // 位置と向きをリセットする
       

       
    }
}