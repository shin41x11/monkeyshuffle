﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treering : MonoBehaviour
{

    private float y_min = 1.8f; // 初期値。幹にくっついた状態
    private float y_max = 2.8f; // 最大値。

    public ButtonState button;

    private Rigidbody rb; // 自身のrigidBody。再利用用に参照を保存。

    [SerializeField]
    private float RotationPower = 10f; // 回転のパワー

    // Start is called before the first frame update
    void Start()
    {
        // GetComponentは重たいので最初に呼んでおく
        rb = GetComponent<Rigidbody>();
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

            if (this.gameObject.transform.position.y < y_max)
            {
                // y方向に+0..1
                Vector3 pos = this.gameObject.transform.position;
                pos.y = pos.y + 0.01f;
                this.gameObject.transform.position = pos;
               // print("y:" + pos.y);
            }

        }
        else
        {
            // 押されていなかった場合
            if (this.gameObject.transform.position.y > y_min)
            {
                // y方向に-0..1
                Vector3 pos = this.gameObject.transform.position;
                pos.y = pos.y - 0.01f;
                this.gameObject.transform.position = pos;
            }
        }
    }
}