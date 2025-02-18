using PlayerSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Car.Move {
    public class Hover : MonoBehaviour
    {
        // タイヤの位置
        private List<Vector3> _wheelPositions = new List<Vector3>();
        // タイヤのオフセット
        private List<Vector3> _offsets = new List<Vector3>();

        private float _springStrength = 5f;

        private bool _isHover = true;

        private Core _core;

        //Vector3 _currentPosition;
        // 現在姿勢
        //Quaternion _currentRotation;
        //private Vector3 position_UpperRight;
        //private Vector3 position_UpperLeft;
        //private Vector3 position_LowerRight;
        //private Vector3 position_LowerLeft;
        public bool IsHover { get { return _isHover; } set { _isHover = value; } }

        void Start()
        {
            _core = GetComponent<Core>();

            _offsets.Add(new Vector3(1f, -0.4f, 1.85f));  // 右前輪
            _offsets.Add(new Vector3(-1f, -0.4f, 1.85f)); // 左前輪
            _offsets.Add(new Vector3(1f, -0.4f, -1.85f)); // 右後輪
            _offsets.Add(new Vector3(-1f, -0.4f, -1.85f)); // 左後輪
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (_core.Rb != null)
            {
                if (_isHover)
                {
                    _wheelPositions.Clear(); // 位置をリセット

                    //_currentPosition = gameObject.transform.position;
                    //_currentRotation = gameObject.transform.rotation;

                    // 各ホイールの位置を計算
                    foreach (var offset in _offsets)
                    {
                        _wheelPositions.Add(_core.CurrentPosition + _core.CurrentRotation * offset);
                    }

                    // 全ホイールにリフトフォースを適用
                    foreach (var position in _wheelPositions)
                    {
                        ApplyHover(position);
                    }
                    

                    //Vector3 offsetUpperRight = new Vector3(1f, -0.4f, 1.85f);
                    //Vector3 offsetUpperLeft = new Vector3(-1f, -0.4f, 1.85f);
                    //Vector3 offsetLowerRight = new Vector3(1f, -0.4f, -1.85f);
                    //Vector3 offsetLowerLeft = new Vector3(-1f, -0.4f, -1.85f);

                    //position_UpperRight = _currentPosition + _currentRotation * offsetUpperRight;
                    //position_UpperLeft = _currentPosition + _currentRotation * offsetUpperLeft;
                    //position_LowerRight = _currentPosition + _currentRotation * offsetLowerRight;
                    //position_LowerLeft = _currentPosition + _currentRotation * offsetLowerLeft;

                    //ApplyHover(position_UpperRight);
                    //ApplyHover(position_UpperLeft);
                    //ApplyHover(position_LowerRight);
                    //ApplyHover(position_LowerLeft);
                }
            }
        }

        private void ApplyHover(Vector3 position)
        {

            Ray ray = new Ray(position, -gameObject.transform.up);
            Debug.DrawRay(position, -gameObject.transform.up, Color.red, 2.0f);

            if (Physics.Raycast(ray, out RaycastHit hit, 100, _core.GroundLayer))
            {
                // 地面との距離
                float distance = _core.MaxDistance - hit.distance;

                Vector3 groundNormal = hit.normal;
                float verticalVelocity = Vector3.Dot(_core.Rb.GetPointVelocity(position), groundNormal);

                float springForce = _springStrength * distance;
                float dampingForce = _core.DampingForce * verticalVelocity;

                float force = springForce - dampingForce;
                _core.Rb.AddForceAtPosition(groundNormal * force, position);
            }
        }
    }
}