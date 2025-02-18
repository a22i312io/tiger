using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Player
//{
//    // 表示モデルの種類
//    public enum eKind
//    {
//        police,
//        zombie,
//    }
//}

public class PlayerController : MonoBehaviour
{
    // 接地の状態（trueならジャンプできる）
    private bool _isGround = false;
    // プレイヤーモデルの種類
    Player.eKind _kind = Player.eKind.police;

    public bool IsGround { get { return _isGround; } }
    public Player.eKind Kind { get { return _kind; } set { _kind = value; } }

    void Start() { }

    void Update() { }

    // 位置と向きをリセットする
    public void Restart()
    {
        // ランダムに決定する
        float x = Random.value * 10.0f - 5.0f;
        float y = 4;
        float z = Random.value * 10.0f + 10.0f;

        // 表示モデルの種類によってz座標の符号を決める（向きも同様）
        if (_kind == 0)
        {
            z = -z;
        }

        transform.position = new Vector3(x, y, z);
        transform.rotation = Quaternion.Euler(0.0f, _kind == 0 ? 0.0f : 180.0f, 0.0f);

        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    // 衝突中に呼び出されるコールバック
    void OnCollisionStay(Collision collision) { _isGround = true; }

    // 衝突終了時に呼び出されるコールバック
    void OnCollisionExit(Collision collision) { _isGround = false; }
}