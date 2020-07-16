using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// 猿の両手の下側(くっつき判定部分向けのスクリプト
// OnCollision系関数を親に渡すのみ
public class MonkeyArmLower : MonoBehaviour
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
        if (collider.transform.CompareTag("TreeRingUpper"))
        {
            Debug.Log("OnTriggerEnter with MonkeyArmLower and TreeRingUpper");
            parentObject.TriggerEnterMonkeyArmLowerAndTreeRingUpper(collider);
        }
    }

}
