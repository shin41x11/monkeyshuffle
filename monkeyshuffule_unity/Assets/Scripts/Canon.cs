using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public FixedJoystick joystick;

    // prefabとなるmonkey
    public GameObject prefab_bullet_monkey;
    //銃口
    public GameObject muzzle;

    private float speed = 400;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // ジョイスティックを見て、射出台の向き変更
        //     print("test1");
        transform.Rotate(joystick.Horizontal, joystick.Vertical, 0);

        // ジョイスティックの状態表示
        //print("Horizontal: " + joystick.Horizontal);
        //print("Vertical: " + joystick.Vertical);

        // 一先ず押されたら発射にする。押されてる状態なら発射に変えたい。
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //     print("test1");
            GameObject bullet_monkey = Instantiate(prefab_bullet_monkey) as GameObject;

            Vector3 force;

            force = transform.up * speed;

            // Rigidbodyに力を加えて発射
            bullet_monkey.GetComponent<Rigidbody>().AddForce(force);

            // 弾丸の位置を調整
            bullet_monkey.transform.position = muzzle.transform.position;
        }
    }
}
