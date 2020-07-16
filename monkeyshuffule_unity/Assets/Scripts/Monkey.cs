using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Monkey : MonoBehaviour
{
    private Rigidbody rb; // rigidBody。再利用用に参照を保存。
    private bool isConnectionLock; // 接続状態を固定しているか。状態変更から1秒間は固定する。

    // Start is called before the first frame update
    void Start()
    {
        // GetComponentは重たいので最初に呼んでおく
        rb = gameObject.transform.GetComponent<Rigidbody>();
        isConnectionLock = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Explosion(GameObject treeGameObject)
    {
        float pow = 1.0f;
        float radius = 3.0f;

        print("explosion!");
        rb.AddExplosionForce(pow, treeGameObject.transform.position,radius);
    }

    void ReleaseTree()
    {
        gameObject.transform.parent = null;
        rb.isKinematic = false;
        LockConnection();
    }

    void ConnectTree(GameObject treeGameObject)
    {
        gameObject.transform.parent = treeGameObject.transform;
        rb.isKinematic = true;
        LockConnection();
    }

    void LockConnection()
    {
        isConnectionLock = true;
        //1秒後にロックを解除する
        StartCoroutine(WaitAndUnlockConnection());
    }

    IEnumerator WaitAndUnlockConnection()
    {
        yield return new WaitForSeconds(1f);
        isConnectionLock = false;
    }

    public void TriggerEnterMonkeyArmUpperAndTreeRingLower(Collider collider)
    {
        if (isConnectionLock) return;
        ReleaseTree();
        Explosion(collider.gameObject);
    }

    public void TriggerEnterMonkeyArmLowerAndTreeRingUpper(Collider collider)
    {
        if (isConnectionLock) return;
        ConnectTree(collider.gameObject);
    }
}
