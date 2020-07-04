using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treering : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var angle = this.gameObject.transform.localEulerAngles;
        angle.y += Time.deltaTime * 60.0f;
        this.gameObject.transform.localEulerAngles = angle;
    }
}

