using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    // 画面外に出たら削除
    void OnBecameInvisible()
    {
        print("Destory");
        Destroy(this.gameObject);
    }
}
