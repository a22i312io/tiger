using Car;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour
{
    // 接地の状態（trueならジャンプできる）
    private bool _isGround = false;
    // プレイヤーモデルの種類
    //private Player.eKind _kind = Player.eKind.police;
    // 移動（アニメーションの）速度
    private float _speed = 0.0f;
    // プレイヤーを動かすためにRigidbodyに加える力
    private Vector3 _force = Vector3.zero;
    // Modelオブジェクト
    private GameObject model = null;
    // Modelオブジェクトの子（TT_demo_policeとTT_demo_zombieノード）
    private List<GameObject> children = new List<GameObject>();

    public bool IsGround { get { return _isGround; } }

    //public Player.eKind Kind { get { return _kind; } set { _kind = value; } }
    public float Speed { set { _speed = value; } }
    public Vector3 Force { get { return _force; } set { _force = value; } }

    void Start()
    {
        // Modelノードを探す
        model = transform.Find("Model").gameObject;
        if (model)
        {
            // Modelノードの子をchildrenに詰む
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
            // 現在のKind（policeまたはzombie）を名前に含むノードだけを有効にする（他を消す）
            //obj.SetActive(obj.name.Contains(_kind.ToString()));

            // 有効化されたノードにアニメーション速度を反映する
            if (obj.activeSelf)
            {
                Animator anim = obj.GetComponent<Animator>();
                if (anim)
                {
                    // 0.25乗しているのは単なる調整のため
                    float animSpeed = Mathf.Pow(_speed, 0.25f);
                    anim.SetFloat("Speed", animSpeed);
                }
            }
        }
    }

    // Sleep（サーバーで衝突判定やシミュレーションを行う状態）
    public void Sleep()
    {
        // Colliderを無効化
        GetComponent<SphereCollider>().enabled = false;
        // Rigidbodyを無効化（位置を姿勢指定のみで動かす）
        GetComponent<Rigidbody>().isKinematic = true;
    }

    // WakeUp（ローカルで衝突判定やシミュレーションを行う状態）
    public void WakeUp()
    {
        // Colliderを有効化
        GetComponent<SphereCollider>().enabled = true;
        // Rigidbodyを有効化
        GetComponent<Rigidbody>().isKinematic = false;
    }

    // 位置と向きをリセットする
    //public void Restart()
    //{
    //    // ランダムに決定する
    //    float x = Random.value * 10.0f - 5.0f;
    //    float y = 4;
    //    float z = Random.value * 10.0f + 10.0f;

    //    // 表示モデルの種類によってz座標の符号を決める（向きも同様）
    //    if (_kind == 0)
    //    {
    //        z = -z;
    //    }

    //    transform.position = new Vector3(x, y, z);
    //    transform.rotation = Quaternion.Euler(0.0f, _kind == 0 ? 0.0f : 180.0f, 0.0f);

    //    // アニメーション速度を初期化
    //    _speed = 0.0f;

    //    // フォースを初期化
    //    _force = Vector3.zero;

    //    // Rigidbodyの移動速度と回転速度を初期化
    //    GetComponent<Rigidbody>().velocity = Vector3.zero;
    //    GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    //}

    // 衝突中に呼び出されるコールバック
    void OnCollisionStay(Collision collision) { _isGround = true; }

    // 衝突終了時に呼び出されるコールバック
    void OnCollisionExit(Collision collision) { _isGround = false; }


    // プレイヤーを動かすためにRigidbodyに加える力を計算する
    //public void UpdateForce(PlayerBase player)
    //{
    //    float baseForce = 600.0f;
    //    float jumpForce = 24.0f * 8.0f;

    //    // カメラの前方と右方向のベクトル
    //    Vector3 vForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
    //    Vector3 vRight = Camera.main.transform.right;

    //    Vector3 inputValue = player.InputValue;
    //    _force = baseForce * Time.deltaTime * (vForward * inputValue.y + vRight * inputValue.x);

    //    // ジャンプ
    //    if (player.IsJump && _isGround)
    //    {
    //        _force.y += jumpForce;
    //    }
    //}
}