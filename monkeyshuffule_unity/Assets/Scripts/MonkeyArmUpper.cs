using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// 猿の両手の上側(吹っ飛び判定部分向けのスクリプト
// OnCollision系関数を親に渡すのみ
public class MonkeyArmUpper : MonoBehaviour
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
        if (collider.transform.CompareTag("TreeRingLower"))
        {
            Debug.Log("OnTriggerEnter with MonkeyArmUpper and TreeRingLower");
            parentObject.TriggerEnterMonkeyArmUpperAndTreeRingLower(collider);
        }
    }

}
