using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 猿
/// </summary>
public class Monkey : MonoBehaviour

{    private Rigidbody rb; // rigidBody。再利用用に参照を保存。
    private bool isConnectionLock; // 接続状態を固定しているか。状態変更から1秒間は固定する。
    private Branch branch; // 発射元の枝
    private GoalTree goalTree; 

    // Start is called before the first frame update
    void Start()
    {
        // GetComponentは重たいので最初に呼んでおく
        rb = gameObject.transform.GetComponent<Rigidbody>();
        isConnectionLock = false;

        goalTree = GameObject.Find("treeGoal").GetComponent<GoalTree>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void setBranch(Branch value)
    {
        branch = value;
    }

    /// <summary>
    /// 猿を削除して、枝の待機に戻す
    /// </summary>
    public void disappear()
    {
        branch.increaseWaitingMonkey();
        Destroy(gameObject);
    }

    /// <summary>
    /// 猿が木の輪の下側に接触した際の弾き飛ばす
    /// </summary>
    /// <param name="treeGameObject">接触した木の輪。木の輪の中心から爆発による力を発生させる</param>
    // 猿が木の輪の下側に接触した際の弾き飛ばす
    void Explosion(GameObject treeGameObject)
    {
        float pow = 500f;
        float radius = 100f;

        print("explosion!" + gameObject.name);
        rb.AddExplosionForce(pow, treeGameObject.transform.position,radius);
        StartCoroutine(Spin());
    }

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
}
