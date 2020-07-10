using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Monkey : MonoBehaviour
{
    private Dictionary<string, GameObject> collisionDict; // 衝突中オブジェクト辞書。keyにobject名(tree1等), valueにgameobjectを持つ
    private Rigidbody rb; // rigidBody。再利用用に参照を保存。

    // Start is called before the first frame update
    void Start()
    {
        collisionDict = new Dictionary<string, GameObject>();

        // GetComponentは重たいので最初に呼んでおく
        rb = gameObject.transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (collisionDict.Count == 1)
        {
            if (rb.isKinematic == false)
            {
                Debug.Log("captured:"); // ぶつかった
                gameObject.transform.parent = collisionDict.First().Value.transform;
                rb.isKinematic = true;
            }
        }
        else
        {
            if (rb.isKinematic == true)
            {
                Debug.Log("released:"); // 解放された
                gameObject.transform.parent = null;
                rb.isKinematic = false;
            }
        }
    }

    public void relayOnTriggerEnter(Collider collider)
    {
        string collisionObjectName = collider.gameObject.transform.parent.name;

        if (collisionDict.ContainsKey(collisionObjectName) == false)
        {
            collisionDict.Add(collisionObjectName, collider.gameObject);
            Debug.Log("Add:" + collisionObjectName); // ぶつかった相手の名前を取得
        }
    }



    public void relayOnTriggerExit(Collider collider)
    {
        string collisionObjectName = collider.gameObject.transform.parent.name;

        if (collisionDict.ContainsKey(collisionObjectName))
        {
            collisionDict.Remove(collisionObjectName);
            Debug.Log("Remove:" + collisionObjectName); // ぶつかった相手の名前を取得
        }
    }
}
