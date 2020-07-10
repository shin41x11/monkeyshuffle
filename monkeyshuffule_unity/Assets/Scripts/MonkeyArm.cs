using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// 猿の両手の引っかかる部分向けのスクリプト
// OnCollision系関数を親に渡すのみ
public class MonkeyArm : MonoBehaviour
{

    private Monkey parentObject;

    // Start is called before the first frame update
    void Start()
    {
        parentObject = gameObject.transform.parent.gameObject.GetComponent<Monkey>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("enter:"); // ぶつかった
        if (collider.transform.CompareTag("TreeRing"))
        {
            parentObject.relayOnTriggerEnter(collider);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        Debug.Log("exit:"); // ぶつかった
        if (collider.transform.CompareTag("TreeRing"))
        {
            parentObject.relayOnTriggerExit(collider);
        }
    }
}
