using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
// 猿の両手のスクリプト
// OnCollision系関数を親に渡すのみ
/// </summary>
public class MonkeyArm : MonoBehaviour
{

//    private Rigidbody rb; // rigidBody。再利用用に参照を保存。
    private Monkey parentObject;

    // Start is called before the first frame update
    void Start()
    {
//        rb = gameObject.transform.GetComponent<Rigidbody>();
        parentObject = gameObject.transform.parent.gameObject.GetComponent<Monkey>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        // 猿の両手の上側(吹っ飛び判定部分)
        if (gameObject.transform.CompareTag("MonkeyArmUpper"))
        {
            if (collider.transform.CompareTag("TreeRingLower"))
            {



                parentObject.TriggerEnterMonkeyArmUpperAndTreeRingLower(collider);
            }
        }

        // 猿の両手の下側(くっつき判定部分)
        else if (gameObject.transform.CompareTag("MonkeyArmLower"))
        {
            if (collider.transform.CompareTag("TreeRingUpper"))
            {
                Vector3 connectPoint = collider.ClosestPoint(transform.position);
                parentObject.TriggerEnterMonkeyArmLowerAndTreeRingUpper(collider, gameObject, connectPoint);
            }
        }
    }



}
