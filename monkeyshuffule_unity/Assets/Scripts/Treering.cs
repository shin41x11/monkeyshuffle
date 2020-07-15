using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treering : MonoBehaviour
{
    private float y_add_max = 1f; // 延びうる最大値。
    private float y_init;

    public ButtonState button;

    private Rigidbody rb; // 自身のrigidBody。再利用用に参照を保存。

    //[SerializeField]
    //private float RotationPower = 10f; // 回転のパワー

    // Start is called before the first frame update
    void Start()
    {
        // GetComponentは重たいので最初に呼んでおく
        rb = GetComponent<Rigidbody>();

        y_init = rb.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        rotate();
        buttonForce();
    }

    // 自動回転 
    // @todo 定期的に逆回転させる
    void rotate()
    {

        // 物理で回す版
        //rb.AddTorque(0f, RotationPower, 0f);

        // 角度をスクリプトで変更する版
        var angle = this.gameObject.transform.localEulerAngles;
        angle.y += Time.deltaTime * 60.0f;
        this.gameObject.transform.localEulerAngles = angle;
    }

    // ボタンが押されていたら一定の上向きの力を与える
    void buttonForce()
    {
        // 押されていた場合
        if (ReferenceEquals(button, null) == false && button.IsPressed() || (Input.GetKey(KeyCode.Space)))
        {
            // print("test1");
            // rigidbodyを設定するとリングが落下してしまうので、一旦コメントアウト
            //this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up);

            float y_max = y_init + y_add_max;
            if (this.gameObject.transform.position.y < y_max)
            {
                Vector3 pos = this.gameObject.transform.position;
                pos.y = Math.Min(pos.y + Time.deltaTime * 1f, y_max);
                this.gameObject.transform.position = pos;
               // print("y:" + pos.y);
            }

        }
        else
        {
            // 押されていなかった場合
            if (this.gameObject.transform.position.y > y_init)
            {
                // y方向に-0..1
                Vector3 pos = this.gameObject.transform.position;
                pos.y = pos.y - Time.deltaTime * 1f;
                pos.y = Math.Max(pos.y + Time.deltaTime * 1f, y_init);
                this.gameObject.transform.position = pos;
            }
        }
    }
}