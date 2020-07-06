using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{

    public ButtonState button;
    public GameObject prefab_monkey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        buttonListen();
    }

    void buttonListen()
    {
        // 押された場合
        if (ReferenceEquals(button, null) == false && button.IsDown())
        {
            GameObject new_monkey = Instantiate(prefab_monkey) as GameObject;

            // y方向に+1ずらした位置に猿を生成する
            Vector3 pos = this.gameObject.transform.position;
            pos.y = pos.y + 1f;
            new_monkey.transform.position = pos;

        }
    }

}
