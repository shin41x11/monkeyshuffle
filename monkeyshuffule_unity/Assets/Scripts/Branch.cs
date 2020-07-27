using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 猿が降りてくる枝
public class Branch : MonoBehaviour
{

    public ButtonState button;
    public GameObject prefab_monkey;
    private int waitingMonkey; // 枝から降りれる猿
    private int waitingMonkeMax = 5; //枝から降りれる猿の最大数

    // Start is called before the first frame update
    void Start()
    {
        waitingMonkey = waitingMonkeMax;
    }

    // Update is called once per frame
    void Update()
    {
        buttonListen();
    }

    void buttonListen()
    {
        if (ReferenceEquals(button, null) == false && button.IsDown())
        {
            if(waitingMonkey > 0)
            {
                appearMonkey();
                waitingMonkey--;
            }
        }
    }

    /// <summary>
    /// 待機状態の猿をゲームに出現させる
    /// </summary>
    void appearMonkey()
    {
        GameObject new_monkey = Instantiate(prefab_monkey) as GameObject;

        Monkey monkey = new_monkey.GetComponent<Monkey>();
        monkey.name = "monkey" + (waitingMonkeMax - waitingMonkey + 1); 
        monkey.setBranch(this.gameObject.GetComponent<Branch>());
        

        // y方向に+1ずらした位置に猿を生成する
        Vector3 pos = this.gameObject.transform.position;
        pos.y = pos.y + 1f;
        new_monkey.transform.position = pos;

    }

    public void increaseWaitingMonkey()
    {
        waitingMonkey++;
    }

}
