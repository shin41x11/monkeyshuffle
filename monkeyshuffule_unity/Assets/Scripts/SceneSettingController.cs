using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 調整・デバッグ用のパラメータ設定を行うマネージャ
public class SceneSettingController : MonoBehaviour
{

    public float timescale = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = timescale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
