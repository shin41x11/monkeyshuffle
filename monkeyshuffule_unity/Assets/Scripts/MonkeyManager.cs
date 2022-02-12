using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 猿を管理するマネージャー。プレイヤー毎に所持する。
/// プレイヤーに紐づくオブジェクトは生成時に、このスクリプトへの参照を設定される。
/// 名前はMonkeyManger1 or MonkeyManager2とすることで、以下のようにアクセスする
/// monkeyManger = GameObject.Find("MonkeyManager" + playerNo);
/// 
/// </summary>
public class MonkeyManager : MonoBehaviour

{
    public int playerNo; // プレイヤー番号( 1 or 2)
    private int waitingMonkey; // 枝から降りれる猿
    private int waitingMonkeMax = 10; //枝から降りれる猿の最大数
    private Text waitingMonkeyText;

    private int goalMonkey; //
    private Text goalMonkeyText;

    public Monkey monkeyPrefab;
    private Transform branch; // 猿の生成ポイントを示す

    private ButtonState pickupButton;

    [SerializeField] private SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {

        branch = GameObject.Find("Branch" + playerNo).GetComponent<Transform>();
        pickupButton = GameObject.Find("PickupButton" + playerNo).GetComponent<ButtonState>();

        waitingMonkey = waitingMonkeMax;

        waitingMonkeyText = GameObject.Find("WaitingCounter" + playerNo).GetComponent<Text>();
        waitingMonkeyText.text = "×" + waitingMonkey;

        goalMonkey = 0;
        goalMonkeyText = GameObject.Find("GoalCounter" + playerNo).GetComponent<Text>();
        goalMonkeyText.text = "×" + goalMonkey;
    }

    // Update is called once per frame
    void Update()
    {
        listenPickupButton();
    }

    void listenPickupButton()
    {
        if (ReferenceEquals(pickupButton, null) == false && pickupButton.IsDown())
        {
            if (waitingMonkey > 0)
            {
                appearMonkey();
                waitingMonkey--;
                waitingMonkeyText.text = "×" + waitingMonkey;
            }
        }
    }

    /// <summary>
    /// 待機状態の猿をゲームに出現させる
    /// </summary>
    void appearMonkey()
    {
        Monkey new_monkey = Instantiate(monkeyPrefab) as Monkey;

        Monkey monkey = new_monkey.GetComponent<Monkey>();
        monkey.setMonkeyManager(this);
        monkey.name = "monkey" + (waitingMonkeMax - waitingMonkey + 1);

        // y方向に+1ずらした位置に猿を生成する
        Vector3 pos = branch.position;
        pos.y = pos.y + 1f;
        new_monkey.transform.position = pos;

        soundManager.Play("monkey_appear");
    }

    public void increaseWaitingMonkey()
    {
        waitingMonkey++;
        waitingMonkeyText.text = "×" + waitingMonkey;
    }

    /// <summary>
    /// ゴールに接触した猿のカウンタを反映する
    /// </summary>
    public void goal()
    {
        goalMonkey++;
        goalMonkeyText.text = "×" + goalMonkey;
        soundManager.Play("monkey_goal");
    }

    public int getWaitingMonkey()
    {
        return waitingMonkey;
    }

    public void playSound(string name)
    {
        soundManager.Play(name);
    }
}
