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

    }

    public void increaseWaitingMonkey()
    {
        waitingMonkey++;
        waitingMonkeyText.text = "×" + waitingMonkey;
    }

    /// <summary>
    /// 猿を削除して、枝の待機に戻す
    /// </summary>
    public void disappear()
    {
//        branch.increaseWaitingMonkey();
        Destroy(gameObject);
    }

    /// <summary>
    /// ゴールに接触した猿のカウンタを反映する
    /// </summary>
    public void goal()
    {
        goalMonkey++;
        goalMonkeyText.text = "×" + goalMonkey;

    }

    public int getWaitingMonkey()
    {
        return waitingMonkey;
    }

    /*
    /// <summary>
    /// 猿と木の親子関係を解除する
    /// </summary>
    void ReleaseTree()
    {
        gameObject.transform.parent = null;
        rb.isKinematic = false;
        LockConnection();
    }

    /// <summary>
    /// 猿と木の親子関係を設定する
    /// </summary>
    /// <param name="treeGameObject"></param>
    void ConnectTreeRing(GameObject treeGameObject)
    {
        // TreeRingの子オブジェクトにする(接触部のシリンダーの子オブジェクトにすると、計算が煩雑になる)
        rb.transform.parent = treeGameObject.transform.parent.gameObject.transform.parent.gameObject.transform;

        // 腕の中心でぶら下がるように微調整する
        float fixed_rad = 1.3f;// scale1の際の木と猿の距離
        float rad = (new Vector3(0, 0, 0) - new Vector3(rb.transform.localPosition.x, 0, rb.transform.localPosition.z)).magnitude; 
        print("before rad:" + rad + " rot:" + rb.transform.localRotation.eulerAngles + " pos:" + rb.transform.localPosition);

        rb.transform.localPosition = new Vector3(rb.transform.localPosition.x / rad * fixed_rad, -0.8f, rb.transform.localPosition.z / rad * fixed_rad);
        
        print("after rot:" + rb.transform.localRotation.eulerAngles + " pos:" + rb.transform.localPosition);
//        print("rotY" + rotY);

        rb.isKinematic = true;
        LockConnection();
    }

    /// <summary>
    /// 猿と木の親子関係を１秒間固定する
    /// </summary>
    void LockConnection()
    {
        isConnectionLock = true;
        StartCoroutine(WaitAndUnlockConnection());
    }

    /// <summary>
    /// 猿と木の親子関係を１秒間固定するコルーチン
    /// </summary>
    IEnumerator WaitAndUnlockConnection()
    {
        print("begin lock");
        yield return new WaitForSeconds(1f);
        isConnectionLock = false;
        print("release lock");

    }

    /// <summary>
    /// 猿を１秒間回転させるコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator Spin()
    {
        int i;
        for (i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.1f);
            float angle = 1f;
            transform.Rotate(transform.forward, angle);
        }
    }

    /// <summary>
    /// ゴールの木にぶつかった際にゴールの木の子オブジェクトにする処理
    /// </summary>
    private void connectTreeGoal(GameObject treeGameObject)
    {
        rb.transform.parent = treeGameObject.transform;
    }

    private void goalPond()
    {
        
        goalTree.goal();
        Destroy(gameObject);
    }

    /// <summary>
    /// 「猿の腕の上」と「木の輪の下」の接触時に呼ばれるトリガーを受ける関数
    ///  猿と木の親子関係を解除し、猿を吹き飛ばす
    /// </summary>
    /// <param name="collider"></param>
    public void TriggerEnterMonkeyArmUpperAndTreeRingLower(GameObject colliderGameObject)
    {
        if (isConnectionLock) return;

        // GoalTreeの子オブジェクトだったら何もしない
        if (gameObject.transform.parent && gameObject.transform.parent.CompareTag("GoalTree")) return;

        Debug.Log("OnTriggerEnter " + gameObject.name + "  " + getOriginGameobject(colliderGameObject).name + " MonkeyArmUpper ");
        Debug.Log("Monkey pos:" + rb.transform.position  + "rotation:" + rb.transform.rotation);
        ReleaseTree();
        Explosion(colliderGameObject);
    }

    /// <summary>
    /// 「猿の腕の下」と「木の輪の腕」の接触時に呼ばれるトリガーを受ける関数
    /// 猿を木の子オブジェクトに設定する
    /// </summary>
    public void TriggerEnterMonkeyArmLowerAndTreeRingUpper(GameObject colliderGameObject)
    {
        if (isConnectionLock) return;

        // GoalTreeの子オブジェクトだったら何もしない
        if (gameObject.transform.parent && gameObject.transform.parent.CompareTag("GoalTree")) return;

        Debug.Log("OnTriggerEnter " + rb.name + "  " + getOriginGameobject(colliderGameObject).name + " MonkeyArmUpper and TreeRingLower");
        Debug.Log("Monkey pos:" + rb.transform.position + "rotation:" + rb.transform.rotation.eulerAngles);
        ConnectTreeRing(colliderGameObject);
    }

    /// <summary>
    /// 元祖のオブジェクト名(tree1等)を再帰的に取得する
    /// </summary>
    /// <param name="myGameObject"></param>
    /// <returns></returns>
    private GameObject getOriginGameobject(GameObject myGameObject)
    {
        if (myGameObject.transform.parent)
        {
            return getOriginGameobject(myGameObject.transform.parent.gameObject);
        }

        return myGameObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
 //       print("oncollisionenter:" + collision.gameObject.tag);
        // 地面に落下した猿の消失処理
        if (collision.gameObject.transform.CompareTag("Ground"))
        {
            disappear();
        }



    }

    private void OnTriggerEnter(Collider collider)
    {
        // ゴールの木に触れた際の親オブジェクト変更処理
        if (collider.gameObject.transform.CompareTag("GoalTree"))
        {
            connectTreeGoal(collider.gameObject);
        }

        // 欠けている木の輪に触れた際の親オブジェクト解除処理
        if (collider.gameObject.transform.CompareTag("TreeRingPhantom"))
        {
            ReleaseTree();
        }

        // ゴールの池に落下した猿の消失処理
        if (collider.gameObject.transform.CompareTag("GoalPond"))
        {
            goalPond();
        }
    }
    */
}
