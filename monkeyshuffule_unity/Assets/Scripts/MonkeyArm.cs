using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
// ���̗���̃X�N���v�g
// OnCollision�n�֐���e�ɓn���̂�
/// </summary>
public class MonkeyArm : MonoBehaviour
{

//    private Rigidbody rb; // rigidBody�B�ė��p�p�ɎQ�Ƃ�ۑ��B
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
        // ���̗���̏㑤(������є��蕔��)
        if (gameObject.transform.CompareTag("MonkeyArmUpper"))
        {
            if (collider.transform.CompareTag("TreeRingLower"))
            {



                parentObject.TriggerEnterMonkeyArmUpperAndTreeRingLower(collider);
            }
        }

        // ���̗���̉���(���������蕔��)
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
