using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 一定時間毎に猿を新規生成して落下させるテストシーン向けのスクリプト
// @todo 発生座標の範囲が固定値なのが美しくないので、自身のカバーする範囲から自動取得するように変更する
public class Cloud : MonoBehaviour
{

    // prefabとなるmonkey
    public GameObject prefab_bullet_monkey; // 落下させる猿の元プレハブ
    public float span = 0.1f; // 猿を
    private float addTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        addTime += Time.deltaTime;

        if(addTime > span)
        {
            addTime -= span;

            float posx, posy, posz;
            posx = (float)(0.5 - Random.value) * 4; // -2～2を一律ランダム生成
            posy = 3;
            posz = (float)(0.5 - Random.value) * 3; // -1.5～1,5を一律ランダム生成

            Vector3 pos = new Vector3(posx, posy, posz);

            // prefab_bullet_monkeyをposの位置に回転させずに生成する
            GameObject bullet_monkey = Instantiate(prefab_bullet_monkey, pos, Quaternion.identity) as GameObject;
        }
    }
}
